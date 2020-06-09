using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.ServiceProcess;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Library
{

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
    
    public static partial class ExtensionMethods
    {   
        private static Mutex mutex = null;

        public static void SingleInstance(this System.Windows.Forms.Form formApp)
        {
            bool createdNew;
            string appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;    
            mutex = new Mutex(true, appName, out createdNew);    
            if (!createdNew)  
            {                  
                formApp.Close();
            }
        }

        public static Icon GetCurrentIcon(this System.Windows.Forms.Form formApp)
        {
            return Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public static void Run(this System.Windows.Forms.Form app)
        {
            System.Windows.Forms.Application.Run(app);
        }        

        public static bool NullOrEmpty(this string s)
        {
            return s == null || s == "";
        }

        public static string Join(this IEnumerable<string> strArray, string sepStr = null, char sepChar = '\n')
        {
            if (sepStr == null) 
                return String.Join($"{sepChar}", strArray);
            else 
                return String.Join(sepStr, strArray);
        }

        public static string NoParens(this string str)
        {
            if (str.Length < 2) return str;            
            return str.Trim(new char[] { '"'});
        }
        
        public static bool Contains(this string[] strArray, string item)
        {
            foreach(string str in strArray)
            {   
                bool found = item.Equals(str) || item.NoParens().Equals(str);   
                if (found) return true;
            }
            return false;            
        }

        private static void showMessageWithOptions(string message, string title, bool exitOption = false)
        {
            if (exitOption)
            {
                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Cancel)
                {
                    Environment.Exit(-1);
                }
            } else {
                MessageBox.Show(message, title);
            }                  
        }

        public static void ToMessageBox(this Exception ex, string name = "", bool enableExit = true)
        {                       
            string title = ex.InnerException.ToString();
            string message = $"{ex.Message}\n{ex.StackTrace}\n{ex.Data}";
            try
            {
                string appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                EventLog myLog = new EventLog($"{appName}");
                myLog.Source = $"{appName}_Source";
                myLog.WriteEntry(message, EventLogEntryType.Error);
            }
            catch {}             
            showMessageWithOptions(message, title, true); 
            throw ex;                        
        }

        public static void ToMessageBox(this string str, string name = "", bool enableExit = true)
        {
            showMessageWithOptions(str, name, enableExit);
        }

        public static void ToMessageBox(this string[] strArray, string name = "", bool enableExit = true)
        {
            List<string> strList = new List<string>{};            
            foreach(string s in strArray)
            {
                strList.Add(s);
                if (strList.Count >= 25)
                {
                    showMessageWithOptions(strList.ToArray().Join("\n"), name, enableExit);
                    strList.Clear();                    
                }
            }
            showMessageWithOptions(strList.ToArray().Join("\n"), name, enableExit);            
        }

        public static void ToMessageBox(this Dictionary<string,string> obj, string name = "", bool enableExit = true)        
        {
            Dictionary<string,string> myDict = (Dictionary<string,string>)obj;
            List<string> items = new List<string>{};
            foreach(var x in myDict) 
            {
                items.Add($"'{x.Key}:{x.Value}'\n");
            }
            items.ToArray().ToMessageBox(name, enableExit);            
        }  

        public static string[] ToNames(this ServiceController[] services)
        {
            List<string> names = new List<string>{};
            foreach(ServiceController sc in services)
            {
                names.Add(sc.ServiceName.Trim());
            }
            return names.ToArray();
        }

        public static string ToPrompt(this string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public static List<string> ToList(this string[] strArray)
        {
            List<string> list = new List<string>();
            foreach(string s in strArray)
            {
                list.Add(s);
            }
            return list;
        }

        public static string ReadFileAsString(this string filename)
        {                           
            string fileContents = "";
            try
            {
                string localFilePath = Path.Combine(Environment.CurrentDirectory, filename);
                // new IfDebugMsg($"{localFilePath}");

                bool fileExists = File.Exists(localFilePath);
                // new IfDebugMsg($"{localFilePath} found = {fileExists}");

                using (StreamReader r = File.OpenText(localFilePath))
                {
                    fileContents = r.ReadToEnd();
                    // new IfDebugMsg($"{fileContents}");
                }                            
            }
            catch(Exception ex)
            {  
                // new IfDebugMsg($"failed to read from {filename}");          
                ex.ToMessageBox($"failed to read from {filename}");
            }

            return fileContents;        
        }
    }
}