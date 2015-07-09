using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StatementArchitecture
{
    public interface IStatement
    {
        void Generate();

    }

    public abstract class MixableStatement:IStatement
    {
        public abstract void Generate();
        public static MixableStatement operator +(MixableStatement a, MixableStatement b)
        {
            return new Composite(a, b);
        }
    }

    public class Composite : MixableStatement
    {
        private readonly IStatement _a;
        private readonly IStatement _b;
        public Composite(IStatement a, IStatement b)
        {
            _a = a;
            _b = b;
        }

        public override void Generate()
        {
            _a.Generate();
            _b.Generate();
        }
    }
}
