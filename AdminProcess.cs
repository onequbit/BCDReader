
using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Library
{
    public class AdminProcess : IDisposable
    {
        // This class will enable executing a command with elevated privileges.
        // Standard Input and Ouput and Error are unavailable from this process,
        // so attempting to run a command that is expecting input may result in
        // unexpected behavior.
        
        private Process process;        

        private ProcessStartInfo defaultStartInfo = new ProcessStartInfo() 
        {
            FileName = "cmd.exe",
            Arguments = null,            
            WorkingDirectory = Directory.GetCurrentDirectory(),
            UseShellExecute = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            Verb = "runas"
        };

        public string CommandToRun 
        { 
            get
            {
                return this.defaultStartInfo.Arguments;
            }
            set 
            {   
                this.defaultStartInfo.Arguments = value;
            }
        }

        public bool HasExited
        {
            get
            {
                return this.process.HasExited;
            }
        }

        public AdminProcess()
        { 
            this.process = new Process();
            this.process.StartInfo = defaultStartInfo;
        }

        public AdminProcess(string commandToRun) : this()
        {
            this.CommandToRun = $"/c {commandToRun}";            
        }

        public AdminProcess(string commandToRun, Action<AdminProcess> somethingToDo) : this(commandToRun)
        {
            using (var disposable = this)
            {
                this.Start();
                somethingToDo(disposable);
            }           
        }

        public AdminProcess Start()
        {
            if (this.CommandToRun == null)
            {
                throw new ArgumentNullException("CommandToRun is not set");
            }            
            this.process.Start();
            return this;
        }

        public static AdminProcess Start(string commandToRun)
        {
            AdminProcess newProcess = new AdminProcess(commandToRun);
            newProcess.Start();
            return newProcess;        
        }

        public void WaitForExit()
        {
            this.process.WaitForExit();
        }

        ~AdminProcess() 
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
            this.process.Dispose();
        }

        public static string GetOutput(string commandToRun)
        {            
            string output = "";
            try
            {
                new TempFile( (tempfile) => 
                {
                    new AdminProcess( $"{commandToRun} > {tempfile}", (process) =>
                    {
                        process.WaitForExit();                
                        while (!process.HasExited) { } 
                    });                    
                    output = tempfile.ReadFileAsString();                    
                });               
            }
            catch (Exception ex)
            {                
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");                
            }
            return output;        
        }
    }
}