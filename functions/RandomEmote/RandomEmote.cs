using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

public class CPHInline
{
    [DllImport("user32.dll")]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr extraInfo);
    [DllImport("user32.dll")]
    static extern uint MapVirtualKey(uint uCode, uint uMapType);
    const uint KEYEVENTF_KEYUP = 0x0002;
    
    static readonly Dictionary<string, byte> KeyMap = new Dictionary<string, byte>
    {
        { "0", 0x30 }, { "1", 0x31 }, { "2", 0x32 }, { "3", 0x33 }, { "4", 0x34 },
        { "5", 0x35 }, { "6", 0x36 }, { "7", 0x37 }, { "8", 0x38 }, { "9", 0x39 },
        { "Enter", 0x0D }
    };
    
    static readonly Random random = new Random();
    
    async Task PressKey(byte key, int holdMs = 50)
    {
        byte scan = (byte)MapVirtualKey(key, 0);
        uint flags = 0;
        uint flagsUp = KEYEVENTF_KEYUP;
        
        if (key == 0x25 || key == 0x26 || key == 0x27 || key == 0x28)
        {
            flags = 0x1;
            flagsUp = KEYEVENTF_KEYUP | 0x1;
        }

        keybd_event(key, scan, flags, IntPtr.Zero);
        await Task.Delay(holdMs);
        keybd_event(key, scan, flagsUp, IntPtr.Zero);
    }

    async Task Run()
    {
        // Select a random number from 0-9
        int randomEmoteNumber = random.Next(0, 10);
        string emoteKey = randomEmoteNumber.ToString();
        
        // Press the random number key
        byte numberKeyCode = KeyMap[emoteKey];
        await PressKey(numberKeyCode);
        await Task.Delay(100);
        
        // Press Enter to trigger the emote
        byte enterKeyCode = KeyMap["Enter"];
        await PressKey(enterKeyCode);
    }

    public bool Execute()
    {
        Thread t = new Thread(() => Run().Wait());
        t.Start();
        return true;
    }

    public static void Main()
    {
        new CPHInline().Execute();
    }
}
