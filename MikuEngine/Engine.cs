using System;
using Jint;

namespace MikuEngine
{
    public class Engine
    {
        private Jint.Engine _engine;

        public Engine(Action<string> printText, Action waitCallback)
        {
            _engine = new Jint.Engine();
            _engine.SetValue("print", printText);
            _engine.SetValue("wait", waitCallback);
        }

        public virtual void ExecuteScript(string script){
            _engine.Execute(script);
        }
    }
}
