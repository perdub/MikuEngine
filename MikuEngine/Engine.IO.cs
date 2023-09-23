#pragma warning disable IDE1006
#pragma warning disable IDE0051
#pragma warning disable IDE0044

using System;
using Jint;
using System.Linq;
using System.IO;

namespace MikuEngine
{
    //this part of engine class contains fileds and methods to save and restore engine state
    public partial class Engine
    {
        private System.IO.BinaryWriter _writer;
        private MemoryStream _writerBuffer;

        private byte[] SaveEngineState(){
            _writerBuffer?.Dispose();
            _writer?.Dispose();

            _writerBuffer = new MemoryStream();
            _writer = new BinaryWriter(_writerBuffer);

            _writer.Write(_scriptActualPosition);
            _writer.Write(_currentBackgroundResourceId);

            return _writerBuffer.ToArray();
        }
    }
}
