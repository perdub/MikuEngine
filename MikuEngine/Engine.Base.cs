#pragma warning disable IDE1006
#pragma warning disable IDE0051
#pragma warning disable IDE0044
#pragma warning disable CS0121

using System;
using Jint;
using System.Linq;

namespace MikuEngine
{
    public partial class Engine : IDisposable
    {
        private Jint.Engine _engine;

        public Engine(
            Action<string> printText,   //delegate to print text 
            Action waitCallback,        //delegate to wait user click before continue
            Action clearTextFiled,       //delegate to clear text filed before print next text
            
            Action<byte[]> saveEngineState,  //delegate to save engine state

            Action<string> setBackground
        )
        {
            _waitCallback = waitCallback;
            _clearTextFiled = clearTextFiled;
            _printText = printText;
            _saveEngineState = saveEngineState;
            _setBackground = setBackground;

            _engine = new Jint.Engine();

            //map delegates with js methods
            _engine.SetValue("print", (Action<string>)((text)=>{
                this.printText(text);
            }));

            _engine.SetValue("wait", (Action)this.waitUserInput);
            _engine.SetValue("clean", (Action)this.clearTextFiled);

            //create a high-level methods to invoke a complex methods
            _engine.SetValue("say", (Action<string>)((text) =>
            {
                this.printText(text);
                this.waitUserInput();
                this.clearTextFiled();
            }));

            //set backround
            _engine.SetValue("background", (Action<string>)((res)=>{
                _currentBackgroundResourceId = res;
                _setBackground(res);
            }));

            _engine.SetValue("SAVE_STATE", (Action)saveState);
        }

        public virtual void ExecuteScript(string script)
        {
            _engine.Execute(script);
        }
        /*
        public virtual void SaveEngineState(){
            var properties = _engine.Realm.GlobalObject.GetOwnProperties().Skip(SYSTEM_PROPERTIES_ENGINE_COUNT);
            foreach(var prop in properties){
                var jsv = prop.Value;
            }
        }//*/

        public void Dispose()
        {
            this._writer.Dispose();
        }

        const int SYSTEM_PROPERTIES_ENGINE_COUNT = 62;
    }
}
