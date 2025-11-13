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
    
    readonly int loopDurationSeconds = 20;
    
    private volatile bool shouldStop = false;
    
    private T GetGlobalVar<T>(string varName, T defaultValue)
    {
        try
        {
            var method = CPH.GetType().GetMethod("GetGlobalVar");
            if (method != null)
            {
                var genericMethod = method.MakeGenericMethod(typeof(T));
                var result = genericMethod.Invoke(CPH, new object[] { varName, defaultValue });
                return (T)result;
            }
        }
        catch { }
        return defaultValue;
    }
    
    private void SetGlobalVar(string varName, object value, bool persist = false)
    {
        try
        {
            var method = CPH.GetType().GetMethod("SetGlobalVar", new Type[] { typeof(string), typeof(object), typeof(bool) });
            if (method != null)
            {
                method.Invoke(CPH, new object[] { varName, value, persist });
            }
        }
        catch { }
    }
    
    private string GetArg(string argName)
    {
        try
        {
            var method = CPH.GetType().GetMethod("GetArg");
            if (method != null)
            {
                var result = method.Invoke(CPH, new object[] { argName });
                return result?.ToString() ?? "";
            }
        }
        catch { }
        return "";
    }
    
    private bool TryGetArg(string argName, out string value)
    {
        try
        {
            var method = CPH.GetType().GetMethod("TryGetArg", new Type[] { typeof(string), typeof(string).MakeByRefType() });
            if (method != null)
            {
                object[] parameters = new object[] { argName, null };
                bool result = (bool)method.Invoke(CPH, parameters);
                value = parameters[1]?.ToString() ?? "";
                return result;
            }
        }
        catch { }
        value = "";
        return false;
    }
    
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
            // Check global variable for stop condition at start of each cycle
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
            
            // Check rawInput for infiniteRollStop command after each W+Space sequence
            if (CPH != null && TryGetArg("rawInput", out string rawInput))
            {
                if (!string.IsNullOrEmpty(rawInput) && rawInput.ToLower() == "infiniterollstop")
                {
                    shouldStop = true;
                    break;
                }
                else if (!string.IsNullOrEmpty(rawInput) && rawInput.ToLower().StartsWith("infiniterollstop "))
                {
                    // Extract the value part after "infiniterollstop "
                    string value = rawInput.Substring("infiniterollstop ".Length).ToLower().Trim();
                    if (value == "true")
                    {
                        shouldStop = true;
                        break;
                    }
                }
            }
        }
    }

    public bool Execute()
    {
        string stopArg = GetArg("stop")?.ToLower() ?? "";
        if (stopArg == "true" || stopArg == "1")
        {
            // Signal any running instance via global variable
            CPH.SetGlobalVar("infiniteRollStop", "stop", true);
            shouldStop = true;
            return true;
        }
        
        // Check current global state - don't run if it's "stop"
        string currentState = CPH.GetGlobalVar<string>("infiniteRollStop", true);
        if (!string.IsNullOrEmpty(currentState) && currentState.ToLower() == "stop")
        {
            return true; // Don't start if global state is "stop"
        }
        
        // Set state to "start" and begin new loop
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
