using Assets.Tutorials;
using Assets.Utils;
using System;
using System.Linq;

namespace Assets.ProblemSets
{
    public class StringOperatorsProblem : Problem
    {
        public override Dialogue GetDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.PROFESSOR,
                sentences = new string[]
                {
                    "Good morning students. Today we will be learning about String Arithmetics.",
                    "I know most of you won't know this because you're not as smart as me, but you can use some arithemetic operators with strings.",
                    "This may be too hard for you to get, so feel free to leave my classroom like quitters.",
                    this.DialogueInstructions
                    
                },
                successMessage = "I'm surprised you actually got that right... " +
                "For the next task, you must work alone AND you must print out your code and submit it to me! " +
                "I am old school like that...and it's easier on my eyes...",
                sprite = NpcNames.PROFESSOR_SPRITE_HAPPY
            };
        }

        private string DialogueInstructions
        {
            get
            {
                return "Since most people have trouble remembering simple things, I'll repeat the obvious. Strings are words. " +
                    "You can concat (or add) words together with the + operator. You can repeat words in sequence with the * operator. " +
                    this.TaskInstructions;
            }
        }

        private string TaskInstructions
        {
            get
            {
                return $"For this task, I want you to concat '{FirstWord}' with 3 repeated cases of '{SecondWord}'. It should produce '{Answer}'.";
            }
        }

        public override string Instructions
        {
            get
            {
                return string.Format(TITLE_FORMAT, "Using Operators with Strings") +
                    "Python supports concatenating strings using the addition operator. You can do this by using the plus (+) operator. " +
                    "Thinking of concatting as the 'adding' of two strings. You can add two words together. You can even add an empty space. " +
                    $"Adding empty spaces can be very useful if you want to make your words readable when they're printed. {Environment.NewLine}{Environment.NewLine}" +
                    $"word1 = 'hi'{Environment.NewLine}word2 = 'friend'{Environment.NewLine}words = word1 + ' ' + word2 # 'hi friend'{Environment.NewLine}{Environment.NewLine}" +
                    "Additionally, Python supports multiplying strings to form a string with a repeating sequence. " +
                    "You can do this by using the multiply (*) operator. " +
                    $"This is useful for when repeating a word multiple times without having to copy and paste the string.{Environment.NewLine}{Environment.NewLine}" +
                    $"word1 = 'hi '{Environment.NewLine}words = word1 * 3 # 'hi hi hi '{Environment.NewLine}{Environment.NewLine}{this.TaskInstructions}";
            }
        }

        public override string StartCode
        {
            get
            {
                return $"helloworld = 'hello' + ' ' + 'world'{Environment.NewLine}print helloworld{Environment.NewLine}{Environment.NewLine}" +
                    $"lotsofhellos = 'hello' * 10{Environment.NewLine}print lotsofhellos{Environment.NewLine}";
            }
        }

        public override string Answer
        {
            get
            {
                return $"{FirstWord} {SecondWord} {SecondWord} {SecondWord}";
            }
        }

        private string FirstWord
        {
            get
            {
                return "baby";
            }
        }

        private string SecondWord
        {
            get
            {
                return "bye";
            }
        }

        public override bool ValidateAnswer(string code, string codeOutput, out string hint)
        {
            if (string.IsNullOrEmpty(codeOutput))
            {
                hint = $"Your code isn't printing anything. {NpcNames.PROFESSOR_HINT_PREFIX}{this.DialogueInstructions}";
                return false;
            }

            code = code.Trim();
            string[] codeLines = StringUtils.SplitByLines(code);

            bool hasConcat = StringUtils.HasCodeString(codeLines, "+");
            if (!hasConcat)
            {
                hint = $"Your code doesn't concat any string. {NpcNames.PROFESSOR_HINT_PREFIX}{this.DialogueInstructions}";
                return false;
            }

            bool hasSequence = StringUtils.HasCodeString(codeLines, "*");
            if (!hasSequence)
            {
                hint = $"Your code doesn't create a repeating sequence for any string. {NpcNames.PROFESSOR_HINT_PREFIX}{this.DialogueInstructions}";
                return false;
            }

            codeOutput = codeOutput.Trim().ToLowerInvariant();

            if (codeOutput == this.Answer)
            {
                hint = "";
                return true;
            }

            if (!codeOutput.Contains(FirstWord))
            {
                hint = $"Your output is missing the word '{FirstWord}'. {NpcNames.PROFESSOR_HINT_PREFIX}{this.DialogueInstructions}";
                return false;
            }

            if (!codeOutput.StartsWith(FirstWord))
            {
                hint = $"Your output is needs to start with the word '{FirstWord}'. {NpcNames.PROFESSOR_HINT_PREFIX}{this.DialogueInstructions}";
                return false;
            }

            if (!codeOutput.Contains(SecondWord))
            {
                hint = $"Your output is missing the word '{SecondWord}'. {NpcNames.PROFESSOR_HINT_PREFIX}{this.DialogueInstructions}";
                return false;
            }

            int secondWordCount = codeOutput.Split(' ').Count(c => c == SecondWord);
            if (secondWordCount < 3)
            {
                hint = $"Your output has too little cases of '{SecondWord}'. {NpcNames.PROFESSOR_HINT_PREFIX}{this.DialogueInstructions}";
                return false;
            }

            if (secondWordCount > 3)
            {
                hint = $"Your output has too many cases of '{SecondWord}'. {NpcNames.PROFESSOR_HINT_PREFIX}{this.DialogueInstructions}";
                return false;
            }

            if (!codeOutput.EndsWith($"{SecondWord} {SecondWord} {SecondWord}"))
            {
                hint = $"Your output needs to end with three repeating cases of '{SecondWord}'. {NpcNames.PROFESSOR_HINT_PREFIX}{this.DialogueInstructions}";
                return false;
            }

            hint = $"Your output seems incorrect. {NpcNames.PROFESSOR_HINT_PREFIX}{this.DialogueInstructions}";
            return false;
        }
    }
}
