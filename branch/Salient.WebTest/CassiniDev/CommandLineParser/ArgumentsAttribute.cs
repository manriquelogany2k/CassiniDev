using System;

namespace Cassini.CommandLine
{
    /// <summary>
    /// Allows attaching generic help text to arguments class
    /// 
    /// 12/29/09 sky: added 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ArgumentsAttribute:Attribute
    {
        /// <summary>
        /// Returns true if the argument has help text specified.
        /// </summary>
        public bool HasHelpText { get { return null != this._helpText; } }

        /// <summary>
        /// The help text for the argument.
        /// </summary>
        public string HelpText
        {
            get { return this._helpText; }
            set { this._helpText = value; }
        }

        private string _helpText;        
    }
}