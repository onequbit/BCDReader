using Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace App
{
    
    public class Program
    {
        [STAThread] // required for Windows Forms
        public static void Main(string[] args)
        {
            try
            {
                new ConsoleAttached();
                AdminProcess.EnsureElevatedContext();
                
                string output = AdminProcess.GetOutput("bcdedit");                
                Console.WriteLine(output);                                                      
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");                
            }                        
        }
    }
}