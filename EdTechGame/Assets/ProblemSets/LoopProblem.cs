using System;

namespace Assets.ProblemSets
{
    public abstract class LoopProblem : Problem
    {
        public override string Instructions
        {
            get
            {
                return string.Format(TITLE_FORMAT, "Lists") +
                    "There are two types of loops in Python, for and while. We'll just be focusing on for loops for now. " +
                    $"For loops iterate over a given sequence. Here is an example:{Environment.NewLine}{Environment.NewLine}{LoopBasicsStartCode}{Environment.NewLine}{Environment.NewLine}" +
                    $"{TabSpacesMessage} If not properly tabbed or spaced over, your code may not compile or have unexpected runtime errors! " +
                    $"Tabs and spaces allow the compiler to know that the code is under a certain scope. In our case, the tabbed over code is in the scope of the for loop. " +
                    $"We'll discuss scopes in more detail later.{Environment.NewLine}{Environment.NewLine}" +
                    "For loops can iterate over a sequence of numbers using the range() functions. " +
                    "The range function returns a new list with numbers of that specified range. " +
                    "Note that the range function is zero based." +
                    $"Here is a for loop example using the range function:{Environment.NewLine}{Environment.NewLine}{LoopRangeStartCode}{Environment.NewLine}" +
                    $"{TaskInstructions}";
            }
        }

        /// <summary>
        /// Task specific instructions.
        /// </summary>
        protected abstract string TaskInstructions
        {
            get;
        }

        /// <summary>
        /// Start code for loop basics.
        /// </summary>
        protected string LoopBasicsStartCode
        {
            get
            {
                return $"primes = [2, 3, 5, 7]{Environment.NewLine}" +
                    $"for prime in primes:{Environment.NewLine}" +
                    $"   print(prime){TabSpacesComment}";
            }
        }

        /// <summary>
        /// Message used in instructions and code comments to let player know tabs and spaces matter.
        /// This is the first time the player really needs to be careful with tabs/spaces for scope.
        /// </summary>
        private string TabSpacesMessage
        {
            get
            {
                return "The code used in the for loop must be tabbed over (or 4 spaces over)! Spaces and tabs matter in Python!";
            }
        }

        /// <summary>
        /// Tab/spaces message as a Python comment.
        /// </summary>
        private string TabSpacesComment
        {
            get
            {
                return $" # {TabSpacesMessage}";
            }
        }

        /// <summary>
        /// Start code for loops using range function.
        /// </summary>
        protected string LoopRangeStartCode
        {
            get
            {
                return $"# Prints out the numbers 0,1,2,3,4{Environment.NewLine}" +
                    $"for x in range(5):{Environment.NewLine}" +
                    $"   print(x){TabSpacesMessage}{Environment.NewLine}{Environment.NewLine}" +
                    $"# Prints out 3,4,5{Environment.NewLine}" +
                    $"for x in range(3, 6):{Environment.NewLine}" +
                    $"   print(x){TabSpacesMessage}";
            }
        }
    }
}
