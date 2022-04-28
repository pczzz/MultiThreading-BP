# MultiThreading-BP

MultiThreading-BP is a C# script for BluePrism used to prevent code execution stop due to unforseen issues with the target automated application, e.g. error popup. By running a second thread, we can "control" the application behavior and if necesarry, force quit it.
# Usage

The whole script exists one file:
- [Program.cs](https://github.com/JurajPalusek/MultiThreading-BP/blob/main/Program.cs)

The main feature of the script is the ControlMethod:

```cs
    public static void ControlMethod(int controlTime, string[] controlApps)
```

...which is ran as an lambda input to method:

```cs
    public static void StartThread(int controlTime, string[] controlApps)
    {
        Thread controlTread = new Thread(() => ControlMethod(controlTime, controlApps));

        controlTread.Start();
    }
```

This allows us to have a separate thread running the ControlMethod independently from the main RPA code execution.

```cs
    public static int controlTime = 5;
```
This variable represents the number of seconds the second thread will run, until the ControlMethod starts force quiting provided applications. This variable is provided by Developer, here the valua 5 is just for testing purposes.

The said applications are provided also by Developer as a simple string of application names from Task Manager.

```cs
    private static string controlApp = "excel,winword,saplogon,OUTLOOK";
```

These are then split to array of values in order to be able to easily loop through them

```cs
    private static string[] controlApps = controlApp.Split(',');
```

# Contributing

This code is for demonstration purposes. No pull or clone requests are possible.