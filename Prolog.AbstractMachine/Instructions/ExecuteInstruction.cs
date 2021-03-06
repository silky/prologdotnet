using System;
using System.Collections.Generic;
using System.Text;

namespace Axiom.Runtime.Instructions
{
    public class ExecuteInstruction : AbstractInstruction
    {
        private string _label;
        private int _arity;

        public override string Name()
        {
            return "execute";
        }

        public override int NumberOfArguments()
        {
            return 2;
        }

        public override void Execute(AbstractMachineState state)
        {
            AMProgram program = (AMProgram)state.Program;

            string procedureName = _label + "/" + _arity.ToString();

            if (program.IsDefined(procedureName))
            {
                program.NumberOfArguments = _arity;
                program.P = program[procedureName];
            }
            else
            {
                state.Backtrack();
            }
        }

        public override void Process(object[] arguments)
        {
            _arguments = arguments;
            _label = (string)arguments[0];
            _arity = Int32.Parse((string)arguments[1]);
        }


        public override string ToString()
        {
            return Name() + " " + _label + "/" + _arity;
        }
    }
}
