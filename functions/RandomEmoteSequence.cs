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
        { "G", 0x47 },
        { "Right", 0x27 },
        { "E", 0x45 }
    };
    
    static readonly Random random = new Random();
    const int maxRightArrows = 10;
    
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
        int randomRightCount = random.Next(1, maxRightArrows + 1);
        
        List<string> dynamicKeySequence = new List<string>();
        dynamicKeySequence.Add("G");
        
        for (int i = 0; i < randomRightCount; i++)
        {
            dynamicKeySequence.Add("Right");
        }
        
        dynamicKeySequence.Add("E");
        
        for (int i = 0; i < dynamicKeySequence.Count; i++)
        {
            string keyName = dynamicKeySequence[i];
            byte keyCode = KeyMap[keyName];
            await PressKey(keyCode);
            
            if (keyName == "E")
            {
                await Task.Delay(200);
            }
            else
            {
                await Task.Delay(100);
            }
        }
    }

    public bool Execute()
    {
        Thread t = new Thread(() => Run().Wait());
        t.Start();
        return true;
    }
}
