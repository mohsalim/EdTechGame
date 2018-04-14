using Assets.Utils;
using System;

namespace Assets.ProblemSets
{
    public abstract class FunctionProblem : Problem, ISharedInstructions
    {
        public override string Instructions
        {
            get
            {
                return string.Format(TITLE_FORMAT, "Functions") +
                    "Functions are a convenient way to divide your code into useful blocks, allowing us to order our code, make it more readable, reuse it and save some time. " +
                    "Also functions are a key way to define interfaces so programmers can share their code. As we have seen on previous tutorials, Python makes use of blocks. " +
                    $"A block is a area of code of written in the format of: {StringUtils.FormatCodeBlock(FunctionCodeFormatSample)}" +
                    "Where a block line is more Python code (even another block), and the block head is of the following format: block_keyword block_name(argument1,argument2, ...). " +
                    "Block keywords you already know are 'for' and 'in'. Functions in python are defined using the block keyword 'def', followed with the function's name as the block's name. " +
                    $"For example: {StringUtils.FormatCodeBlock(FunctionExample)}" +
                    $"Functions may also receive arguments (variables passed from the caller to the function). " +
                    $"For example: {StringUtils.FormatCodeBlock(FunctionWithArgsExample)}" +
                    $"Functions may return a value to the caller, using the keyword'return'. " +
                    $"For example: {StringUtils.FormatCodeBlock(FunctionWithReturnExample)}" +
                    $"{TaskInstructions}";
            }
        }

        /// <summary>
        /// Function code format used in instruction.
        /// </summary>
        private string FunctionCodeFormatSample
        {
            get
            {
                return $"block_head:{Environment.NewLine}" +
                    $"  1st block line{Environment.NewLine}" +
                    $"  2nd block line{Environment.NewLine}" +
                    $"  ...{Environment.NewLine}";
            }
        }

        public abstract string TaskInstructions
        {
            get;
        }

        public string FunctionBasicsStartCode
        {
            get
            {
                return $"{FunctionExample}{Environment.NewLine}{Environment.NewLine}" +
                    $"{FunctionWithArgsExample}{Environment.NewLine}{Environment.NewLine}" +
                    $"{FunctionWithReturnExample}{Environment.NewLine}{Environment.NewLine}";
            }
        }

        /// <summary>
        /// Part 1 of basic example.
        /// </summary>
        private string FunctionExample
        {
            get
            {
                return $"def my_function():{Environment.NewLine}" +
                    $"   print 'Hello From My Function!'{Environment.NewLine}" +
                    $"my_function() # Call the function after defining it.";
            }
        }

        /// <summary>
        /// Part 2 of basic example.
        /// </summary>
        private string FunctionWithArgsExample
        {
            get
            {
                return $"def my_function_with_args(text):{Environment.NewLine}" +
                    $"  print 'This is my text: ' + text{Environment.NewLine}" +
                    $"my_function_with_args('code monkey') # Call function with argument after defining it.";
            }
        }

        /// <summary>
        /// Part 3 of basic example.
        /// </summary>
        private string FunctionWithReturnExample
        {
            get
            {
                return $"def sum_two_numbers(a, b):{Environment.NewLine}" +
                    $"  return a + b{Environment.NewLine}" +
                    $"s = sum_two_numbers(9, 2) # Call the function after defining it and place it's returned value into a variable.{Environment.NewLine}" +
                    $"print s # Print the results returned from the newly defined function.";
            }
        }

        public string FunctionTestStartCode
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
