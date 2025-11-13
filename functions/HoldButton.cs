using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

public class HoldButton
{
    System.Timers.Timer timer;
    static bool HoldKey = false;
    Stopwatch sw;
    static int HoldDurationMs = 10000; // fallback if no seconds provided
    static byte Key = 0; 


    async Task Run(byte key, int interval)
    {
        const uint KEYEVENTF_KEYUP = 0x0002;
        byte bScan = (byte)MapVirtualKey((uint)key, 0);
        sw = new Stopwatch();

        try
        {
            sw.Start();
            TimerStart(interval);

            // Key down
            keybd_event(key, bScan, 0, IntPtr.Zero);

            // Keep held
            while (HoldKey)
            {
                await Task.Delay(10);
            }

            // Key up
            keybd_event(key, bScan, KEYEVENTF_KEYUP, IntPtr.Zero);

            sw.Stop();
            Console.WriteLine($"Held key '{(char)key}' for {sw.ElapsedMilliseconds} ms");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e}");
        }
    }

    void TimerStart(int interval = 1000)
    {
        timer = new System.Timers.Timer();
        timer.Elapsed += Timer_Elapsed;
        timer.AutoReset = false;
        timer.Interval = interval;
        HoldKey = true;
        timer.Enabled = true;
    }

    static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        HoldKey = false;
    }

    [DllImport("user32.dll")]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr extraInfo);

    [DllImport("user32.dll")]
    static extern uint MapVirtualKey(uint uCode, uint uMapType);

    public bool Execute()
    {
        if (args.ContainsKey("inputKey"))
        {
            string inputKey = args["inputKey"].ToString().ToUpper();
            if (inputKey.Length == 1)
            {
                Key = (byte)inputKey[0];
            }
        }

        if (Key == 0)
        {
            Console.WriteLine("No valid inputKey provided.");
            return false;
        }

        if (args.ContainsKey("inputSeconds"))
        {
            if (int.TryParse(args["inputSeconds"].ToString(), out int sec))
            {
                HoldDurationMs = sec * 1000;
            }
        }

        if (args.ContainsKey("stopNow"))
        {
            bool stop =
                args["stopNow"].ToString().ToLower() == "true" ||
                args["stopNow"].ToString() == "1";

            if (stop)
            {
                HoldKey = false;
                return true;
            }
        }

        Thread t = new Thread(() => Run(Key, HoldDurationMs).Wait());
        t.Start();

        return true;
    }
}
