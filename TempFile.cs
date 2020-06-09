using System;
using System.IO;

namespace Library
{
    public sealed class TempFile : IDisposable
    {
        private string path;

        public TempFile()
        {
            this.path = System.IO.Path.GetTempFileName();
        }

        public TempFile(Action<string> somethingToDo)
        {
            this.path = System.IO.Path.GetTempFileName();
            using(var disposable = this)
            {
                somethingToDo(this.path);
            }
        }

        ~TempFile() 
        { 
            Dispose(false); 
        }
        
        public void Dispose() 
        { 
            Dispose(true); 
        }
        
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                GC.SuppressFinalize(this);                
            }
            if (path != null)
            {
                try { File.Delete(path); }
                catch { } // best effort
                path = null;
            }
        }
    }
}

