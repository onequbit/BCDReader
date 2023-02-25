using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Library
{

    public static partial class Lib
    {
        public static string GetPseudoCmdOutput(string command)
        {
            string output;
            using (Process pseudoCall = new Process())
            {
                pseudoCall.StartInfo = new ProcessStartInfo()
                {
                    FileName = "pseudo.exe",
                    Arguments = command,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };
                pseudoCall.Start();
                output = pseudoCall.StandardOutput.ReadToEnd();
                pseudoCall.WaitForExit();
                if (pseudoCall.ExitCode != 0) output = "";                
            }            
            return output;
        }

        public static string GetCmdOutput(string command)
        {
            string output;            
            using (Process cmdCall = new Process())
            {
                cmdCall.StartInfo = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                cmdCall.Start();
                output = cmdCall.StandardOutput.ReadToEnd();
                cmdCall.WaitForExit();
                // if (cmdCall.ExitCode != 0) output = "";                
            }
            return output;         
        }

        public static string[] CmdWhere(string dir, string filename)
        {
            List<string> paths = new List<string>();
            string searchCmd = $"where /r {dir} {filename}";            
            string[] whereOutput = Lib.GetCmdOutput(searchCmd).Split('\n');                        
            foreach(string path in whereOutput)
            {                
                paths.Add(path.Trim());                
            }            
            return paths.ToArray();
        }

        public static string[] GetEnvPaths()
        {
            string[] envPaths = Lib.GetCmdOutput("echo %path%").Split(';');
            List<string> paths = new List<string>();            
            foreach(string path in envPaths)
            {   
                if (path.Trim().Length > 3)
                    paths.Add(path);
            }
            paths.Add(Environment.CurrentDirectory);
            envPaths = paths.ToArray();
            Array.Reverse(envPaths);
            return envPaths;                
        }

        public static bool SearchEnvPathsFor(string filename, out string foundPath)
        {   
            string[] paths = Lib.GetEnvPaths();            
            foreach(string path in paths)
            {
                try
                {                    
                    string[] found = Lib.CmdWhere(path, filename);
                    if (found.Length > 0 && found[0].Trim().Length > 3)                    
                    {                        
                        foundPath = found[0].Trim();
                        return true;
                    }                                        
                }                
                catch {}
            }            
            foundPath = "";
            return false;
        }

    }

    public class IfDebug
    {
        public IfDebug(Action debugAction)
        {
            #if DEBUG
            debugAction();
            #endif
        }

        public IfDebug(object something)
        {
            #if DEBUG   
            new IfDebugMsg(something.ToString());
            #endif
        }
    }

    public class IfDebugMsg
    {
        public IfDebugMsg(string message)
        {
            #if DEBUG            
            DialogResult result = MessageBox.Show(message, "DEBUG", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Cancel)
            {
                Environment.Exit(-1);
            }
            #endif
        }

        public IfDebugMsg(string[] messageArray)
        {
            #if DEBUG
            List<string> messageBlock = new List<string>{};
            string newMessage = "";
            foreach(string message in messageArray)
            {                    
                messageBlock.Add(message);
                if (messageBlock.Count >= 25)
                {
                    newMessage = messageBlock.ToArray().Join("\n");
                    new IfDebugMsg(newMessage);                    
                    messageBlock.Clear();
                }
            }
            newMessage = messageBlock.ToArray().Join("\n");
            new IfDebugMsg(newMessage);
            #endif
        }
    }

}