using Assets.Tutorials;
using System.Collections.Generic;

namespace Assets.ProblemSets
{
    /// <summary>
    /// Set of tutoials. Tutorials based on learnpython.org.
    /// 
    /// https://www.learnpython.org/en/Functions
    /// </summary>
    public class TutorialSet
    {
        /// <summary>
        /// Variables and types tutorial.
        /// </summary>
        /// <returns></returns>
        public static Tutorial GetTypesAndArithmeticTutorial()
        {
            return new Tutorial()
            {
                Problems = new Queue<Problem>(new Problem[]
                {
                    new HelloWorldProblem(),
                    new IntegerProblem(),
                    new FloatingPointProblem(),
                    new BooleanProblem(),
                    new VariableTest(),
                    new ArithmeticProblem()
                })
            };
        }

        /// <summary>
        /// Strings and lists tutorial.
        /// </summary>
        /// <returns></returns>
        public static Tutorial GetStringsAndListsTutorial()
        {
            return new Tutorial()
            {
                Problems = new Queue<Problem>(new Problem[]
                {
                    new StringOperatorsProblem(),
                    new BasicListProblem(),
                    new AppendListProblem()
                })
            };
        }

        /// <summary>
        /// Loops tutorial.
        /// </summary>
        /// <returns></returns>
        public static Tutorial GetLoopsTutorial()
        {
            return new Tutorial()
            {
                Problems = new Queue<Problem>(new Problem[]
                {
                    new LoopBasicsProblem(),
                    new LoopRangeProblem()
                })
            };
        }

        /// <summary>
        /// Functions tutorial.
        /// </summary>
        /// <returns></returns>
        public static Tutorial GetFunctionsTutorial()
        {
            return new Tutorial()
            {
                Problems = new Queue<Problem>(new Problem[]
                {
                    new FunctionBasicsProblem(),
                    new FunctionTest()
                })
            };
        }
    }
}
