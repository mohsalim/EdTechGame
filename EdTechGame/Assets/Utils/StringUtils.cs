using System;
using System.Linq;

namespace Assets.Utils
{
    public class StringUtils
    {
        /// <summary>
        /// Python code to remove Python comments.
        /// 
        /// https://stackoverflow.com/a/18381470/2498729
        /// </summary>
        private static readonly string RemovePythonCommentsCodeString =
            $"def remove_comments(string):{Environment.NewLine}" +
            $"   pattern = r\"(\".*?\"|\'.*?\')|(/\\*.*?\\*/|//[^\r\n]*$){Environment.NewLine}" +
            $"   # first group captures quoted strings (double or single){Environment.NewLine}" +
            $"   # second group captures comments (//single-line or /* multi-line */){Environment.NewLine}" +
            $"   regex = re.compile(pattern, re.MULTILINE|re.DOTALL){Environment.NewLine}" +
            $"   def _replacer(match):{Environment.NewLine}" +
            $"       #if the 2nd group (capturing comments) is not None:{Environment.NewLine}" +
            $"       #it means we have captured a non-quoted (real) comment string.{Environment.NewLine}" +
            $"       if match.group(2) is not None:{Environment.NewLine}" +
            $"           return \"\" # so we will return empty to remove the comment{Environment.NewLine}" +
            $"       else: # otherwise, we will return the 1st group{Environment.NewLine}" +
            $"           return match.group(1) # captured quoted-string{Environment.NewLine}" +
            $"   return regex.sub(_replacer, string){Environment.NewLine}";

        /// <summary>
        /// Split string by new line character.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string[] SplitByLines(string s)
        {
            return s.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }

        /// <summary>
        /// Creates the Nst, Nnd, Nrd, or Nth text based on given integer N.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetNthText(int i)
        {

            char[] digits = i.ToString().ToCharArray();
            int lastDigit = int.Parse(digits[digits.Length - 1].ToString());

            string suffix;
            switch (lastDigit)
            {
                case 1:
                    suffix = "st";
                    break;
                case 2:
                    suffix = "nd";
                    break;
                case 3:
                    suffix = "rd";
                    break;
                default:
                    suffix = "th";
                    break;
            }

            return $"{i}{suffix}";
        }

        /// <summary>
        /// Checks to see if the given lines of code has a code string that isn't in a comment.
        /// 
        /// TODO: This will fail if comment is in same line.
        /// </summary>
        /// <param name="codeLines"></param>
        /// <param name="codeString"></param>
        /// <returns></returns>
        public static bool HasCodeString(string[] codeLines, string codeString)
        {
            return codeLines.FirstOrDefault(c => !c.Trim().StartsWith("#") && c.Trim().Contains(codeString)) != null;
        }

        /// <summary>
        /// If there are too many or too few lines, then we usually use the text "many" or "few".
        /// </summary>
        /// <param name="requiredNumberOfLines"></param>
        /// <param name="actualNumberOfLines"></param>
        /// <returns></returns>
        public static string GetFewOrMany(int requiredNumberOfLines, int actualNumberOfLines)
        {
            if (actualNumberOfLines > requiredNumberOfLines)
            {
                return "many";
            }

            if (actualNumberOfLines < requiredNumberOfLines)
            {
                return "few";
            }

            throw new ArgumentException($"requiredNumberOfLines {requiredNumberOfLines} cannot be equal to actualNumberOfLines {actualNumberOfLines}.");
        }

        /// <summary>
        /// Add two new lines before and after code block.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string FormatCodeBlock(string code)
        {
            return $"{Environment.NewLine}{Environment.NewLine}{code}{Environment.NewLine}{Environment.NewLine}";
        }

        /// <summary>
        /// Remove python comments
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /*
        public static string RemovePythonComments(string code)
        {
            // TODO What about single or double quotes?
            string regexCode = $"{RemovePythonCommentsCodeString}{Environment.NewLine}print remove_comments({code})";
        }
        */
    }
}
