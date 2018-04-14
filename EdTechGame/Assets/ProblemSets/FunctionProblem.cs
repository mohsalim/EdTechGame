using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ProblemSets
{
    public abstract class FunctionProblem : Problem, ISharedInstructions
    {
        public override string Instructions
        {
            get
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
