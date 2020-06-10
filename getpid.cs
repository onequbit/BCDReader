using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Diagnostics;

namespace ProcessChild
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("please specify process to find");
                Environment.Exit(0);
            }

            Process[] processes = Process.GetProcessesByName(args[0]);            
            
            processes.ToDictionaryList<Process>(new [] {"ProcessName", "Id"}).ForEach( (d) =>
            {                
                string name = d["ProcessName"].ToString();
                string pid = d["Id"].ToString();
                Console.WriteLine($"{name} : {pid}");
            });
                       
        }

        private static Process GetParentProcess()
        {
            int iParentPid = 0;
            int iCurrentPid = Process.GetCurrentProcess().Id;

            IntPtr oHnd = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);

            if (oHnd == IntPtr.Zero)
            return null;

            PROCESSENTRY32 oProcInfo = new PROCESSENTRY32();

            oProcInfo.dwSize =
            (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(PROCESSENTRY32));

            if (Process32First(oHnd, ref oProcInfo) == false)
                return null;

            do
            {
                if (iCurrentPid == oProcInfo.th32ProcessID)
                iParentPid = (int)oProcInfo.th32ParentProcessID;
            }
            while (iParentPid == 0 && Process32Next(oHnd, ref oProcInfo));

            if (iParentPid > 0)
                return Process.GetProcessById(iParentPid);
            else
                return null;
        }

        static uint TH32CS_SNAPPROCESS = 2;

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESSENTRY32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        };

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

        [DllImport("kernel32.dll")]
        static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll")]
        static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);
    }
}

// using System;
// using System.Diagnostics;

// namespace App
// {
//     public class Program
//     {


//         public static void Main(string[] args)
//         {
//             if (args.Length == 0)
//             {
//                 Process self = Process.GetCurrentProcess();
//                 self.ShowPid();
//                 return;
//             }

//             string targetName = args[0];
//             Process[] matchingProcesses = Process.GetProcessesByName(targetName);
//             foreach(var process in matchingProcesses)
//             {
//                 process.ShowPid();
//             }
//         }
//     }

//     public static partial class ExtensionMethods
//     {
//         public static void ShowPid(this Process process)
//         {
//             Console.WriteLine($"{process.Id}");
//         }
//     }
// }