
using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Library
{
    public class ConsoleAttached
    {                       

        public ConsoleAttached()
        {
            new IfDebug( () =>
            {
                string message = $"Environment.UserInteractive: {Environment.UserInteractive}";
                MessageBox.Show(message);
                new Notifier(message);     
            });
            
            Kernel32.AttachConsole(Kernel32.ATTACH_PARENT_PROCESS);            
        }
        

        public static void Detach()
        {                   
            Kernel32.FreeConsole();
        }
    }
}