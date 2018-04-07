using Assets.Tutorials;
using Assets.Utils;
using System;

namespace Assets.ProblemSets
{
    public class ArithmeticProblem : Problem
    {
        public override Dialogue GetDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.BROSEPH,
                sentences = new string[]
                {
                    "Time to do some math bro. This won't be easy, but I know you can do it.",
                    "We use something called operators to perform math between two numbers.",
                    "Operators are just symbols we use like +, -, *, /, %, or **.",
                    "Use + for adding, - for subtracting, * for multiplying, / for dividing, % for remainder after dividing, and ** for expontentials.",
                    "If you don't remeber what those mean, then check out the Instructions section.",
                    this.BasicInstruction
                },
                successMessage = "That was mathematical bro! You just ADDED to your programming skills!",
                sprite = NpcNames.BROSEPH_SPRITE_HAPPY
            };
        }

        private string BasicInstruction
        {
            get
            {
                return "We have to print three values. 1. Five plus ten multiplied by one half. 2. Get remainder of seven divided by two, then minus the first value. 3. Second value cubed (to the power of three).";
            }
        }

        public override string Instructions
        {
            get
            {
                return $"{string.Format(TITLE_FORMAT, "Arithmetic Operators")}" +
                    $"Just as any other programming languages, the addition, subtraction, multiplication, and division operators can be used with numbers." +
                    $"Another operator available is the modulo (%) operator, which returns the integer remainder of the division. dividend % divisor = remainder." +
                    $"Using two multiplication symbols (**) makes a power relationship.{Environment.NewLine}{Environment.NewLine}" +
                    $"Operators:{Environment.NewLine}" +
                    $" - Addition: +{Environment.NewLine}" +
                    $" - Subtraction: -{Environment.NewLine}" +
                    $" - Multiplication: +{Environment.NewLine}" +
                    $" - Division: /{Environment.NewLine}" +
                    $" - Modulo: %{Environment.NewLine}" +
                    $" - Exponentiation (or power): **{Environment.NewLine}{Environment.NewLine}" +
                    $"Print the three values:{Environment.NewLine}" +
                    $" 1. Five plus ten multiplied by one half.{Environment.NewLine}" +
                    $" 2. Get remainder of seven divided by two, then minus the first value.{Environment.NewLine}" +
                    $" 3. Second value cubed (to the power of three).";
            }
        }

        public override string StartCode
        {
            get
            {
                return $"# + for addition, - for subtraction, * for multiplication, / for division,{Environment.NewLine}" +
                    $"# % for modulo (remainder after division), and ** for power (or exponentiation).{Environment.NewLine}" +
                    $"x = 1 + 10{Environment.NewLine}print x{Environment.NewLine}" +
                    $"y = 22.1 - 10.05{Environment.NewLine}print y{Environment.NewLine}" +
                    $"z = x * y{Environment.NewLine}print z{Environment.NewLine}" +
                    $"a = 5 / 2{Environment.NewLine}print a{Environment.NewLine}" +
                    $"b = 5 % 2{Environment.NewLine}print b{Environment.NewLine}" +
                    $"q = 3 ** 2{Environment.NewLine}print q{Environment.NewLine}" +
                    $"r = 3 ** 3{Environment.NewLine}print r{Environment.NewLine}";
            }
        }

        public override string Answer
        {
            get
            {
                return $"10.0{ Environment.NewLine}-9.0{Environment.NewLine}-729.0";
            }
        }

        private string RoundedAnswer
        {
            get
            {
                return $"10{ Environment.NewLine}-9{Environment.NewLine}-729";
            }
        }

        public override bool ValidateAnswer(string codeOutput, out string hint)
        {
            // Empty check.
            if (string.IsNullOrEmpty(codeOutput))
            {
                hint = NOTHING_PRINTED_HINT;
                return false;
            }

            // Trim output.
            codeOutput = codeOutput.Trim();

            // If the output isn't decimal or integer answers.
            if (codeOutput != this.Answer && codeOutput != this.RoundedAnswer)
            {
                // Split answer into parts. Answers should be same number of parts for both answer types.
                string[] answerParts = StringUtils.SplitByLines(this.Answer);
                string[] roundedAnswerParts = StringUtils.SplitByLines(this.RoundedAnswer);

                // For each part.
                for (int i = 0; i < answerParts.Length; i++)
                {
                    // If it does not contain either decimal answer or rounded answer, then let player know.
                    if (!codeOutput.Contains(answerParts[i]) && !codeOutput.Contains(roundedAnswerParts[i]))
                    {
                        hint = $"Your output is missing the solution to the {StringUtils.GetNthText(i + 1)} problem. {BasicInstruction}";
                        return false;
                    }
                }
            }

            // Either combination of answers will reach here.
            hint = "";
            return true;
        }
    }
}
