using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Reflection;

public class CPHInline
{
    [DllImport("user32.dll")]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr extraInfo);
    [DllImport("user32.dll")]
    static extern uint MapVirtualKey(uint uCode, uint uMapType);
    const uint KEYEVENTF_KEYUP = 0x0002;
    
    static readonly Dictionary<string, byte> KeyMap = new Dictionary<string, byte>
    {
        { "W", 0x57 },
        { "Space", 0x20 }
    };
    
    // change this to be the duration of the loop
    readonly int loopDurationSeconds = 20;
    
    private volatile bool shouldStop = false;
    
    async Task PressKey(byte key, int holdMs = 50)
    {
        byte scan = (byte)MapVirtualKey(key, 0);
        uint flags = 0;
        uint flagsUp = KEYEVENTF_KEYUP;

        keybd_event(key, scan, flags, IntPtr.Zero);

        await Task.Delay(holdMs);

        keybd_event(key, scan, flagsUp, IntPtr.Zero);
    }

    async Task Run()
    {
        DateTime startTime = DateTime.Now;
        TimeSpan duration = TimeSpan.FromSeconds(loopDurationSeconds);
        
        while (DateTime.Now - startTime < duration && !shouldStop)
        {
            // Check for rawInput parameter to stop the function
            if (CPH.TryGetArg("rawInput", out string rawInput))
            {
                if (!string.IsNullOrEmpty(rawInput) && 
                    (rawInput.ToLower() == "true" || rawInput.ToLower() == "infiniterollstop"))
                {
                    shouldStop = true;
                    CPH.SetGlobalVar("infiniteRollStop", "stop", true);
                    break;
                }
            }
            
            // Check global variable state
            string globalState = CPH.GetGlobalVar<string>("infiniteRollStop", true);
            if (!string.IsNullOrEmpty(globalState) && globalState.ToLower() == "stop")
            {
                shouldStop = true;
                break;
            }
            
            byte wKey = KeyMap["W"];
            await PressKey(wKey);
            await Task.Delay(100);
            
            if (shouldStop) break;
            
            byte spaceKey = KeyMap["Space"];
            await PressKey(spaceKey);
            await Task.Delay(100);
        }
    }

    public bool Execute()
    {
        // Check for rawInput parameter on every execution
        if (CPH.TryGetArg("rawInput", out string rawInput))
        {
            if (!string.IsNullOrEmpty(rawInput) && 
                (rawInput.ToLower() == "true" || rawInput.ToLower() == "infiniterollstop"))
            {
                shouldStop = true;
                CPH.SetGlobalVar("infiniteRollStop", "stop", true);
                return true;
            }
        }
        
        // Check current global state
        string currentState = CPH.GetGlobalVar<string>("infiniteRollStop", true);
        if (!string.IsNullOrEmpty(currentState) && currentState.ToLower() == "stop")
        {
            return true; // Don't start if already stopped
        }
        
        // Set state to start and begin the infinite roll
        CPH.SetGlobalVar("infiniteRollStop", "start", true);
        shouldStop = false;

        Thread t = new Thread(() => Run().Wait());
        t.Start();
        return true;
    }
    
    public void Stop()
    {
        shouldStop = true;
        CPH.SetGlobalVar("infiniteRollStop", "stop", true);
    }
    
    public static void Main()
    {
        new CPHInline().Execute();
    }
}
