
using System;
using System.IO;
using System.Diagnostics;
using System.Security.Principal;
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

        private TempFile tempFile;

        public string ProgramName
        {
            get
            {
                return this.CurrentProgramName();
            }
        }

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

        private ProcessStartInfo elevateStartInfo = new ProcessStartInfo() 
        {
            FileName = new object().CurrentProgramName(),
            Arguments = string.Join(" ", Environment.GetCommandLineArgs()),            
            WorkingDirectory = Directory.GetCurrentDirectory(),
            UseShellExecute = true,
            CreateNoWindow = false,
            WindowStyle = ProcessWindowStyle.Normal,
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

        // public AdminProcess()
        // {             
            
        // }

        public AdminProcess(string commandToRun)
        {
            this.process = new Process();
            this.process.StartInfo = defaultStartInfo;
            if (commandToRun.Equals(Environment.CommandLine))
            {
                this.process.StartInfo = elevateStartInfo;     
                return;
            }
            else
                this.CommandToRun = $"/c {commandToRun}";                
        }

        // public AdminProcess(string commandToRun, Action<AdminProcess> somethingToDo) : this(commandToRun)
        // {
        //     using (var disposable = this)
        //     {
        //         this.Start();
        //         somethingToDo(disposable);
        //     }           
        // }

        public AdminProcess Start()
        {                   
            this.process.Start();
            return this;
        }

        // public static AdminProcess Start(string commandToRun)
        // {            
        //     AdminProcess newProcess = new AdminProcess(commandToRun);            
        //     newProcess.Start();
        //     return newProcess;        
        // }

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

        public string GetOutput()
        {
            string output = "";
            try
            {
                using (this.tempFile = new TempFile())                
                {
                    this.CommandToRun = $"{this.CommandToRun} > {this.tempFile.Path}";
                    process.Start();
                    process.WaitForExit();                
                    while (!process.HasExited) { } 
                    output = this.tempFile.GetContents();                
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");                
            }
            return output; 
        }

        public static bool ElevatedContext()
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return isElevated;
        }

        public static void EnsureElevatedContext()
        {
            if (AdminProcess.ElevatedContext()) return;            
            var elevated = new AdminProcess(Environment.CommandLine);
            elevated.Start();
            elevated.WaitForExit();
            Process.GetCurrentProcess().Kill();
        }
    }
}