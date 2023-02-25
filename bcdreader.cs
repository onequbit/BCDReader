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
                
                var objs = BCDInfo.Data;

                Console.WriteLine($"{objs.Count} objects parsed");
                foreach(var obj in objs)
                {
                    Console.WriteLine(obj);
                }
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");                
            }                        
        }
    }
}