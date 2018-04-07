﻿using Assets.Tutorials;
using Assets.Utils;
using System;

namespace Assets.ProblemSets
{
    public class HelloWorldProblem : Problem
    {
        public override Dialogue GetDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.BROSEPH,
                sentences = new string[]
                {
                    "Bro, did you finish the programming homework?",
                    "I can't figure it out! Can you please help me solve it?",
                    "Come on bro! I'll pay you back big time time.",
                    "Brooooooooooooooooooooooooooooooooooooooooooooo, please?!",
                    "I think we have to print 'hello word' followed by 'goodbye world', but I'm not sure how to do that! " +
                    "The text inside quotes is referred to as strings. We can use single quotes or double quotes. Take your pick."
                },
                successMessage = "Nice bro! I think that was right!",
                sprite = NpcNames.BROSEPH_SPRITE_HAPPY
            };
        }

        public override string StartCode
        {
            get
            {
                return @"print 'Hello world!'";
            }
        }

        public override string Answer
        {
            get
            {
                return $"hello world{Environment.NewLine}goodbye world";
            }
        }

        public override string Instructions
        {
            get
            {
                return string.Format(TITLE_FORMAT, "Hello, World!") +
                    "Python is a very simple language, and has a very straightforward syntax. " +
                    "It encourages programmers to program without boilerplate (prepared) code. " +
                    "The simplest function in Python is the \"print\" function. " +
                    "It simply prints out a line followed up a line break (referred to as a newline). " +
                    "The words surrounded by the double quotes are refferred to \"strings\". " +
                    "Think of strings as a sequence of characters (letters, numerals, symbols and punctuation marks). " +
                    "This is typically the text you find in a book or article.\n\n" +
                    "There are two major Python versions, Python 2 and Python 3. Python 2 and 3 are quite different. This tutorial uses Python 2.\n\n" +
                    "Use the \"print\" command to print the line \"Hello World!\" followed by \"Goodbye world\" on the next line.";
            }
        }

        public override bool ValidateAnswer(string codeOutput, out string hint)
        {
            if (string.IsNullOrEmpty(codeOutput))
            {
                hint = NOTHING_PRINTED_HINT;
                return false;
            }

            codeOutput = codeOutput.ToLowerInvariant().Trim();

            string[] codeOutputLines = StringUtils.SplitByLines(codeOutput);
            string[] answerLines = StringUtils.SplitByLines(this.Answer);

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

            hint = $"Your output should say:\n{this.Answer}";
            return false;
        }

    }
}
