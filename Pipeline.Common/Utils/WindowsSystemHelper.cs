using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;

namespace Pipeline.Common.Utils
{
    class WindowsSystemHelper
    {

        #region Win32 Send Input structs

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646270(v=vs.85).aspx
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct INPUT
        {
            public uint Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        /// <summary>
        /// http://social.msdn.microsoft.com/Forums/en/csharplanguage/thread/f0e82d6e-4999-4d22-b3d3-32b25f61fb2a
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public HARDWAREINPUT Hardware;
            [FieldOffset(0)]
            public KEYBDINPUT Keyboard;
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646310(v=vs.85).aspx
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct HARDWAREINPUT
        {
            public uint Msg;
            public ushort ParamL;
            public ushort ParamH;
        }

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646310(v=vs.85).aspx
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            public ushort Vk;
            public ushort Scan;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        /// <summary>
        /// http://social.msdn.microsoft.com/forums/en-US/netfxbcl/thread/2abc6be8-c593-4686-93d2-89785232dacd
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            public int X;
            public int Y;
            public uint MouseData;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        public enum KeyCode : ushort
        {
            #region Media

            /// <summary>
            /// Next track if a song is playing
            /// </summary>
            MEDIA_NEXT_TRACK = 0xb0,

            /// <summary>
            /// Play pause
            /// </summary>
            MEDIA_PLAY_PAUSE = 0xb3,

            /// <summary>
            /// Previous track
            /// </summary>
            MEDIA_PREV_TRACK = 0xb1,

            /// <summary>
            /// Stop
            /// </summary>
            MEDIA_STOP = 0xb2,

            #endregion

            #region math

            /// <summary>Key "+"</summary>
            ADD = 0x6b,
            /// <summary>
            /// "*" key
            /// </summary>
            MULTIPLY = 0x6a,

            /// <summary>
            /// "/" key
            /// </summary>
            DIVIDE = 0x6f,

            /// <summary>
            /// Subtract key "-"
            /// </summary>
            SUBTRACT = 0x6d,

            #endregion

            #region Browser
            /// <summary>
            /// Go Back
            /// </summary>
            BROWSER_BACK = 0xa6,
            /// <summary>
            /// Favorites
            /// </summary>
            BROWSER_FAVORITES = 0xab,
            /// <summary>
            /// Forward
            /// </summary>
            BROWSER_FORWARD = 0xa7,
            /// <summary>
            /// Home
            /// </summary>
            BROWSER_HOME = 0xac,
            /// <summary>
            /// Refresh
            /// </summary>
            BROWSER_REFRESH = 0xa8,
            /// <summary>
            /// browser search
            /// </summary>
            BROWSER_SEARCH = 170,
            /// <summary>
            /// Stop
            /// </summary>
            BROWSER_STOP = 0xa9,
            #endregion

            #region Numpad numbers
            /// <summary>
            /// 
            /// </summary>
            NUMPAD0 = 0x60,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD1 = 0x61,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD2 = 0x62,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD3 = 0x63,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD4 = 100,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD5 = 0x65,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD6 = 0x66,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD7 = 0x67,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD8 = 0x68,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD9 = 0x69,

            #endregion

            #region Fkeys
            /// <summary>
            /// F1
            /// </summary>
            F1 = 0x70,
            /// <summary>
            /// F10
            /// </summary>
            F10 = 0x79,
            /// <summary>
            /// 
            /// </summary>
            F11 = 0x7a,
            /// <summary>
            /// 
            /// </summary>
            F12 = 0x7b,
            /// <summary>
            /// 
            /// </summary>
            F13 = 0x7c,
            /// <summary>
            /// 
            /// </summary>
            F14 = 0x7d,
            /// <summary>
            /// 
            /// </summary>
            F15 = 0x7e,
            /// <summary>
            /// 
            /// </summary>
            F16 = 0x7f,
            /// <summary>
            /// 
            /// </summary>
            F17 = 0x80,
            /// <summary>
            /// 
            /// </summary>
            F18 = 0x81,
            /// <summary>
            /// 
            /// </summary>
            F19 = 130,
            /// <summary>
            /// 
            /// </summary>
            F2 = 0x71,
            /// <summary>
            /// 
            /// </summary>
            F20 = 0x83,
            /// <summary>
            /// 
            /// </summary>
            F21 = 0x84,
            /// <summary>
            /// 
            /// </summary>
            F22 = 0x85,
            /// <summary>
            /// 
            /// </summary>
            F23 = 0x86,
            /// <summary>
            /// 
            /// </summary>
            F24 = 0x87,
            /// <summary>
            /// 
            /// </summary>
            F3 = 0x72,
            /// <summary>
            /// 
            /// </summary>
            F4 = 0x73,
            /// <summary>
            /// 
            /// </summary>
            F5 = 0x74,
            /// <summary>
            /// 
            /// </summary>
            F6 = 0x75,
            /// <summary>
            /// 
            /// </summary>
            F7 = 0x76,
            /// <summary>
            /// 
            /// </summary>
            F8 = 0x77,
            /// <summary>
            /// 
            /// </summary>
            F9 = 120,

            #endregion

            #region Other
            /// <summary>
            /// 
            /// </summary>
            OEM_1 = 0xba,
            /// <summary>
            /// 
            /// </summary>
            OEM_102 = 0xe2,
            /// <summary>
            /// 
            /// </summary>
            OEM_2 = 0xbf,
            /// <summary>
            /// 
            /// </summary>
            OEM_3 = 0xc0,
            /// <summary>
            /// 
            /// </summary>
            OEM_4 = 0xdb,
            /// <summary>
            /// 
            /// </summary>
            OEM_5 = 220,
            /// <summary>
            /// 
            /// </summary>
            OEM_6 = 0xdd,
            /// <summary>
            /// 
            /// </summary>
            OEM_7 = 0xde,
            /// <summary>
            /// 
            /// </summary>
            OEM_8 = 0xdf,
            /// <summary>
            /// 
            /// </summary>
            OEM_CLEAR = 0xfe,
            /// <summary>
            /// 
            /// </summary>
            OEM_COMMA = 0xbc,
            /// <summary>
            /// 
            /// </summary>
            OEM_MINUS = 0xbd,
            /// <summary>
            /// 
            /// </summary>
            OEM_PERIOD = 190,
            /// <summary>
            /// 
            /// </summary>
            OEM_PLUS = 0xbb,

            #endregion

            #region KEYS

            /// <summary>
            /// 
            /// </summary>
            KEY_0 = 0x30,
            /// <summary>
            /// 
            /// </summary>
            KEY_1 = 0x31,
            /// <summary>
            /// 
            /// </summary>
            KEY_2 = 50,
            /// <summary>
            /// 
            /// </summary>
            KEY_3 = 0x33,
            /// <summary>
            /// 
            /// </summary>
            KEY_4 = 0x34,
            /// <summary>
            /// 
            /// </summary>
            KEY_5 = 0x35,
            /// <summary>
            /// 
            /// </summary>
            KEY_6 = 0x36,
            /// <summary>
            /// 
            /// </summary>
            KEY_7 = 0x37,
            /// <summary>
            /// 
            /// </summary>
            KEY_8 = 0x38,
            /// <summary>
            /// 
            /// </summary>
            KEY_9 = 0x39,
            /// <summary>
            /// 
            /// </summary>
            KEY_A = 0x41,
            /// <summary>
            /// 
            /// </summary>
            KEY_B = 0x42,
            /// <summary>
            /// 
            /// </summary>
            KEY_C = 0x43,
            /// <summary>
            /// 
            /// </summary>
            KEY_D = 0x44,
            /// <summary>
            /// 
            /// </summary>
            KEY_E = 0x45,
            /// <summary>
            /// 
            /// </summary>
            KEY_F = 70,
            /// <summary>
            /// 
            /// </summary>
            KEY_G = 0x47,
            /// <summary>
            /// 
            /// </summary>
            KEY_H = 0x48,
            /// <summary>
            /// 
            /// </summary>
            KEY_I = 0x49,
            /// <summary>
            /// 
            /// </summary>
            KEY_J = 0x4a,
            /// <summary>
            /// 
            /// </summary>
            KEY_K = 0x4b,
            /// <summary>
            /// 
            /// </summary>
            KEY_L = 0x4c,
            /// <summary>
            /// 
            /// </summary>
            KEY_M = 0x4d,
            /// <summary>
            /// 
            /// </summary>
            KEY_N = 0x4e,
            /// <summary>
            /// 
            /// </summary>
            KEY_O = 0x4f,
            /// <summary>
            /// 
            /// </summary>
            KEY_P = 80,
            /// <summary>
            /// 
            /// </summary>
            KEY_Q = 0x51,
            /// <summary>
            /// 
            /// </summary>
            KEY_R = 0x52,
            /// <summary>
            /// 
            /// </summary>
            KEY_S = 0x53,
            /// <summary>
            /// 
            /// </summary>
            KEY_T = 0x54,
            /// <summary>
            /// 
            /// </summary>
            KEY_U = 0x55,
            /// <summary>
            /// 
            /// </summary>
            KEY_V = 0x56,
            /// <summary>
            /// 
            /// </summary>
            KEY_W = 0x57,
            /// <summary>
            /// 
            /// </summary>
            KEY_X = 0x58,
            /// <summary>
            /// 
            /// </summary>
            KEY_Y = 0x59,
            /// <summary>
            /// 
            /// </summary>
            KEY_Z = 90,

            #endregion

            #region volume
            /// <summary>
            /// Decrese volume
            /// </summary>
            VOLUME_DOWN = 0xae,

            /// <summary>
            /// Mute volume
            /// </summary>
            VOLUME_MUTE = 0xad,

            /// <summary>
            /// Increase volue
            /// </summary>
            VOLUME_UP = 0xaf,

            #endregion


            /// <summary>
            /// Take snapshot of the screen and place it on the clipboard
            /// </summary>
            SNAPSHOT = 0x2c,

            /// <summary>Send right click from keyboard "key that is 2 keys to the right of space bar"</summary>
            RightClick = 0x5d,

            /// <summary>
            /// Go Back or delete
            /// </summary>
            BACKSPACE = 8,

            /// <summary>
            /// Control + Break "When debuging if you step into an infinite loop this will stop debug"
            /// </summary>
            CANCEL = 3,
            /// <summary>
            /// Caps lock key to send cappital letters
            /// </summary>
            CAPS_LOCK = 20,
            /// <summary>
            /// Ctlr key
            /// </summary>
            CONTROL = 0x11,

            /// <summary>
            /// Alt key
            /// </summary>
            ALT = 18,

            /// <summary>
            /// "." key
            /// </summary>
            DECIMAL = 110,

            /// <summary>
            /// Delete Key
            /// </summary>
            DELETE = 0x2e,


            /// <summary>
            /// Arrow down key
            /// </summary>
            DOWN = 40,

            /// <summary>
            /// End key
            /// </summary>
            END = 0x23,

            /// <summary>
            /// Escape key
            /// </summary>
            ESC = 0x1b,

            /// <summary>
            /// Home key
            /// </summary>
            HOME = 0x24,

            /// <summary>
            /// Insert key
            /// </summary>
            INSERT = 0x2d,

            /// <summary>
            /// Open my computer
            /// </summary>
            LAUNCH_APP1 = 0xb6,
            /// <summary>
            /// Open calculator
            /// </summary>
            LAUNCH_APP2 = 0xb7,

            /// <summary>
            /// Open default email in my case outlook
            /// </summary>
            LAUNCH_MAIL = 180,

            /// <summary>
            /// Opend default media player (itunes, winmediaplayer, etc)
            /// </summary>
            LAUNCH_MEDIA_SELECT = 0xb5,

            /// <summary>
            /// Left control
            /// </summary>
            LCONTROL = 0xa2,

            /// <summary>
            /// Left arrow
            /// </summary>
            LEFT = 0x25,

            /// <summary>
            /// Left shift
            /// </summary>
            LSHIFT = 160,

            /// <summary>
            /// left windows key
            /// </summary>
            LWIN = 0x5b,


            /// <summary>
            /// Next "page down"
            /// </summary>
            PAGEDOWN = 0x22,

            /// <summary>
            /// Num lock to enable typing numbers
            /// </summary>
            NUMLOCK = 0x90,

            /// <summary>
            /// Page up key
            /// </summary>
            PAGE_UP = 0x21,

            /// <summary>
            /// Right control
            /// </summary>
            RCONTROL = 0xa3,

            /// <summary>
            /// Return key
            /// </summary>
            ENTER = 13,

            /// <summary>
            /// Right arrow key
            /// </summary>
            RIGHT = 0x27,

            /// <summary>
            /// Right shift
            /// </summary>
            RSHIFT = 0xa1,

            /// <summary>
            /// Right windows key
            /// </summary>
            RWIN = 0x5c,

            /// <summary>
            /// Shift key
            /// </summary>
            SHIFT = 0x10,

            /// <summary>
            /// Space back key
            /// </summary>
            SPACE_BAR = 0x20,

            /// <summary>
            /// Tab key
            /// </summary>
            TAB = 9,

            /// <summary>
            /// Up arrow key
            /// </summary>
            UP = 0x26,

        }

        #endregion

        #region Native Library 

        //Function mapping to user32
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int abc);

        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", SetLastError = true)]
        static extern IntPtr CreateCompatibleDC([In] IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        static extern IntPtr CreateCompatibleBitmap([In] IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern bool DeleteDC([In] IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        [DllImport("kernel32.dll", EntryPoint = "CloseHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool CloseHandle(IntPtr handle);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool CreateProcessAsUser(IntPtr tokenHandle, string applicationName, string commandLine, IntPtr processAttributes, IntPtr threadAttributes, bool inheritHandle, int creationFlags, IntPtr envrionment, string currentDirectory, ref STARTUPINFO startupInfo, ref PROCESS_INFORMATION processInformation);

        [DllImport("Kernel32.dll", EntryPoint = "WTSGetActiveConsoleSessionId")]
        internal static extern int WTSGetActiveConsoleSessionId();

        [DllImport("WtsApi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool WTSQueryUserToken(int SessionId, out IntPtr phToken);

        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr processHandle;
            public IntPtr threadHandle;
            public int processID;
            public int threadID;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct STARTUPINFO
        {
            public int length;
            public string reserved;
            public string desktop;
            public string title;
            public int x;
            public int y;
            public int width;
            public int height;
            public int consoleColumns;
            public int consoleRows;
            public int consoleFillAttribute;
            public int flags;
            public short showWindow;
            public short reserverd2;
            public IntPtr reserved3;
            public IntPtr stdInputHandle;
            public IntPtr stdOutputHandle;
            public IntPtr stdErrorHandle;
        }

        enum TernaryRasterOperations : uint
        {
            /// <summary>dest = source</summary>
            SRCCOPY = 0x00CC0020,
            /// <summary>dest = source OR dest</summary>
            SRCPAINT = 0x00EE0086,
            /// <summary>dest = source AND dest</summary>
            SRCAND = 0x008800C6,
            /// <summary>dest = source XOR dest</summary>
            SRCINVERT = 0x00660046,
            /// <summary>dest = source AND (NOT dest)</summary>
            SRCERASE = 0x00440328,
            /// <summary>dest = (NOT source)</summary>
            NOTSRCCOPY = 0x00330008,
            /// <summary>dest = (NOT src) AND (NOT dest)</summary>
            NOTSRCERASE = 0x001100A6,
            /// <summary>dest = (source AND pattern)</summary>
            MERGECOPY = 0x00C000CA,
            /// <summary>dest = (NOT source) OR dest</summary>
            MERGEPAINT = 0x00BB0226,
            /// <summary>dest = pattern</summary>
            PATCOPY = 0x00F00021,
            /// <summary>dest = DPSnoo</summary>
            PATPAINT = 0x00FB0A09,
            /// <summary>dest = pattern XOR dest</summary>
            PATINVERT = 0x005A0049,
            /// <summary>dest = (NOT dest)</summary>
            DSTINVERT = 0x00550009,
            /// <summary>dest = BLACK</summary>
            BLACKNESS = 0x00000042,
            /// <summary>dest = WHITE</summary>
            WHITENESS = 0x00FF0062,
            /// <summary>
            /// Capture window as seen on screen.  This includes layered windows
            /// such as WPF windows with AllowsTransparency="true"
            /// </summary>
            CAPTUREBLT = 0x40000000
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public struct SIZE
        {
            public int cx;
            public int cy;
        }

        #endregion

        #region SendInput helper functions 

        /// <summary>
        /// simulate key press
        /// </summary>
        /// <param name="keyCode"></param>
        public static void SendKeyPress(KeyCode keyCode)
        {
            INPUT input = new INPUT
            {
                Type = 1
            };
            input.Data.Keyboard = new KEYBDINPUT()
            {
                Vk = (ushort)keyCode,
                Scan = 0,
                Flags = 0,
                Time = 0,
                ExtraInfo = IntPtr.Zero,
            };

            INPUT input2 = new INPUT
            {
                Type = 1
            };
            input2.Data.Keyboard = new KEYBDINPUT()
            {
                Vk = (ushort)keyCode,
                Scan = 0,
                Flags = 2,
                Time = 0,
                ExtraInfo = IntPtr.Zero
            };
            INPUT[] inputs = new INPUT[] { input, input2 };
            if (SendInput(2, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Exception();
        }

        /// <summary>
        /// Send a key down and hold it down until sendkeyup method is called
        /// </summary>
        /// <param name="keyCode"></param>
        public static void SendKeyDown(KeyCode keyCode)
        {
            INPUT input = new INPUT
            {
                Type = 1
            };
            input.Data.Keyboard = new KEYBDINPUT();
            input.Data.Keyboard.Vk = (ushort)keyCode;
            input.Data.Keyboard.Scan = 0;
            input.Data.Keyboard.Flags = 0;
            input.Data.Keyboard.Time = 0;
            input.Data.Keyboard.ExtraInfo = IntPtr.Zero;
            INPUT[] inputs = new INPUT[] { input };
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Release a key that is being hold down
        /// </summary>
        /// <param name="keyCode"></param>
        public static void SendKeyUp(KeyCode keyCode)
        {
            INPUT input = new INPUT
            {
                Type = 1
            };
            input.Data.Keyboard = new KEYBDINPUT();
            input.Data.Keyboard.Vk = (ushort)keyCode;
            input.Data.Keyboard.Scan = 0;
            input.Data.Keyboard.Flags = 2;
            input.Data.Keyboard.Time = 0;
            input.Data.Keyboard.ExtraInfo = IntPtr.Zero;
            INPUT[] inputs = new INPUT[] { input };
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Exception();

        }

        #endregion

        #region Command line functions


        internal static class SessionFinder
        {
            private const int INT_ConsoleSession = -1;

            internal static IntPtr GetLocalInteractiveSession()
            {
                IntPtr tokenHandle = IntPtr.Zero;
                int sessionID = WindowsSystemHelper.WTSGetActiveConsoleSessionId();
                if (sessionID != INT_ConsoleSession)
                {
                    if (!WindowsSystemHelper.WTSQueryUserToken(sessionID, out tokenHandle))
                    {
                        throw new System.ComponentModel.Win32Exception();
                    }
                }
                return tokenHandle;
            }
        }

        internal static void StartProcessAsUser(string executablePath, string commandline, string workingDirectory, IntPtr sessionTokenHandle)
        {
            var processInformation = new WindowsSystemHelper.PROCESS_INFORMATION();
            try
            {
                var startupInformation = new WindowsSystemHelper.STARTUPINFO();
                startupInformation.length = Marshal.SizeOf(startupInformation);
                startupInformation.desktop = string.Empty;
                bool result = WindowsSystemHelper.CreateProcessAsUser
                (
                    sessionTokenHandle,
                    executablePath,
                    commandline,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    false,
                    0,
                    IntPtr.Zero,
                    workingDirectory,
                    ref startupInformation,
                    ref processInformation
                );
                if (!result)
                {
                    int error = Marshal.GetLastWin32Error();
                    string message = string.Format("CreateProcessAsUser Error: {0}", error);
                    throw new ApplicationException(message);
                }
            }
            finally
            {
                if (processInformation.processHandle != IntPtr.Zero)
                {
                    WindowsSystemHelper.CloseHandle(processInformation.processHandle);
                }
                if (processInformation.threadHandle != IntPtr.Zero)
                {
                    WindowsSystemHelper.CloseHandle(processInformation.threadHandle);
                }
                if (sessionTokenHandle != IntPtr.Zero)
                {
                    WindowsSystemHelper.CloseHandle(sessionTokenHandle);
                }
            }
        }

        public static string GetBeyondCompareReport(string export_name, string original_file, string newer_file)
        {
            string bc_exe = "BCompare";
            string com_exe = "BComp";

            //Clean up any possible existing BCompare instances
            WindowsSystemHelper.KillProcesses(bc_exe);
            WindowsSystemHelper.KillProcesses(com_exe);

            //Using BeyondCompare executable (GUI mode) in order to capture the visual result of the comparsion- no return value can be captured from the process so this is purely cosmetic
            string exe_cmd = $"\"C:\\Program Files\\Beyond Compare 4\\{bc_exe}.exe\"";

            string bc_report = $"{BaseValues.PathReportFile}{export_name}_BCReport.html";
            string script_path = $"{BaseValues.PathReportFile}{export_name}_BCScript.txt";
            string script_cmd = $"\"@{script_path}\"";
            string cmd_report_type = (Path.GetExtension(original_file).Contains(TableType.XML.ToString().ToLower())) ? "text-report" : "file-report";

            //Files as arguments to be used in command
            string report_script = $"option confirm:yes-to-all\n{cmd_report_type} layout:side-by-side &\n options:ignore-unimportant &\n output-to:\"{bc_report}\" output-options:html-color \"{original_file}\" \"{newer_file}\"";

            File.WriteAllText(script_path, report_script);

            Thread.Sleep(2500);

            //Launch BeyondCompare GUI window as maximized
            IntPtr app_handle = LaunchCommandLineApp(bc_exe, exe_cmd, script_cmd);

            Thread.Sleep(2500);

            //Clean up processes before returning the screenshot image path
            WindowsSystemHelper.KillProcesses(bc_exe);

            return bc_report;
        }

        /// <summary>
        /// Launch BeyondCompare with specific arguments to diff two files of the same type and return a path to a fullscreen screenshot of the GUI. 
        /// If you wish to get the comparison's result value instead of a screenshot of the GUI, then set the 'headless' argument to true.
        /// </summary>
        /// <param name="original_file">The original file for comparison</param>
        /// <param name="newer_file">The more recent file</param>
        /// <param name="headless">When true, returns the exit-code of running a headless comparison</param>
        public static string GetBeyondCompareResult(string original_file, string newer_file, bool headless = false)
        {
            string bc_exe = "BCompare";
            string com_exe = "BComp";

            //Clean up any possible existing BCompare instances
            WindowsSystemHelper.KillProcesses(bc_exe);
            WindowsSystemHelper.KillProcesses(com_exe);

            if (headless == true)
            {
                //Path to Beyond Compare's command script
                string com_path = $"\"C:\\Program Files\\Beyond Compare 4\\{com_exe}.com\"";

                //Using '/qc' to perform a headless quick compare (process exit code returned is the result value of the comparison, eg. 1 = no differences between files)
                string cmd_args = $"/qc /iu \"{original_file}\" \"{newer_file}\"";

                return WindowsSystemHelper.GetCommandLineResult(com_exe, com_path, cmd_args).ToString();
            }
            else
            {
                string image_path = string.Empty;

                //Using BeyondCompare executable (GUI mode) in order to capture the visual result of the comparsion- no return value can be captured from the process so this is purely cosmetic
                string exe_path = $"\"C:\\Program Files\\Beyond Compare 4\\{bc_exe}.exe\" ";

                //Files as arguments to be used in command
                string bc_command_args = $"/iu \"{original_file}\" \"{newer_file}\"";

                //Launch BeyondCompare GUI window as maximized
                IntPtr app_handle = WindowsSystemHelper.LaunchCommandLineApp(bc_exe, exe_path, bc_command_args, true);

                //image_path = UtilsHelper.CaptureFullScreen();
                Image app_image = WindowsSystemHelper.CaptureWindow(app_handle);
                bool exists = Directory.Exists($"{BaseValues.PathReportFile}ScreenShots");
                if (!exists)
                    Directory.CreateDirectory($"{BaseValues.PathReportFile}ScreenShots");

                image_path = $@"{BaseValues.PathReportFile}ScreenShots\BCOMPARE__{DateTime.Now:hh-mm-ssff}.png";
                app_image.Save(image_path, System.Drawing.Imaging.ImageFormat.Png);

                Thread.Sleep(2500);

                //Clean up processes before returning the screenshot image path
                WindowsSystemHelper.KillProcesses(bc_exe);

                return image_path;
            }
        }

        internal static void ExecuteBCompareNative(string original_file, string newer_file)
        {
            string bc_exe = "BCompare";
            string exe_path = $"C:\\Program Files\\Beyond Compare 4\\{bc_exe}.exe /silent";
            string working_dir = BaseValues.PathReportFile;
            string bc_command_args = $"/iu \"{original_file}\" \"{newer_file}\"";
            string full_command = $"\"{exe_path}\" {bc_command_args}";

            IntPtr sessionTokenHandle = IntPtr.Zero;
            try
            {
                sessionTokenHandle = SessionFinder.GetLocalInteractiveSession();
                if (sessionTokenHandle != IntPtr.Zero)
                {
                    WindowsSystemHelper.StartProcessAsUser(exe_path, full_command, working_dir, sessionTokenHandle);
                }
            }
            catch
            {
                //What are we gonna do?
            }
            finally
            {
                if (sessionTokenHandle != IntPtr.Zero)
                {
                    WindowsSystemHelper.CloseHandle(sessionTokenHandle);
                }
            }
        }


        public static Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window 
            IntPtr hdcSrc = WindowsSystemHelper.GetWindowDC(handle);
            // get the size 
            WindowsSystemHelper.RECT windowRect = new WindowsSystemHelper.RECT();
            WindowsSystemHelper.GetWindowRect(handle, out windowRect);

            int width = windowRect.Right - windowRect.Left;
            int height = windowRect.Bottom - windowRect.Top;

            // create a device context we can copy to 
            IntPtr hdcDest = WindowsSystemHelper.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to, 
            // using GetDeviceCaps to get the width/height 
            IntPtr hBitmap = WindowsSystemHelper.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object 
            IntPtr hOld = WindowsSystemHelper.SelectObject(hdcDest, hBitmap);
            // bitblt over 
            WindowsSystemHelper.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, WindowsSystemHelper.TernaryRasterOperations.SRCCOPY);
            // restore selection 
            WindowsSystemHelper.SelectObject(hdcDest, hOld);
            // clean up  
            WindowsSystemHelper.DeleteDC(hdcDest);
            WindowsSystemHelper.ReleaseDC(handle, hdcSrc);

            // get a .NET image object for it 
            Image img = Image.FromHbitmap(hBitmap);

            // free up the Bitmap object 
            WindowsSystemHelper.DeleteObject(hBitmap);

            return img;
        }

        /// <summary>
        /// Kills all instances of a process that match the provided executable name (friendly process name without file extension)
        /// For each process instance found it will first kill all child processes recursively, then kill the parents
        /// </summary>
        /// <param name="process_name"></param>
        [DebuggerNonUserCode]
        public static void KillProcesses(string process_name)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(process_name);

                if (processes == null || processes.Length < 1)
                    return;

                foreach (Process process in processes)
                    WindowsSystemHelper.KillProcessTree(process);
            }
            catch
            {
                //Invalid operation or process no longer exists
            }
        }

        [DebuggerNonUserCode]
        public static void KillProcessTree(Process process)
        {
            ManagementObjectSearcher m_object_searcher = new ManagementObjectSearcher($"Select * From Win32_Process Where ParentProcessID={process.Id}");

            try
            {
                ManagementObjectCollection m_objects = m_object_searcher.Get();

                if (m_objects == null || m_objects.Count < 1)
                    return;

                foreach (ManagementObject m_object in m_objects)
                {
                    int _process_id = Convert.ToInt32(m_object["ProcessID"]);
                    Process _process = Process.GetProcessById(_process_id);

                    //Recursively kill all children and their children first
                    WindowsSystemHelper.KillProcessTree(process);
                }
            }
            catch
            {
                //No processes found that match the provided parent process ID
            }
            finally
            {
                //Dispose of the WMI searcher and then kill the child/parent process at this level
                m_object_searcher.Dispose();

                if (!process.HasExited)
                    process.Kill();
            }
        }


        /// <summary>
        /// Launch an executable from CommandLine with provided args and return app handle
        /// </summary>
        public static IntPtr LaunchCommandLineApp(string proc_name, string exe_path, string command_args, bool maximized = false)
        {
            try
            {
                ProcessStartInfo start_info = new ProcessStartInfo(proc_name)
                {
                    UseShellExecute = false,
                    FileName = exe_path,
                    Arguments = command_args
                };

                //if (maximized)
                //    start_info.WindowStyle = ProcessWindowStyle.Maximized;

                Process exe_process = new Process
                {
                    StartInfo = start_info
                };

                exe_process.Start();
                exe_process.WaitForInputIdle(10000);

                Thread.Sleep(1000);

                IntPtr window_handle = IntPtr.Zero;
                if (maximized)
                {
                    window_handle = exe_process.MainWindowHandle;
                    Rectangle screen_area = Screen.AllScreens[0].Bounds;
                    MoveWindow(exe_process.MainWindowHandle, 0, 0, screen_area.Width, screen_area.Height, true);
                }

                Thread.Sleep(1000);

                exe_process.Close();
                return window_handle;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Encountered exception while executing command: [{command_args}] using executable '{exe_path}'... Exception: {ex.Message}");
                throw new InvalidOperationException("BeyondCompare GUI failed to start", ex.InnerException);
            }
        }

        /// <summary>
        /// Launch an executable from CommandLine with provided args
        /// </summary>
        public static int GetCommandLineResult(string proc_name, string exe_path, string command_args)
        {
            int return_code;

            try
            {
                ProcessStartInfo start_info = new ProcessStartInfo(proc_name)
                {
                    UseShellExecute = false,
                    FileName = exe_path,
                    Arguments = command_args
                };

                Process exe_process = new Process
                {
                    StartInfo = start_info
                };

                exe_process.Start();
                exe_process.WaitForExit(5000);

                return_code = exe_process.ExitCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Encountered exception while executing command: [{command_args}] using executable '{exe_path}'... Exception: {ex.Message}");
                throw new InvalidOperationException("BeyondCompare command script failed to start", ex.InnerException);
            }

            return return_code;
        }

        #endregion
    }
}
