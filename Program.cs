using System;
using System.Diagnostics;
using System.Threading;

class MultiThreadingSolution
{
    private static bool _continueThread = true;

    private static int _resetTime = 0;

    private static string controlApp = "excel,winword,saplogon,OUTLOOK";

    private static string[] controlApps = controlApp.Split(',');

    public static int controlTime = 5;

    public static void Main()
    {
        StartThread(controlTime, controlApps);

        Console.WriteLine("Thread started");

        Thread.Sleep(2000);

        ResetThread(5);

        CloseThread();
    }

    //Control method to start counting the provided time value to zero. When zero is reached,
    //the method will force quit all provided applications.
    public static void ControlMethod(int controlTime, string[] controlApps)
    {
        while (_continueThread & controlTime != 0)
        {
            if (_resetTime != 0)
            {
                controlTime = _resetTime;
                _resetTime = 0;
            }
            Thread.Sleep(1000);

            controlTime--;

            Console.WriteLine(controlTime);
        }

        if (controlTime == 0)
        {
            foreach (string app in controlApps)
            {
                foreach (Process p in Process.GetProcessesByName(app))
                {
                    p.Kill();
                    Thread.Sleep(500);
                }
            }
        }
    }

    //Start a thread that calls a parameterized static method.
    public static void StartThread(int controlTime, string[] controlApps)
    {
        Thread controlTread = new Thread(() => ControlMethod(controlTime, controlApps));

        controlTread.Start();
    }

    //Providing a non zero positive int number, the thread countdown is increased by said number.
    public static void ResetThread(int resetTime)
    {
        _resetTime = resetTime;
    }

    //By calling this method the thread is ddestroyed immidiatly without closing target applications. 
    public static void CloseThread()
    {
        _continueThread = false;
    }
}