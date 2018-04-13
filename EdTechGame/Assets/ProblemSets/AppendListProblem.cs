using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ProblemSets
{
    public class AppendListProblem : ListProblem
    {
        public override Dialogue GetDialogue()
        {
            throw new NotImplementedException();
        }

        protected override string TaskInstruction
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string StartCode
        {
            get
            {
                return AppendStartCode;
            }
        }

        public override string Answer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool ValidateAnswer(string code, string codeOutput, out string hint)
        {
            throw new NotImplementedException();
        }
    }
}
