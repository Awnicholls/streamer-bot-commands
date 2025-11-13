using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

public class CPHInline
{
    // Keyboard hook constants
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private const int WM_KEYUP = 0x0101;
    
    // Virtual key codes for WASD
    private const int VK_W = 0x57;
    private const int VK_A = 0x41;
    private const int VK_S = 0x53;
    private const int VK_D = 0x44;
    
    // Timer and state variables
    private static System.Timers.Timer globalTimer;
    private static bool isActive = false;
    private static LowLevelKeyboardProc keyboardProc = HookCallback;
    private static IntPtr hookID = IntPtr.Zero;
    
    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
    
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);
    
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
    
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
    
    [StructLayout(LayoutKind.Sequential)]
    private struct KBDLLHOOKSTRUCT
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }
    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && isActive)
        {
            if (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_KEYUP)
            {
                KBDLLHOOKSTRUCT kbd = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                
                if (kbd.vkCode == VK_W || kbd.vkCode == VK_A || kbd.vkCode == VK_S || kbd.vkCode == VK_D)
                {
                    Console.WriteLine($"Intercepted key: {(char)kbd.vkCode}");
                    return (IntPtr)1;
                }
            }
        }
        
        return CallNextHookEx(hookID, nCode, wParam, lParam);
    }
    
    // Install keyboard hook
    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }
    
    // Start the timer
    private static void StartTimer()
    {
        globalTimer = new System.Timers.Timer(1 * 60 * 1000); 
        globalTimer.Elapsed += OnTimerElapsed;
        globalTimer.AutoReset = false;
        globalTimer.Start();
        
        Console.WriteLine("60-minute timer started. WASD keys will be intercepted.");
    }
    
    // Timer elapsed event
    private static void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        isActive = false;
        
        // Unhook keyboard
        if (hookID != IntPtr.Zero)
        {
            UnhookWindowsHookEx(hookID);
            hookID = IntPtr.Zero;
        }
        
        // Update StreamerBot global variable
        CPH.SetGlobalVar("keyInterceptActive", false);
        
        Console.WriteLine("60 minutes elapsed. Key interception disabled. Global variable updated.");
        
        globalTimer?.Dispose();
    }
    
    public bool Execute()
    {
        try
        {
            // Check if we should start based on global variable
            bool shouldStart = false;
            
            if (args.ContainsKey("startInterception"))
            {
                shouldStart = args["startInterception"].ToString().ToLower() == "true";
            }
            else
            {
                // Check StreamerBot global variable
                var globalVar = CPH.GetGlobalVar("keyInterceptActive");
                if (globalVar != null)
                {
                    shouldStart = globalVar.ToString().ToLower() == "true";
                }
            }
            
            if (shouldStart && !isActive)
            {
                isActive = true;
                
                // Set up keyboard hook
                hookID = SetHook(keyboardProc);
                
                if (hookID == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to install keyboard hook.");
                    return false;
                }
                
                // Start the timer
                StartTimer();
                
                // Update global variable to indicate active
                CPH.SetGlobalVar("keyInterceptActive", true);
                
                Console.WriteLine("Key interception started for 60 minutes.");
                
                Thread hookThread = new Thread(() =>
                {
                    while (isActive)
                    {
                        Thread.Sleep(100);
                    }
                });
                hookThread.IsBackground = true;
                hookThread.Start();
            }
            else if (!shouldStart && isActive)
            {
                isActive = false;
                
                if (hookID != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(hookID);
                    hookID = IntPtr.Zero;
                }
                
                globalTimer?.Stop();
                globalTimer?.Dispose();
                
                CPH.SetGlobalVar("keyInterceptActive", false);
                
                Console.WriteLine("Key interception manually stopped.");
            }
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in key interceptor: {ex.Message}");
            return false;
        }
    }
}
