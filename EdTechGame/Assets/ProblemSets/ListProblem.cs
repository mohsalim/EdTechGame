using Assets.Utils;
using System;

namespace Assets.ProblemSets
{
    public abstract class ListProblem : Problem, ISharedInstructions
    {
        public override string Instructions
        {
            get
            {
                return string.Format(TITLE_FORMAT, "Loops") +
                    "Lists are very similar to arrays. " +
                    "They can contain any type of variable, and they can contain as many variables as you wish. " +
                    $"Here is an example of a list. {StringUtils.FormatCodeBlock(BasicStartCode)}" +
                    "Notice that the index is zero-based. " +
                    "This means that if you want to access the first item in the list, its index will be 0. " +
                    $"If you want to access the second item in the list, its index will be 1. And so on.{Environment.NewLine}{Environment.NewLine}" +
                    "Additionally, you can create an empty list and add items to it." +
                    $"Here is an example of how to build a list using the method 'append'. {StringUtils.FormatCodeBlock(AppendStartCode)}" +
                    $"{TaskInstructions}";
            }
        }

        public abstract string TaskInstructions
        {
            get;
        }

        protected string BasicStartCode
        {
            get
            {
                return $"mylist = [1,2,3]{Environment.NewLine}" +
                    $"print mylist[0] # Lists are 0-based so this will get the first element which is 1.{Environment.NewLine}" +
                    $"mylist[0] = 5 # You can change the value at a specific index.{Environment.NewLine}" +
                    $"print mylist # You can print the entire list.{Environment.NewLine}" +
                    $"print len(mylist) # You can print the number of items (or length) of the list.";
            }
        }

        /// <summary>
        /// Will be used for AppendListProblem.cs.
        /// </summary>
        protected string AppendStartCode
        {
            get
            {
                return $"mylist = [] # This is an empty list.{Environment.NewLine}" +
                    $"mylist.append(1) # Add 1 to the list.{Environment.NewLine}" +
                    $"mylist.append(2) # Add 2 to the list (after 1).{Environment.NewLine}" +
                    $"mylist.append(3) # Add 3 to the list (after 1 and 2).{Environment.NewLine}" +
                    $"print mylist[0] # Prints 1.{Environment.NewLine}" +
                    $"print mylist[1] # Prints 2.{Environment.NewLine}" +
                    $"print mylist[2] # Prints 3.{Environment.NewLine}";
            }
        }
    }
}
