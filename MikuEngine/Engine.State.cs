#pragma warning disable IDE1006
#pragma warning disable IDE0051
#pragma warning disable IDE0044

using System;
using Jint;
using System.Linq;
using System.IO;

namespace MikuEngine
{
    //this part of engine class contains fileds and methods who represend surrent engine state
    public partial class Engine
    {
        private int _scriptCalculatedPosition = 0;
        private int _scriptActualPosition = 0;
        private bool inSkipMode = false;

        private string _currentBackgroundResourceId = string.Empty;
    }
}
