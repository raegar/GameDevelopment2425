using System;

namespace SaveSystem
{
    public class FuncPredicate : IPredicate
    {
        readonly Func<bool> func;
        public FuncPredicate(Func<bool> func)
        {
            this.func = func;
        }
        public bool Evaluate()
        {
            return func.Invoke();
        }
    }
}
