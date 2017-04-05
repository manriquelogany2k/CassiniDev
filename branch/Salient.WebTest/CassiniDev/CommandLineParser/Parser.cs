//////////////////////////////////////////////////////////////////////////////
//    Command Line Argument Parser
//    ----------------------------
//
//    Author: hotweird@hotmail.com
//
//    Microsoft Public License (Ms-PL)
//
//    This license governs use of the accompanying software. If you use the software, you
//    accept this license. If you do not accept the license, do not use the software.
//
//    1. Definitions
//
//    The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
//    same meaning here as under U.S. copyright law.
//
//    A "contribution" is the original software, or any additions or changes to the software.
//
//    A "contributor" is any person that distributes its contribution under this license.
//
//    "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
//    2. Grant of Rights
//
//    (A) Copyright Grant- Subject to the terms of this license, including the license conditions
//        and limitations in section 3, each contributor grants you a non-exclusive, worldwide,
//        royalty-free copyright license to reproduce its contribution, prepare derivative works
//        of its contribution, and distribute its contribution or any derivative works that you create.
//
//    (B) Patent Grant- Subject to the terms of this license, including the license conditions and
//        limitations in section 3, each contributor grants you a non-exclusive, worldwide,
//        royalty-free license under its licensed patents to make, have made, use, sell, offer for
//        sale, import, and/or otherwise dispose of its contribution in the software or derivative
//        works of the contribution in the software.
//
//    3. Conditions and Limitations
//
//    (A) No Trademark License- This license does not grant you rights to use any contributors'
//        name, logo, or trademarks.
//
//    (B) If you bring a patent claim against any contributor over patents that you claim are
//        infringed by the software, your patent license from such contributor to the software ends
//        automatically.
//
//    (C) If you distribute any portion of the software, you must retain all copyright, patent,
//        trademark, and attribution notices that are present in the software.
//
//    (D) If you distribute any portion of the software in source code form, you may do so only under
//        this license by including a complete copy of this license with your distribution. If you
//        distribute any portion of the software in compiled or object code form, you may only do so
//        under a license that complies with this license.
//
//    (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no
//        express warranties, guarantees or conditions. You may have additional consumer rights under
//        your local laws which this license cannot change. To the extent permitted under your local
//        laws, the contributors exclude the implied warranties of merchantability, fitness for a
//        particular purpose and non-infringement.
//
//    Usage
//    -----
//
//    Parsing command line arguments to a console application is a common problem. 
//    This library handles the common task of reading arguments from a command line 
//    and filling in the values in a type.
//
//    To use this library, define a class whose fields represent the data that your 
//    application wants to receive from arguments on the command line. Then call 
//    CommandLine.ParseArguments() to fill the object with the data 
//    from the command line. Each field in the class defines a command line argument. 
//    The type of the field is used to validate the data read from the command line. 
//    The name of the field defines the name of the command line option.
//
//    The parser can handle fields of the following types:
//
//    - string
//    - int
//    - uint
//    - bool
//    - enum
//    - array of the above type
//
//    For example, suppose you want to read in the argument list for wc (word count). 
//    wc takes three optional boolean arguments: -l, -w, and -c and a list of files.
//
//    You could parse these arguments using the following code:
//
//    class WCArguments
//    {
//        public bool lines;
//        public bool words;
//        public bool chars;
//        public string[] files;
//    }
//
//    class WC
//    {
//        static void Main(string[] args)
//        {
//            if (CommandLine.ParseArgumentsWithUsage(args, parsedArgs))
//            {
//            //     insert application code here
//            }
//        }
//    }
//
//    So you could call this aplication with the following command line to count 
//    lines in the foo and bar files:
//
//        wc.exe /lines /files:foo /files:bar
//
//    The program will display the following usage message when bad command line 
//    arguments are used:
//
//        wc.exe -x
//
//    Unrecognized command line argument '-x'
//        /lines[+|-]                         short form /l
//        /words[+|-]                         short form /w
//        /chars[+|-]                         short form /c
//        /files:<string>                     short form /f
//        @<file>                             Read response file for more options
//
//    That was pretty easy. However, you realy want to omit the "/files:" for the 
//    list of files. The details of field parsing can be controled using custom 
//    attributes. The attributes which control parsing behaviour are:
//
//    ArgumentAttribute 
//        - controls short name, long name, required, allow duplicates, default value
//        and help text
//    DefaultArgumentAttribute 
//        - allows omition of the "/name".
//        - This attribute is allowed on only one field in the argument class.
//
//    So for the wc.exe program we want this:
//
//    using System;
//    using Utilities;
//
//    class WCArguments
//    {
//        [Argument(ArgumentType.AtMostOnce, HelpText="Count number of lines in the input text.")]
//        public bool lines;
//        [Argument(ArgumentType.AtMostOnce, HelpText="Count number of words in the input text.")]
//        public bool words;
//        [Argument(ArgumentType.AtMostOnce, HelpText="Count number of chars in the input text.")]
//        public bool chars;
//        [DefaultArgument(ArgumentType.MultipleUnique, HelpText="Input files to count.")]
//        public string[] files;
//    }
//
//    class WC
//    {
//        static void Main(string[] args)
//        {
//            WCArguments parsedArgs = new WCArguments();
//            if (CommandLine.ParseArgumentsWithUsage(args, parsedArgs))
//            {
//            //     insert application code here
//            }
//        }
//    }
//
//
//
//    So now we have the command line we want:
//
//        wc.exe /lines foo bar
//
//    This will set lines to true and will set files to an array containing the 
//    strings "foo" and "bar".
//
//    The new usage message becomes:
//
//        wc.exe -x
//
//    Unrecognized command line argument '-x'
//    /lines[+|-]  Count number of lines in the input text. (short form /l)
//    /words[+|-]  Count number of words in the input text. (short form /w)
//    /chars[+|-]  Count number of chars in the input text. (short form /c)
//    @<file>      Read response file for more options
//    <files>      Input files to count. (short form /f)
//
//    If you want more control over how error messages are reported, how /help is 
//    dealt with, etc you can instantiate the CommandLine.Parser class.
//
//
//
//    Cheers,
//    Peter Hallam
//    C# Compiler Developer
//    Microsoft Corp.
//
//
//
//
//    Release Notes
//    -------------
//
//    10/02/2002 Initial Release
//    10/14/2002 Bug Fix
//    01/08/2003 Bug Fix in @ include files
//    10/23/2004 Added user specified help text, formatting of help text to 
//            screen width. Added ParseHelp for /?.
//    11/23/2004 Added support for default values.
//    02/23/2005 Fix bug with short name and default arguments.
//
//    12/24/2009 sky: Added ushort as valid argument type. 
//    12/27/2009 sky: todo: expose out and err to enable use in forms app.
//    12/29/2009 sky: added ArgumentsAttribute and GetGenericUsageString to allow attaching generic help text
//    01/01/2010 sky: split classes into seperate files
//    01/01/2010 sky: cleaned up Parser.cs
//////////////////////////////////////////////////////////////////////////////
/// 
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Cassini.CommandLine
{
    /// <summary>
    /// Parser for command line arguments.
    ///
    /// The parser specification is infered from the instance fields of the object
    /// specified as the destination of the parse.
    /// Valid argument types are: int, uint, string, bool, enums
    /// Also argument types of Array of the above types are also valid.
    /// 
    /// Error checking options can be controlled by adding a ArgumentAttribute
    /// to the instance fields of the destination object.
    ///
    /// At most one field may be marked with the DefaultArgumentAttribute
    /// indicating that arguments without a '-' or '/' prefix will be parsed as that argument.
    ///
    /// If not specified then the parser will infer default options for parsing each
    /// instance field. The default long name of the argument is the field name. The
    /// default short name is the first character of the long name. Long names and explicitly
    /// specified short names must be unique. Default short names will be used provided that
    /// the default short name does not conflict with a long name or an explicitly
    /// specified short name.
    ///
    /// Arguments which are array types are collection arguments. Collection
    /// arguments can be specified multiple times.
    /// </summary>
    public sealed class Parser
    {
        private ArrayList arguments;
        private Hashtable argumentMap;
        private Argument defaultArgument;
        private ErrorReporter reporter;
        private const int STD_OUTPUT_HANDLE = -11;
        private const int spaceBeforeParam = 2;
        private Type _argumentSpecification;

        /// <summary>
        /// The System Defined new line string.
        /// </summary>
        public const string NewLine = "\r\n";


        /// <summary>
        /// Don't ever call this.
        /// </summary>
        private Parser() { }

        /// <summary>
        /// Parses Command Line Arguments. Displays usage message to Console.Out
        /// if /?, /help or invalid arguments are encounterd.
        /// Errors are output on Console.Error.
        /// Use ArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name="arguments"> The actual arguments. </param>
        /// <param name="destination"> The resulting parsed arguments. </param>
        /// <returns> true if no errors were detected. </returns>
        public static bool ParseArgumentsWithUsage(string[] arguments, object destination)
        {
            if (Parser.ParseHelp(arguments) || !Parser.ParseArguments(arguments, destination))
            {
                // error encountered in arguments. Display usage message
                System.Console.Write(Parser.ArgumentsUsage(destination.GetType()));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Parses Command Line Arguments. 
        /// Errors are output on Console.Error.
        /// Use ArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name="arguments"> The actual arguments. </param>
        /// <param name="destination"> The resulting parsed arguments. </param>
        /// <returns> true if no errors were detected. </returns>
        public static bool ParseArguments(string[] arguments, object destination)
        {
            return Parser.ParseArguments(arguments, destination, new ErrorReporter(Console.Error.WriteLine));
        }

        /// <summary>
        /// Parses Command Line Arguments. 
        /// Use ArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name="arguments"> The actual arguments. </param>
        /// <param name="destination"> The resulting parsed arguments. </param>
        /// <param name="reporter"> The destination for parse errors. </param>
        /// <returns> true if no errors were detected. </returns>
        public static bool ParseArguments(string[] arguments, object destination, ErrorReporter reporter)
        {
            Parser parser = new Parser(destination.GetType(), reporter);
            return parser.Parse(arguments, destination);
        }

        private static void NullErrorReporter(string message)
        {
        }

        /// <summary>
        /// Checks if a set of arguments asks for help.
        /// </summary>
        /// <param name="args"> Args to check for help. </param>
        /// <returns> Returns true if args contains /? or /help. </returns>
        public static bool ParseHelp(string[] args)
        {
            Parser helpParser = new Parser(typeof(HelpArgument), new ErrorReporter(NullErrorReporter));
            HelpArgument helpArgument = new HelpArgument();
            helpParser.Parse(args, helpArgument);
            return helpArgument.help;
        }

        /// <summary>
        /// Returns a Usage string for command line argument parsing.
        /// Use ArgumentAttributes to control parsing behaviour.
        /// Formats the output to the width of the current console window.
        /// </summary>
        /// <param name="argumentType"> The type of the arguments to display usage for. </param>
        /// <returns> Printable string containing a user friendly description of command line arguments. </returns>
        public static string ArgumentsUsage(Type argumentType)
        {
            int screenWidth = Parser.GetConsoleWindowWidth();
            if (screenWidth == 0)
                screenWidth = 80;
            return ArgumentsUsage(argumentType, screenWidth);
        }

        /// <summary>
        /// Returns a Usage string for command line argument parsing.
        /// Use ArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name="argumentType"> The type of the arguments to display usage for. </param>
        /// <param name="columns"> The number of columns to format the output to. </param>
        /// <returns> Printable string containing a user friendly description of command line arguments. </returns>
        public static string ArgumentsUsage(Type argumentType, int columns)
        {
            return (new Parser(argumentType, null)).GetUsageString(columns);
        }

        /// <summary>
        /// Returns the number of columns in the current console window
        /// </summary>
        /// <returns>Returns the number of columns in the current console window</returns>
        public static int GetConsoleWindowWidth()
        {
            int screenWidth;
            CONSOLE_SCREEN_BUFFER_INFO csbi = new CONSOLE_SCREEN_BUFFER_INFO();

            int rc;
            rc = GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), ref csbi);
            screenWidth = csbi.dwSize.x;
            return screenWidth;
        }

        /// <summary>
        /// Searches a StringBuilder for a character
        /// </summary>
        /// <param name="text"> The text to search. </param>
        /// <param name="value"> The character value to search for. </param>
        /// <param name="startIndex"> The index to stat searching at. </param>
        /// <returns> The index of the first occurence of value or -1 if it is not found. </returns>
        public static int IndexOf(StringBuilder text, char value, int startIndex)
        {
            for (int index = startIndex; index < text.Length; index++)
            {
                if (text[index] == value)
                    return index;
            }

            return -1;
        }

        /// <summary>
        /// Searches a StringBuilder for a character in reverse
        /// </summary>
        /// <param name="text"> The text to search. </param>
        /// <param name="value"> The character to search for. </param>
        /// <param name="startIndex"> The index to start the search at. </param>
        /// <returns>The index of the last occurence of value in text or -1 if it is not found. </returns>
        public static int LastIndexOf(StringBuilder text, char value, int startIndex)
        {
            for (int index = Math.Min(startIndex, text.Length - 1); index >= 0; index--)
            {
                if (text[index] == value)
                    return index;
            }

            return -1;
        }

        
        /// <summary>
        /// Creates a new command line argument parser.
        /// </summary>
        /// <param name="argumentSpecification"> The type of object to  parse. </param>
        /// <param name="reporter"> The destination for parse errors. </param>
        public Parser(Type argumentSpecification, ErrorReporter reporter)
        {
            _argumentSpecification = argumentSpecification;
            this.reporter = reporter;
            this.arguments = new ArrayList();
            this.argumentMap = new Hashtable();

            foreach (FieldInfo field in argumentSpecification.GetFields())
            {
                if (!field.IsStatic && !field.IsInitOnly && !field.IsLiteral)
                {
                    ArgumentAttribute attribute = GetAttribute(field);
                    if (attribute is DefaultArgumentAttribute)
                    {
                        //SKY:TODO:REPLACE?: Debug.Assert(this.defaultArgument == null);
                        this.defaultArgument = new Argument(attribute, field, reporter);
                    }
                    else
                    {
                        this.arguments.Add(new Argument(attribute, field, reporter));
                    }
                }
            }

            // add explicit names to map
            foreach (Argument argument in this.arguments)
            {
                //SKY:TODO:REPLACE?: Debug.Assert(!argumentMap.ContainsKey(argument.LongName));
                this.argumentMap[argument.LongName] = argument;
                if (argument.ExplicitShortName)
                {
                    if (argument.ShortName != null && argument.ShortName.Length > 0)
                    {
                        //SKY:TODO:REPLACE?: Debug.Assert(!argumentMap.ContainsKey(argument.ShortName));
                        this.argumentMap[argument.ShortName] = argument;
                    }
                    else
                    {
                        argument.ClearShortName();
                    }
                }
            }

            // add implicit names which don't collide to map
            foreach (Argument argument in this.arguments)
            {
                if (!argument.ExplicitShortName)
                {
                    if (argument.ShortName != null && argument.ShortName.Length > 0 && !argumentMap.ContainsKey(argument.ShortName))
                        this.argumentMap[argument.ShortName] = argument;
                    else
                        argument.ClearShortName();
                }
            }
        }

        private static ArgumentAttribute GetAttribute(FieldInfo field)
        {
            object[] attributes = field.GetCustomAttributes(typeof(ArgumentAttribute), false);
            if (attributes.Length == 1)
                return (ArgumentAttribute)attributes[0];

            //SKY:TODO:REPLACE?: Debug.Assert(attributes.Length == 0);
            return null;
        }

        private void ReportUnrecognizedArgument(string argument)
        {
            this.reporter(string.Format("Unrecognized command line argument '{0}'", argument));
        }

        /// <summary>
        /// Parses an argument list into an object
        /// </summary>
        /// <param name="args"></param>
        /// <param name="destination"></param>
        /// <returns> true if an error occurred </returns>
        private bool ParseArgumentList(string[] args, object destination)
        {
            bool hadError = false;
            if (args != null)
            {
                foreach (string argument in args)
                {
                    if (argument.Length > 0)
                    {
                        switch (argument[0])
                        {
                            case '-':
                            case '/':
                                int endIndex = argument.IndexOfAny(new char[] { ':', '+', '-' }, 1);
                                string option = argument.Substring(1, endIndex == -1 ? argument.Length - 1 : endIndex - 1);
                                string optionArgument;
                                if (option.Length + 1 == argument.Length)
                                {
                                    optionArgument = null;
                                }
                                else if (argument.Length > 1 + option.Length && argument[1 + option.Length] == ':')
                                {
                                    optionArgument = argument.Substring(option.Length + 2);
                                }
                                else
                                {
                                    optionArgument = argument.Substring(option.Length + 1);
                                }

                                Argument arg = (Argument)this.argumentMap[option];
                                if (arg == null)
                                {
                                    ReportUnrecognizedArgument(argument);
                                    hadError = true;
                                }
                                else
                                {
                                    hadError |= !arg.SetValue(optionArgument, destination);
                                }
                                break;
                            case '@':
                                string[] nestedArguments;
                                hadError |= LexFileArguments(argument.Substring(1), out nestedArguments);
                                hadError |= ParseArgumentList(nestedArguments, destination);
                                break;
                            default:
                                if (this.defaultArgument != null)
                                {
                                    hadError |= !this.defaultArgument.SetValue(argument, destination);
                                }
                                else
                                {
                                    ReportUnrecognizedArgument(argument);
                                    hadError = true;
                                }
                                break;
                        }
                    }
                }
            }

            return hadError;
        }

        /// <summary>
        /// Parses an argument list.
        /// </summary>
        /// <param name="args"> The arguments to parse. </param>
        /// <param name="destination"> The destination of the parsed arguments. </param>
        /// <returns> true if no parse errors were encountered. </returns>
        public bool Parse(string[] args, object destination)
        {
            bool hadError = ParseArgumentList(args, destination);

            // check for missing required arguments
            foreach (Argument arg in this.arguments)
            {
                hadError |= arg.Finish(destination);
            }
            if (this.defaultArgument != null)
            {
                hadError |= this.defaultArgument.Finish(destination);
            }

            return !hadError;
        }

        /// <summary>
        /// A user firendly usage string describing the command line argument syntax.
        /// </summary>
        public string GetUsageString(int screenWidth)
        {
            ArgumentHelpStrings[] strings = GetAllHelpStrings();

            int maxParamLen = 0;
            foreach (ArgumentHelpStrings helpString in strings)
            {
                maxParamLen = Math.Max(maxParamLen, helpString.syntax.Length);
            }

            const int minimumNumberOfCharsForHelpText = 10;
            const int minimumHelpTextColumn = 5;
            const int minimumScreenWidth = minimumHelpTextColumn + minimumNumberOfCharsForHelpText;

            int helpTextColumn;
            int idealMinimumHelpTextColumn = maxParamLen + spaceBeforeParam;
            screenWidth = Math.Max(screenWidth, minimumScreenWidth);
            if (screenWidth < (idealMinimumHelpTextColumn + minimumNumberOfCharsForHelpText))
                helpTextColumn = minimumHelpTextColumn;
            else
                helpTextColumn = idealMinimumHelpTextColumn;

            const string newLine = "\n";
            StringBuilder builder = new StringBuilder();

            // 01/01/2010 sky
            string genericUsage = GetGenericUsageString(_argumentSpecification, screenWidth);
            if (!string.IsNullOrEmpty(genericUsage))
            {
                builder.AppendLine(genericUsage);
            }
            

            foreach (ArgumentHelpStrings helpStrings in strings)
            {
                // add syntax string
                int syntaxLength = helpStrings.syntax.Length;
                builder.Append(helpStrings.syntax);

                // start help text on new line if syntax string is too long
                int currentColumn = syntaxLength;
                if (syntaxLength >= helpTextColumn)
                {
                    builder.Append(newLine);
                    currentColumn = 0;
                }

                // add help text broken on spaces
                int charsPerLine = screenWidth - helpTextColumn;
                int index = 0;
                while (index < helpStrings.help.Length)
                {
                    // tab to start column
                    builder.Append(' ', helpTextColumn - currentColumn);
                    currentColumn = helpTextColumn;

                    // find number of chars to display on this line
                    int endIndex = index + charsPerLine;
                    if (endIndex >= helpStrings.help.Length)
                    {
                        // rest of text fits on this line
                        endIndex = helpStrings.help.Length;
                    }
                    else
                    {
                        endIndex = helpStrings.help.LastIndexOf(' ', endIndex - 1, Math.Min(endIndex - index, charsPerLine));
                        if (endIndex <= index)
                        {
                            // no spaces on this line, append full set of chars
                            endIndex = index + charsPerLine;
                        }
                    }

                    // add chars
                    builder.Append(helpStrings.help, index, endIndex - index);
                    index = endIndex;

                    // do new line
                    AddNewLine(newLine, builder, ref currentColumn);

                    // don't start a new line with spaces
                    while (index < helpStrings.help.Length && helpStrings.help[index] == ' ')
                        index++;
                }

                // add newline if there's no help text                
                if (helpStrings.help.Length == 0)
                {
                    builder.Append(newLine);
                }
            }
            

            return builder.ToString();
        }

        /// <summary>
        /// 01/01/2010 sky
        /// </summary>
        private static string GetGenericUsageString(Type type,int cols)
        {
            object[] attributes = type.GetCustomAttributes(typeof (ArgumentsAttribute), true);
            if(attributes.Length== 0 || !((ArgumentsAttribute)attributes[0]).HasHelpText)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            string usage = ((ArgumentsAttribute) attributes[0]).HelpText;
            // simple width formatter
            var lines = Regex.Split(usage, Environment.NewLine);
            foreach (string line in lines)
            {
                var words = Regex.Split(line, " ");
                var currentLine = string.Empty;
                foreach (string word in words)
                {
                    if (currentLine.Length+word.Length+1>cols)
                    {
                        // start new line
                        sb.AppendLine(currentLine);
                        currentLine = word + " ";
                    }
                    else
                    {
                        currentLine += (word + " ");
                    }
                }
                sb.AppendLine(currentLine);
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        private static void AddNewLine(string newLine, StringBuilder builder, ref int currentColumn)
        {
            builder.Append(newLine);
            currentColumn = 0;
        }
        
        private ArgumentHelpStrings[] GetAllHelpStrings()
        {
            ArgumentHelpStrings[] strings = new ArgumentHelpStrings[NumberOfParametersToDisplay()];

            int index = 0;
            foreach (Argument arg in this.arguments)
            {
                strings[index] = GetHelpStrings(arg);
                index++;
            }
            strings[index++] = new ArgumentHelpStrings("@<file>", "Read response file for more options");
            if (this.defaultArgument != null)
                strings[index++] = GetHelpStrings(this.defaultArgument);

            return strings;
        }

        private static ArgumentHelpStrings GetHelpStrings(Argument arg)
        {
            return new ArgumentHelpStrings(arg.SyntaxHelp, arg.FullHelpText);
        }

        private int NumberOfParametersToDisplay()
        {
            int numberOfParameters = this.arguments.Count + 1;
            if (HasDefaultArgument)
                numberOfParameters += 1;
            return numberOfParameters;
        }

        /// <summary>
        /// Does this parser have a default argument.
        /// </summary>
        /// <value> Does this parser have a default argument. </value>
        public bool HasDefaultArgument
        {
            get { return this.defaultArgument != null; }
        }

        private bool LexFileArguments(string fileName, out string[] arguments)
        {
            string args = null;

            try
            {
                using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    args = (new StreamReader(file)).ReadToEnd();
                }
            }
            catch (Exception e)
            {
                this.reporter(string.Format("Error: Can't open command line argument file '{0}' : '{1}'", fileName, e.Message));
                arguments = null;
                return false;
            }

            bool hadError = false;
            ArrayList argArray = new ArrayList();
            StringBuilder currentArg = new StringBuilder();
            bool inQuotes = false;
            int index = 0;

            // while (index < args.Length)
            try
            {
                while (true)
                {
                    // skip whitespace
                    while (char.IsWhiteSpace(args[index]))
                    {
                        index += 1;
                    }

                    // # - comment to end of line
                    if (args[index] == '#')
                    {
                        index += 1;
                        while (args[index] != '\n')
                        {
                            index += 1;
                        }
                        continue;
                    }

                    // do one argument
                    do
                    {
                        if (args[index] == '\\')
                        {
                            int cSlashes = 1;
                            index += 1;
                            while (index == args.Length && args[index] == '\\')
                            {
                                cSlashes += 1;
                            }

                            if (index == args.Length || args[index] != '"')
                            {
                                currentArg.Append('\\', cSlashes);
                            }
                            else
                            {
                                currentArg.Append('\\', (cSlashes >> 1));
                                if (0 != (cSlashes & 1))
                                {
                                    currentArg.Append('"');
                                }
                                else
                                {
                                    inQuotes = !inQuotes;
                                }
                            }
                        }
                        else if (args[index] == '"')
                        {
                            inQuotes = !inQuotes;
                            index += 1;
                        }
                        else
                        {
                            currentArg.Append(args[index]);
                            index += 1;
                        }
                    } while (!char.IsWhiteSpace(args[index]) || inQuotes);
                    argArray.Add(currentArg.ToString());
                    currentArg.Length = 0;
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                // got EOF 
                if (inQuotes)
                {
                    this.reporter(string.Format("Error: Unbalanced '\"' in command line argument file '{0}'", fileName));
                    hadError = true;
                }
                else if (currentArg.Length > 0)
                {
                    // valid argument can be terminated by EOF
                    argArray.Add(currentArg.ToString());
                }
            }

            arguments = (string[])argArray.ToArray(typeof(string));
            return hadError;
        }

        private static string LongName(ArgumentAttribute attribute, FieldInfo field)
        {
            return (attribute == null || attribute.DefaultLongName) ? field.Name : attribute.LongName;
        }

        private static string ShortName(ArgumentAttribute attribute, FieldInfo field)
        {
            if (attribute is DefaultArgumentAttribute)
                return null;
            if (!ExplicitShortName(attribute))
                return LongName(attribute, field).Substring(0, 1);
            return attribute.ShortName;
        }

        private static string HelpText(ArgumentAttribute attribute, FieldInfo field)
        {
            if (attribute == null)
                return null;
            else
                return attribute.HelpText;
        }

        private static bool HasHelpText(ArgumentAttribute attribute)
        {
            return (attribute != null && attribute.HasHelpText);
        }

        private static bool ExplicitShortName(ArgumentAttribute attribute)
        {
            return (attribute != null && !attribute.DefaultShortName);
        }

        private static object DefaultValue(ArgumentAttribute attribute, FieldInfo field)
        {
            return (attribute == null || !attribute.HasDefaultValue) ? null : attribute.DefaultValue;
        }

        private static Type ElementType(FieldInfo field)
        {
            if (IsCollectionType(field.FieldType))
                return field.FieldType.GetElementType();
            else
                return null;
        }

        private static ArgumentType Flags(ArgumentAttribute attribute, FieldInfo field)
        {
            if (attribute != null)
                return attribute.Type;
            else if (IsCollectionType(field.FieldType))
                return ArgumentType.MultipleUnique;
            else
                return ArgumentType.AtMostOnce;
        }

        private static bool IsCollectionType(Type type)
        {
            return type.IsArray;
        }

        private static bool IsValidElementType(Type type)
        {
            //SKY:12/25/09 - added ushort
            return type != null && (
                                       type == typeof(int) ||
                                       type == typeof(uint) ||
                                       type == typeof(ushort) ||
                                       type == typeof(string) ||
                                       type == typeof(bool) ||
                                       type.IsEnum);
        }

        #region Embedded Types

        [System.Diagnostics.DebuggerDisplay("Name = {LongName}")]
        private class Argument
        {
            public Argument(ArgumentAttribute attribute, FieldInfo field, ErrorReporter reporter)
            {
                this.longName = Parser.LongName(attribute, field);
                this.explicitShortName = Parser.ExplicitShortName(attribute);
                this.shortName = Parser.ShortName(attribute, field);
                this.hasHelpText = Parser.HasHelpText(attribute);
                this.helpText = Parser.HelpText(attribute, field);
                this.defaultValue = Parser.DefaultValue(attribute, field);
                this.elementType = ElementType(field);
                this.flags = Flags(attribute, field);
                this.field = field;
                this.seenValue = false;
                this.reporter = reporter;
                this.isDefault = attribute != null && attribute is DefaultArgumentAttribute;

                if (IsCollection)
                {
                    this.collectionValues = new ArrayList();
                }

                //SKY:TODO:REPLACE?: Debug.Assert(this.longName != null && this.longName != "");
                //SKY:TODO:REPLACE?: Debug.Assert(!this.isDefault || !this.ExplicitShortName);
                //SKY:TODO:REPLACE?: Debug.Assert(!IsCollection || AllowMultiple, "Collection arguments must have allow multiple");
                //SKY:TODO:REPLACE?: Debug.Assert(!Unique || IsCollection, "Unique only applicable to collection arguments");
                //SKY:TODO:REPLACE?: Debug.Assert(IsValidElementType(Type) ||IsCollectionType(Type));
                //SKY:TODO:REPLACE?: Debug.Assert((IsCollection && IsValidElementType(elementType)) ||(!IsCollection && elementType == null));
                //SKY:TODO:REPLACE?: Debug.Assert(!(this.IsRequired && this.HasDefaultValue), "Required arguments cannot have default value");
                //SKY:TODO:REPLACE?: Debug.Assert(!this.HasDefaultValue || (this.defaultValue.GetType() == field.FieldType), "Type of default value must match field type");
            }

            public bool Finish(object destination)
            {
                if (this.SeenValue)
                {
                    if (this.IsCollection)
                    {
                        this.field.SetValue(destination, this.collectionValues.ToArray(this.elementType));
                    }
                }
                else
                {
                    if (this.HasDefaultValue)
                    {
                        this.field.SetValue(destination, this.DefaultValue);
                    }
                }

                return ReportMissingRequiredArgument();
            }

            private bool ReportMissingRequiredArgument()
            {
                if (this.IsRequired && !this.SeenValue)
                {
                    if (this.IsDefault)
                        reporter(string.Format("Missing required argument '<{0}>'.", this.LongName));
                    else
                        reporter(string.Format("Missing required argument '/{0}'.", this.LongName));
                    return true;
                }
                return false;
            }

            private void ReportDuplicateArgumentValue(string value)
            {
                this.reporter(string.Format("Duplicate '{0}' argument '{1}'", this.LongName, value));
            }

            public bool SetValue(string value, object destination)
            {
                if (SeenValue && !AllowMultiple)
                {
                    this.reporter(string.Format("Duplicate '{0}' argument", this.LongName));
                    return false;
                }
                this.seenValue = true;

                object newValue;
                if (!ParseValue(this.ValueType, value, out newValue))
                    return false;
                if (this.IsCollection)
                {
                    if (this.Unique && this.collectionValues.Contains(newValue))
                    {
                        ReportDuplicateArgumentValue(value);
                        return false;
                    }
                    else
                    {
                        this.collectionValues.Add(newValue);
                    }
                }
                else
                {
                    this.field.SetValue(destination, newValue);
                }

                return true;
            }

            public Type ValueType
            {
                get { return this.IsCollection ? this.elementType : this.Type; }
            }

            private void ReportBadArgumentValue(string value)
            {
                this.reporter(string.Format("'{0}' is not a valid value for the '{1}' command line option", value, this.LongName));
            }

            private bool ParseValue(Type type, string stringData, out object value)
            {
                // null is only valid for bool variables
                // empty string is never valid
                if ((stringData != null || type == typeof(bool)) && (stringData == null || stringData.Length > 0))
                {
                    try
                    {
                        if (type == typeof(string))
                        {
                            value = stringData;
                            return true;
                        }
                        else if (type == typeof(bool))
                        {
                            if (stringData == null || stringData == "+")
                            {
                                value = true;
                                return true;
                            }
                            else if (stringData == "-")
                            {
                                value = false;
                                return true;
                            }
                        }
                        else if (type == typeof(int))
                        {
                            value = int.Parse(stringData);
                            return true;
                        }
                        else if (type == typeof(uint))
                        {
                            value = int.Parse(stringData);
                            return true;
                        }
                        //SKY:12/25/09 - added ushort
                        else if (type == typeof(ushort))
                        {
                            value = ushort.Parse(stringData);
                            return true;
                        }
                        else
                        {
                            //SKY:TODO:REPLACE?: Debug.Assert(type.IsEnum);

                            bool valid = false;
                            foreach (string name in Enum.GetNames(type))
                            {
                                if (name == stringData)
                                {
                                    valid = true;
                                    break;
                                }
                            }
                            if (valid)
                            {
                                value = Enum.Parse(type, stringData, true);
                                return true;
                            }
                        }
                    }
                    catch
                    {
                        // catch parse errors
                    }
                }

                ReportBadArgumentValue(stringData);
                value = null;
                return false;
            }

            private void AppendValue(StringBuilder builder, object value)
            {
                //SKY:12/25/09 - add ushort
                if (value is string || value is int || value is uint || value is ushort || value.GetType().IsEnum)
                {
                    builder.Append(value.ToString());
                }
                else if (value is bool)
                {
                    builder.Append((bool)value ? "+" : "-");
                }
                else
                {
                    bool first = true;
                    foreach (object o in (System.Array)value)
                    {
                        if (!first)
                        {
                            builder.Append(", ");
                        }
                        AppendValue(builder, o);
                        first = false;
                    }
                }
            }

            public string LongName
            {
                get { return this.longName; }
            }

            public bool ExplicitShortName
            {
                get { return this.explicitShortName; }
            }

            public string ShortName
            {
                get { return this.shortName; }
            }

            public bool HasShortName
            {
                get { return this.shortName != null; }
            }

            public void ClearShortName()
            {
                this.shortName = null;
            }

            public bool HasHelpText
            {
                get { return this.hasHelpText; }
            }

            public string HelpText
            {
                get { return this.helpText; }
            }

            public object DefaultValue
            {
                get { return this.defaultValue; }
            }

            public bool HasDefaultValue
            {
                get { return null != this.defaultValue; }
            }

            public string FullHelpText
            {
                get
                {
                    StringBuilder builder = new StringBuilder();
                    if (this.HasHelpText)
                    {
                        builder.Append(this.HelpText);
                    }
                    if (this.HasDefaultValue)
                    {
                        if (builder.Length > 0)
                            builder.Append(" ");
                        builder.Append("Default value:'");
                        AppendValue(builder, this.DefaultValue);
                        builder.Append('\'');
                    }
                    if (this.HasShortName)
                    {
                        if (builder.Length > 0)
                            builder.Append(" ");
                        builder.Append("(short form /");
                        builder.Append(this.ShortName);
                        builder.Append(")");
                    }
                    return builder.ToString();
                }
            }

            public string SyntaxHelp
            {
                get
                {
                    StringBuilder builder = new StringBuilder();

                    if (this.IsDefault)
                    {
                        builder.Append("<");
                        builder.Append(this.LongName);
                        builder.Append(">");
                    }
                    else
                    {
                        builder.Append("/");
                        builder.Append(this.LongName);
                        Type valueType = this.ValueType;
                        if (valueType == typeof(int))
                        {
                            builder.Append(":<int>");
                        }
                        else if (valueType == typeof(uint))
                        {
                            builder.Append(":<uint>");
                        }
                        else if (valueType == typeof(bool))
                        {
                            builder.Append("[+|-]");
                        }
                        else if (valueType == typeof(string))
                        {
                            builder.Append(":<string>");
                        }
                        else
                        {
                            //SKY:TODO:REPLACE?: Debug.Assert(valueType.IsEnum);

                            builder.Append(":{");
                            bool first = true;
                            foreach (FieldInfo field in valueType.GetFields())
                            {
                                if (field.IsStatic)
                                {
                                    if (first)
                                        first = false;
                                    else
                                        builder.Append('|');
                                    builder.Append(field.Name);
                                }
                            }
                            builder.Append('}');
                        }
                    }

                    return builder.ToString();
                }
            }

            public bool IsRequired
            {
                get { return 0 != (this.flags & ArgumentType.Required); }
            }

            public bool SeenValue
            {
                get { return this.seenValue; }
            }

            public bool AllowMultiple
            {
                get { return 0 != (this.flags & ArgumentType.Multiple); }
            }

            public bool Unique
            {
                get { return 0 != (this.flags & ArgumentType.Unique); }
            }

            public Type Type
            {
                get { return field.FieldType; }
            }

            public bool IsCollection
            {
                get { return IsCollectionType(Type); }
            }

            public bool IsDefault
            {
                get { return this.isDefault; }
            }

            private string longName;
            private string shortName;
            private string helpText;
            private bool hasHelpText;
            private bool explicitShortName;
            private object defaultValue;
            private bool seenValue;
            private FieldInfo field;
            private Type elementType;
            private ArgumentType flags;
            private ArrayList collectionValues;
            private ErrorReporter reporter;
            private bool isDefault;
        }

        private class HelpArgument
        {
            [Argument(ArgumentType.AtMostOnce, ShortName = "?")]
            public bool help = false;
        }

        private struct ArgumentHelpStrings
        {
            public ArgumentHelpStrings(string syntax, string help)
            {
                this.syntax = syntax;
                this.help = help;
            }

            public string syntax;
            public string help;
        }

        private struct COORD
        {
            internal Int16 x;
            internal Int16 y;
        }

        private struct SMALL_RECT
        {
            internal Int16 Left;
            internal Int16 Top;
            internal Int16 Right;
            internal Int16 Bottom;
        }

        private struct CONSOLE_SCREEN_BUFFER_INFO
        {
            internal COORD dwSize;
            internal COORD dwCursorPosition;
            internal Int16 wAttributes;
            internal SMALL_RECT srWindow;
            internal COORD dwMaximumWindowSize;
        }
        #endregion

        #region PInvoke
        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", EntryPoint = "GetConsoleScreenBufferInfo", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetConsoleScreenBufferInfo(int hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

        #endregion
    }
}