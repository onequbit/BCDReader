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
        public static int Main(string[] args)
        {            
            try
            {   
                if (args.Length == 0)
                {
                    throw new ArgumentException("no file target specified");
                }
                string file = args[0];
                bool found = Lib.SearchEnvPathsFor(file, out string path);
                if (found)
                {
                    Console.WriteLine(path);                    
                }
                else
                {
                    throw new FileNotFoundException($"'{file}' not found");                 
                }                
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"{ex.Message}");
                return -1;
            }
            return 0;                        
        }
    }
}