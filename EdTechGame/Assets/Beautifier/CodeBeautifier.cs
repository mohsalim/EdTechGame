using System;
using System.Linq;

namespace Assets.Beautifier
{
    /// <summary>
    /// TODO: Look into how to get this to work while typing and running code.
    /// https://answers.unity.com/questions/1485585/color-specific-words-in-input-field.html
    /// </summary>
    public class CodeBeautifier
    {
        /// <summary>
        /// Key word format. Such as "if" or "True".
        /// </summary>
        protected const string KEY_WORD_FORMAT = "<color=#00ffffff>{0}</color>";

        /// <summary>
        /// Key words in Python 2.7.
        /// </summary>
        protected static readonly string[] KEY_WORDS = new string[]
        {
            "True",
            "False",
            "if",
            "else",
            "elif",
            "not",
            "or",
            "and",
            "float",
            "int",
            "print"
        };

        /// <summary>
        /// Beautify keywords in the given code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string Beautify(string code)
        {
            string beautifedCode = "";

            string[] lines = code.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach(string line in lines)
            {
                string beautifiedLine = "";

                string[] parts = line.Split(' ');

                foreach (string part in parts)
                {
                    // TODO: What about functions such as float(...)?
                    beautifiedLine += KEY_WORDS.FirstOrDefault(k => k == part) == null
                        ? part
                        : string.Format(KEY_WORD_FORMAT, part) 
                        + " ";              
                }

                // TODO: Trimming the end may be problematic.
                // TODO: Adding new line can be problematic.
                beautifedCode += beautifiedLine.TrimEnd() + Environment.NewLine;
            }

            return beautifedCode;
        }

    }
}
