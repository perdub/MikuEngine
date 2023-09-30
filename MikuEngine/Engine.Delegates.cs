#pragma warning disable IDE1006
#pragma warning disable IDE0051
#pragma warning disable IDE0044

using System;
using Tenray.Topaz;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MikuEngine
{
    //this part of engine class contains fileds and methods to work with delegates
    public partial class Engine
    {
        private Func<CancellationToken, Task> _waitCallback;
        private Action _clearTextFiled;
        private Action<string> _printText;
        private Action<byte[]> _saveEngineState;

        private Action<string> _setBackground;
        private Action<string, Types.Vector2> _setSprite;

        private void invokeWithPositionCheck(Action d)
        {
            _scriptActualPosition++;
            if (!inSkipMode)
            {
                d();
            }
        }

        private void invokeWithPositionCheck(Action<string> d, string param)
        {
            _scriptActualPosition++;
            if (!inSkipMode)
            {
                d(param);
            }
        }

        private void invokeWithPositionCheck(Action<byte[]> d, byte[] param)
        {
            _scriptActualPosition++;
            if (!inSkipMode)
            {
                d(param);
            }
        }

        private void printText(string text)
        {
            invokeWithPositionCheck(_printText, text);
        }

        private void clearTextFiled()
        {
            invokeWithPositionCheck(_clearTextFiled);
        }

        private void waitUserInput()
        {
            //invokeWithPositionCheck(_waitCallback);
        }

        private void saveState(){
            _saveEngineState?.Invoke(
                this.SaveEngineState()
            );
        }
    }
}
