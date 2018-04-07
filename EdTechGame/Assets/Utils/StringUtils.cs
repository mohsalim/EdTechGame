using System;

namespace Assets.Utils
{
    public class StringUtils
    {
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
    }
}
