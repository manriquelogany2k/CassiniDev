using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace CassiniDev.FixtureExamples.TestWeb
{
    [Serializable]
    [DataContract]
    public class HelloWorldArgs
    {
        private string _message;
        [DataMember]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }

    [ServiceContract(Namespace = "http://TestWebApp")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TestAjaxService
    {
        [OperationContract]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [OperationContract]
        public HelloWorldArgs HelloWorldWithArgsInOut(HelloWorldArgs args)
        {
            args.Message = "you said: " + args.Message;
            return args;
        }
    }
}
