using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

public class KeyPressSequence
{
    [DllImport("user32.dll")]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr extraInfo);
    [DllImport("user32.dll")]
    static extern uint MapVirtualKey(uint uCode, uint uMapType);
    const uint KEYEVENTF_KEYUP = 0x0002;
    static readonly Dictionary<string, byte> KeyMap = new Dictionary<string, byte>
    {
        // Special keys
        { "Escape", 0x1B }, { "Enter", 0x0D }, { "Space", 0x20 }, { "Tab", 0x09 },
        { "Shift", 0x10 }, { "Ctrl", 0x11 }, { "Alt", 0x12 },
        
        // Arrow keys
        { "Left", 0x25 }, { "Up", 0x26 }, { "Right", 0x27 }, { "Down", 0x28 },
        
        // Letters A-Z
        { "A", 0x41 }, { "B", 0x42 }, { "C", 0x43 }, { "D", 0x44 },
        { "E", 0x45 }, { "F", 0x46 }, { "G", 0x47 }, { "H", 0x48 },
        { "I", 0x49 }, { "J", 0x4A }, { "K", 0x4B }, { "L", 0x4C },
        { "M", 0x4D }, { "N", 0x4E }, { "O", 0x4F }, { "P", 0x50 },
        { "Q", 0x51 }, { "R", 0x52 }, { "S", 0x53 }, { "T", 0x54 },
        { "U", 0x55 }, { "V", 0x56 }, { "W", 0x57 }, { "X", 0x58 },
        { "Y", 0x59 }, { "Z", 0x5A },
        
        // Numbers 0-9
        { "0", 0x30 }, { "1", 0x31 }, { "2", 0x32 }, { "3", 0x33 },
        { "4", 0x34 }, { "5", 0x35 }, { "6", 0x36 }, { "7", 0x37 },
        { "8", 0x38 }, { "9", 0x39 }
    };
    
    // change sequence here - can change it to be args so can reuse. dictionary above just makes it easier to make it readable, there are better ways
    readonly string[] keySequence =
    {
        "Escape",
        "Right",
        "E",
        "F",
        "Escape"
    };
    async Task PressKey(byte key, int holdMs = 50)
    {
        byte scan = (byte)MapVirtualKey(key, 0);
        uint flags = 0;
        uint flagsUp = KEYEVENTF_KEYUP;
        // Arrow keys need extended key flag
        if (key == 0x25 || key == 0x26 || key == 0x27 || key == 0x28) 
        {
            flags = 0x1; // KEYEVENTF_EXTENDEDKEY
            flagsUp = KEYEVENTF_KEYUP | 0x1;
        }

        // Key down
        keybd_event(key, scan, flags, IntPtr.Zero);

        // Optional small hold
        await Task.Delay(holdMs);

        // Key up
        keybd_event(key, scan, flagsUp, IntPtr.Zero);
    }

    async Task Run()
    {
        foreach (string keyName in keySequence)
        {
            byte keyCode = KeyMap[keyName];
            await PressKey(keyCode);
            await Task.Delay(100);
        }
    }

    public bool Execute()
    {
        Thread t = new Thread(() => Run().Wait());
        t.Start();
        return true;
    }
    public static void Main()
    {
        new KeyPressSequence().Execute();
    }
}
