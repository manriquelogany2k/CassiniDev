// /* **********************************************************************************
//  *
//  * Copyright (c) Sky Sanders. All rights reserved.
//  * 
//  * This source code is subject to terms and conditions of the Microsoft Public
//  * License (Ms-PL). A copy of the license can be found in the license.htm file
//  * included in this distribution.
//  *
//  * You must not remove this notice, or any other, from this software.
//  *
//  * **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using mshtml;

namespace CassiniDev.Testing
{
    /// <summary>
    /// TODO: check to see if JS is parsed in IHTMLDocument
    ///       seems inline js is run but cannot confirm that events like onload are fired. 
    ///       so don't count on reliablly running js pages in this... use watin for that
    /// TODO: implement a timeout
    /// 
    /// The idea to fetch the content with a webrequest and parse it with mshtml instead of fetching AND parsing with mshtml
    /// came from http://cambridgecode.blogspot.com/2009/11/simple-html-parsing-code-using-mshtml.html.
    /// 
    /// This eliminates the dependancy on the windows message pump (Application.DoEvents()) and makes the code just a bit less smelly. 
    /// 
    /// parsing code adapted from http://cambridgecode.blogspot.com/2009/11/simple-html-parsing-code-using-mshtml.html. No need to re-invent that
    /// wheel until/if it becomes obvious that it needs improvement. Maybe some tests? ;o)
    /// </summary>
    public class HttpRequestHelper : IDisposable
    {
        public const string ContentTypeApplicationFormUTF8 = "application/x-www-form-urlencoded; charset=UTF-8";
        public const string ContentTypeApplicationJsonUTF8 = "application/json; charset=UTF-8";
        public const string ContentTypeTextJsonUTF8 = "text/json; charset=UTF-8";

        #region HttpWebRequest

        /// <summary>
        /// Posts an AJAX request using text/json as content type.
        /// </summary>
        /// <param name="requestUri">Service uri including method name</param>
        /// <param name="postData">Typically a Dictionary&lt;string, object&gt;. Anonymous types are acceptable</param>
        /// <param name="cookies">Optional, can pass null. Used to send and retrieve cookies. Pass the same instance to subsequent calls to maintain state if required.</param>
        /// <returns>Json string</returns>
        public static string AjaxApp(Uri requestUri, ICollection<KeyValuePair<string, object>> postData,
                                     CookieContainer cookies)
        {
            return Ajax(requestUri, postData, ContentTypeApplicationJsonUTF8, cookies);
        }

        /// <summary>
        /// Posts an AJAX request using application/json as content type.
        /// </summary>
        /// <param name="requestUri">Service uri including method name</param>
        /// <param name="postData">Typically a Dictionary&lt;string, object&gt;. Anonymous types are acceptable</param>
        /// <param name="cookies">Optional, can pass null. Used to send and retrieve cookies. Pass the same instance to subsequent calls to maintain state if required.</param>
        /// <returns>Json string</returns>
        public static string AjaxTxt(Uri requestUri, ICollection<KeyValuePair<string, object>> postData,
                                     CookieContainer cookies)
        {
            return Ajax(requestUri, postData, ContentTypeTextJsonUTF8, cookies);
        }

        /// <summary>
        /// Posts an AJAX request
        /// </summary>
        /// <param name="requestUri">Service uri including method name</param>
        /// <param name="postData">Typically a Dictionary&lt;string, object&gt;. Anonymous types are acceptable</param>
        /// <param name="contentType">One of the standard UTF8 Json types.</param>
        /// <param name="cookies">Optional, can pass null. Used to send and retrieve cookies. Pass the same instance to subsequent calls to maintain state if required.</param>
        /// <returns>Json string</returns>
        private static string Ajax(Uri requestUri, ICollection<KeyValuePair<string, object>> postData,
                                   string contentType, CookieContainer cookies)
        {
            byte[] body = null;

            if (postData != null && postData.Count > 0)
            {
                string data = JavaScriptSerializer.Serialize(postData);
                body = Encoding.UTF8.GetBytes(data);
            }
            return SendHttpRequest(requestUri, "POST", contentType, body, cookies);
        }


        /// <summary>
        /// Posts a form
        /// 
        /// Is sufficient if all that is required is the html served. If JS execution is desired 
        /// limited support is provided by parsing the return value with ParseHtml.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="postData">Standard form values collection</param>
        /// <param name="cookies">Optional, can pass null. Used to send and retrieve cookies. Pass the same instance to subsequent calls to maintain state if required.</param>
        /// <returns></returns>
        public static string Post(Uri requestUri, NameValueCollection postData, CookieContainer cookies)
        {
            byte[] data = null;
            if (postData != null)
            {
                data = Encoding.UTF8.GetBytes(UrlEncode(postData));
            }

            return SendHttpRequest(requestUri, "POST", ContentTypeApplicationFormUTF8, data, cookies);
        }

        /// <summary>
        /// Gets a document. 
        /// 
        /// Is sufficient if all that is required is the html served. If JS execution is desired 
        /// limited support is provided by parsing the return value with ParseHtml.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="postData">Standar query string arguments</param>
        /// <param name="cookies">Optional, can pass null. Used to send and retrieve cookies. Pass the same instance to subsequent calls to maintain state if required.</param>
        /// <returns></returns>
        public static string Get(Uri requestUri, NameValueCollection postData, CookieContainer cookies)
        {
            string data = string.Empty;
            if (postData != null)
            {
                data = UrlEncode(postData);
            }

            Uri uri = AppendQuery(requestUri, data);
            return SendHttpRequest(uri, "GET", "", null, cookies);
        }


        /// <summary>
        /// 
        /// Is sufficient if all that is required is the html served. If JS execution is desired 
        /// limited support is provided by parsing the return value with ParseHtml.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="method"></param>
        /// <param name="contentType"></param>
        /// <param name="postData">UTF8 encoded bytes</param>
        /// <param name="cookies">Optional, can pass null. Used to send and retrieve cookies. Pass the same instance to subsequent calls to maintain state if required.</param>
        /// <returns></returns>
        public static string SendHttpRequest(Uri requestUri, string method, string contentType, byte[] postData,
                                             CookieContainer cookies)
        {
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(requestUri);

            // set the headers
            // TODO: provide built in useragent options
            req.Method = method;
            req.Headers.Add("Pragma", "no-cache");
            req.Headers.Add("Cache-Control", "no-cache");
            req.ContentType = contentType;
            req.CookieContainer = cookies;

            if (postData != null)
            {
                req.ContentLength = postData.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(postData, 0, postData.Length);
                }
            }

            string responseText;

            // send the request and get the response
            using (WebResponse response = req.GetResponse())
            {
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                responseText = reader.ReadToEnd();
                responseStream.Close();
                response.Close();
            }

            return responseText;
        }

        #endregion

        #region MSHTML

        /// <summary>
        /// IHTMLDocument5 accessor to common document HTMLDocument2
        /// </summary>
        public IHTMLDocument5 HTMLDocument5
        {
            get { return (IHTMLDocument5) _doc2; }
        }

        /// <summary>
        /// IHTMLDocument4 accessor to common document HTMLDocument2
        /// </summary>
        public IHTMLDocument4 HTMLDocument4
        {
            get { return (IHTMLDocument4) _doc2; }
        }

        /// <summary>
        /// IHTMLDocument3 accessor to common document HTMLDocument2
        /// </summary>
        public IHTMLDocument3 HTMLDocument3
        {
            get { return (IHTMLDocument3) _doc2; }
        }

        /// <summary>
        /// IHTMLDocument accessor to common document HTMLDocument2
        /// </summary>
        public IHTMLDocument HTMLDocument
        {
            get { return _doc2; }
        }


        /// <summary>
        /// Primary accessor to common document
        /// </summary>
        public IHTMLDocument2 HTMLDocument2
        {
            get { return _doc2; }
        }


        private static IHTMLDocument2 GetMsHtmlDocument(String source)
        {
            IHTMLDocument2 rtn = (IHTMLDocument2) new HTMLDocument();
            rtn.write(new object[] {source});
            rtn.close();
            return rtn;
        }

        /// <summary>
        /// Executes inline JS confirmed; Events such as onload
        /// not confirmed. Onload handler injected element not present in
        /// body html. May need to wait but having trouble sinking the event.
        /// Use Watin for robust script testing.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IHTMLDocument2 ParseHtml(String source)
        {
            _doc2 = GetMsHtmlDocument(source);
            return _doc2;
        }

        /// <summary>
        /// Executes inline JS confirmed; Events such as onload
        /// not confirmed. Onload handler injected element not present in
        /// body html. May need to wait but having trouble sinking the event.
        /// Use Watin for robust script testing.
        /// </summary>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IHTMLDocument2 ParseHtml(IHTMLDocument2 source)
        {
            _doc2 = source;
            return _doc2;
        }

        /// <summary>
        /// Executes inline JS confirmed; Events such as onload
        /// not confirmed. Onload handler injected element not present in
        /// body html. May need to wait but having trouble sinking the event.
        /// Use Watin for robust script testing.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IHTMLDocument2 ParseHtml(IHTMLElement source)
        {
            _doc2 = GetMsHtmlDocument(source.outerHTML);
            return _doc2;
        }

        #endregion

        #region Fields

        private bool _disposed;
        private IHTMLDocument2 _doc2;

        #endregion

        #region Static Helpers

        public static JavaScriptSerializer JavaScriptSerializer = new JavaScriptSerializer();

        public static string UrlEncode(NameValueCollection qs)
        {
            return string.Join("&",
                               Array.ConvertAll(qs.AllKeys,
                                                key =>
                                                string.Format("{0}={1}", HttpUtility.UrlEncode(key),
                                                              HttpUtility.UrlEncode(qs[key]))));
        }

        public static Uri AppendQuery(Uri uri, string query)
        {
            return new Uri(uri + (string.IsNullOrEmpty(uri.Query) ? "?" : "&") + query);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (!_disposed)
            {
                _doc2 = null;
            }
            _disposed = true;

            GC.SuppressFinalize(this);
        }

        ~HttpRequestHelper()
        {
            Dispose();
        }

        #endregion

        /// <summary>
        /// Finds any elements matching the ElementTagName and the collection of options in searchOptions
        /// </summary>
        /// <param name="searchOptions">Contains our search specification</param>
        /// <returns>List of objects found matching our search options</returns>
        public HtmlElementList FindElements(HtmlElementSearchOptions searchOptions)
        {
            HtmlElementList rtn = new HtmlElementList();

            // Search through every known element in this document object
            foreach (IHTMLElement element in (IHTMLElementCollection) _doc2.body.all)
            {
                // If HtmlElementSearchOptions.ElementTagName is not empty or null, it is a required search parameter
                // Otherwise, any element tag type will be searched on
                if (searchOptions.ElementTagName.Equals(element.tagName, StringComparison.InvariantCultureIgnoreCase) ||
                    String.IsNullOrEmpty(searchOptions.ElementTagName))
                {
                    bool IsMatchingElement = true;

                    // Validate all search option attributes
                    foreach (KeyValue option in searchOptions)
                    {
                        // Attempt to get the attribute
                        // not using DLR
                        //dynamic attribute = element.getAttribute(option.Key.ToLower(), 0);
                        object attribute = element.getAttribute(option.Key.ToLower(), 0);

                        // If key value is null, it will match a non-existing attribute
                        if (attribute == null && option.Value == null)
                        {
                            continue;
                        }

                        // When the attribute is not found on the element, it returns null
                        if (attribute != null)
                        {
                            // Invalidate this element as a valid element if the value does not match the search value
                            if (!option.Value.Equals(attribute.ToString(), StringComparison.InvariantCultureIgnoreCase))
                            {
                                IsMatchingElement = false;
                                break;
                            }

                            continue;
                        }
                        else
                        {
                            IsMatchingElement = false;
                            break;
                        }
                    }

                    // If we have deemed our element as matching, lets add it to the return collection
                    if (IsMatchingElement) rtn.Add(element);
                }
            }

            return rtn;
        }

        /// <summary>
        /// Returns the first element whose ID matches the ID parameter
        /// </summary>
        /// <param name="ID">ID of element to find</param>
        /// <returns>Html element found. If no elements are found, returns null</returns>
        public IHTMLElement FindElementByID(String ID)
        {
            IHTMLElement rtn = null;

            // Instantiate our options to search by ID.
            // In this case, we will not specify ElementTagName so we can match on any element with matching ID
            HtmlElementSearchOptions options = new HtmlElementSearchOptions();
            options.Add(new KeyValue("ID", ID));

            // Run the search
            HtmlElementList foundElements = FindElements(options);

            // Save the first element found to our return object
            if (foundElements.Count > 0) rtn = foundElements[0];

            return rtn;
        }

        #region Obsolete - Can use HttpWebRequest and just parse it with MSHTML

        // obviated by hint found on http://cambridgecode.blogspot.com/2009/11/simple-html-parsing-code-using-mshtml.html
        ///// <summary>
        ///// </summary>
        ///// <param name="url"></param>
        ///// <returns></returns>
        //public IHTMLDocument2 NavigateTo(Uri url)
        //{
        //    _doc = new HTMLDocumentClass();

        //    IPersistStreamInit ips = (IPersistStreamInit)_doc;
        //    ips.InitNew();

        //    _doc2 = _doc.createDocumentFromUrl(url.AbsoluteUri, "null");

        //    while (_doc2.readyState != "complete")
        //    {
        //        // no way around this - mshtml needs a message pump
        //        Application.DoEvents();
        //    }
        //    return _doc2;
        //}


        //#region Nested type: IPersistStreamInit

        //[Guid("7FD52380-4E07-101B-AE2D-08002B2EC713")]
        //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        //private interface IPersistStreamInit
        //{
        //    void GetClassID(out Guid pClassID);
        //    int IsDirty();
        //    void Load(IStream pStm);
        //    void Save(IStream pStm, bool fClearDirty);
        //    void GetSizeMax(out long pcbSize);
        //    void InitNew();
        //}
        //#endregion

        #endregion
    }

    /// <summary>
    /// Acts as a named interface to a list of IHTMLElements allowing for extending in later development
    /// </summary>
    public class HtmlElementList : List<IHTMLElement>
    {
    }

    /// <summary>
    /// Holds all possible search options
    /// </summary>
    public class HtmlElementSearchOptions : List<KeyValue>
    {
        // Optional attribute to search based on the tag type
        // e.g. To search for only <table> tags, the value should be 'table'
        public String ElementTagName = String.Empty;
    }

    /// <summary>
    /// Holds key & value pair for attribute searching
    /// </summary>
    public class KeyValue
    {
        public String Key;
        public String Value;

        public KeyValue(String key, String value)
        {
            Key = key;
            Value = value;
        }
    }
}