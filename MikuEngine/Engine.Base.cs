#pragma warning disable IDE1006
#pragma warning disable IDE0051
#pragma warning disable IDE0044
#pragma warning disable CS0121

using System;
using Tenray.Topaz;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

namespace MikuEngine
{
    public partial class Engine : IDisposable
    {
        private TopazEngine _engine;

        private bool _waitInput = false;

        private async Task<int> waitInput()
        {
            _waitInput = true;
            while(_waitInput)
            {
                await Task.Delay(WAIT_FOR_USER_INPUT_DELAY);
            }
            return 1;
        }

        public void EmulateUserInput(){
            _waitInput = false;
        }

        public Engine(
            Action<string> printText,   //delegate to print text 
            Func<CancellationToken, Task> waitCallback,        //delegate to wait user click before continue
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

            _engine = new TopazEngine();

            //map delegates with js methods
            _engine.SetValue("print", (string text) =>
            {
                this.printText(text);
            });

            _engine.SetValue("wait", async ()=>{
                await waitInput();
                return 1;
            });

            _engine.SetValue("clean", () =>
            {
                this.clearTextFiled();
            });

            //create a high-level methods to invoke a complex methods
            _engine.SetValue("say", (string text) =>
            {
                this.printText(text);
                this.waitUserInput();
                this.clearTextFiled();
            });

            //set backround
            _engine.SetValue("background", (string res) =>
            {
                _currentBackgroundResourceId = res;
                _setBackground(res);
            });

            //function to wait some milliseconds
            _engine.SetValue("sleep", async (int time) =>
            {
                await Task.Delay(time);
                return 1;
            });

            _engine.SetValue("SAVE_STATE", (Action)saveState);
        }

        public virtual void ExecuteScript(string script)
        {
            ExecuteScriptAsync(script).Wait();
        }

        public async virtual Task ExecuteScriptAsync(string script)
        {
            await _engine.ExecuteScriptAsync(script);
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
        const int WAIT_FOR_USER_INPUT_DELAY = 100;
    }
}
