using Assets.Tutorials;
using System.Collections.Generic;

namespace Assets.ProblemSets
{
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
                    new StringOperatorsProblem()
                })
            };
        }
    }
}
