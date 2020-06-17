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
        
        public static void ParseBcdLines(string data)
        {
            int count = 0;
            foreach(string line in data.Split('\n'))
            {
                string[] parts = line.Split();
                if (parts.Length == 1)
                {
                    Console.WriteLine($"{count}# *{line}*");
                }
                else
                {
                    (string key, string[] rest) = parts.Pop();
                    string value = rest.Join(" ").Trim();
                    Console.WriteLine($"{count}# '{key}':'{value}'");
                }
                count++;
            }
        }

        [STAThread] // required for Windows Forms
        public static void Main(string[] args)
        {            
            try
            {   
                // this part is finally working...
                // Lib.CmdWhere(@"Z:\OneDrive\Dev\Pseudo\","pseudo.exe").ToConsole();
                string path = Lib.SearchEnvPathsFor("Pseudo.exe");
                Console.WriteLine(path);
                // string[] pseudoExe = Lib.GetCmdOutput("where /r c: pseudo.exe").Split('\n');
                // Console.WriteLine($"pseudoExe: {pseudoExe}");
                // string bcdInfo = Lib.GetPseudoCmdOutput("bcdedit");
                // ParseBcdLines(bcdInfo);
                
                
                // Console.WriteLine(bcdInfo);                
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");                
            }                        
        }
    }
}///