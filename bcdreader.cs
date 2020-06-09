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
        
        public static void Main(string[] args)
        {
            try
            {
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