using System;

namespace Assets.ProblemSets
{
    public class HelloWorldProblem : Problem
    {
        public override string StartCode
        {
            get
            {
                return @"print 'Hello world!";
            }
        }

        public override string Answer
        {
            get
            {
                return $"hello wolrd{Environment.NewLine}goodbye world";
            }
        }

        private string[] SplitByLines(string s)
        {
            return s.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public override bool ValidateAnswer(string codeOutput, out string hint)
        {
            if (string.IsNullOrEmpty(codeOutput))
            {
                hint = NOTHING_PRINTED_HINT;
                return false;
            }

            codeOutput = codeOutput.ToLowerInvariant().Trim();

            string[] codeOutputLines = SplitByLines(codeOutput);
            string[] answerLines = SplitByLines(this.Answer);

            if (codeOutputLines.Length != answerLines.Length)
            {
                hint = $"You should be printing {answerLines.Length} things.";
                return false;
            }

            bool containsAllText = true;
            for(int i = 0; i < codeOutputLines.Length; i++)
            {
                containsAllText &= codeOutputLines[i].Contains(answerLines[i]);
            }

            if (containsAllText)
            {
                hint = null;
                return true;
            }

            for (int i = 0; i < codeOutputLines.Length; i++)
            {
                string[] answerParts = answerLines[i].Split(' ');
                for (int j = 0; j < answerParts.Length; j++)
                {
                    if (!codeOutputLines[i].Contains(answerParts[j]))
                    {
                        hint = $"Looks like your output is missing the word {answerParts[j]}.";
                        return false;
                    }
                }
            }

            hint = $"Your output should say ${this.Answer}";
            return false;
        }

    }
}
