using System;
using System.Runtime.InteropServices;
namespace Library    
{    
    public class Kernel32
    {

        [DllImport("kernel32.dll", EntryPoint = "AttachConsole")]
        public static extern bool AttachConsole(int dwProcessId);
        public const int ATTACH_PARENT_PROCESS = -1;

        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle")]
        public static extern IntPtr GetStdHandle(int nStdHandle);
        public const int STD_INPUT_HANDLE = -10;
        public const int STD_OUTPUT_HANDLE = -11;
        public const int STD_ERROR_HANDLE = -12;

        [DllImport("kernel32.dll", EntryPoint = "FreeConsole")]
        public static extern bool FreeConsole();

        [DllImport("kernel32.dll", EntryPoint = "AllocConsole")]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll", EntryPoint = "FlushConsoleInputBuffer")]        
        public static extern bool FlushConsoleInputBuffer( int hConsoleInput );
        
    }
}