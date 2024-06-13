using UnityEngine;
using System;
using System.IO;
using System.Diagnostics;
using System.Text;

public class HardwareStatistics : MonoBehaviour
{
    public float loggingInterval = 3f; // Interval in seconds to log system usage
    public string logFileName = "SystemUsageLog.txt"; // Name of the log file

    private float timer = 0f;

    void Start()
    {
        // Write header to log file
        WriteToFile("Time\tCPU Usage(%)\tRAM Usage(MB)\tActive Processes");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= loggingInterval)
        {
            // Log system usage
            LogSystemUsage();

            // Reset timer
            timer = 0f;
        }
    }

    void LogSystemUsage()
    {
        // Get current time
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // Get CPU usage
        float cpuUsage = GetCPUUsage();

        // Get RAM usage
        float ramUsage = GetRAMUsage();

        // Get active processes
        string activeProcesses = GetActiveProcesses();

        // Write to log file
        WriteToFile(currentTime + "\t" + cpuUsage.ToString("0.00") + "\t" + ramUsage.ToString("0.00") + "\t" + activeProcesses);
    }

    float GetCPUUsage()
    {
        // Use Unity's Profiler to get CPU usage
        float cpuUsage = UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong() / (float)UnityEngine.SystemInfo.processorCount;
        return cpuUsage;
    }

    float GetRAMUsage()
    {
        // Obtain RAM usage in MB
        float ramUsage = UnityEngine.Profiling.Profiler.usedHeapSizeLong / (1024f * 1024f); // Convert bytes to MB
        return ramUsage;
    }

    string GetActiveProcesses()
    {
        // Get active processes
        StringBuilder activeProcesses = new StringBuilder();
        try
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                try
                {
                    string processName = process.ProcessName;
                    activeProcesses.AppendLine(processName);
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogWarning("Error accessing process: " + e.Message);
                }
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogWarning("Error retrieving processes: " + ex.Message);
        }
        return activeProcesses.ToString();
    }

    void WriteToFile(string text)
    {
        // Append text to the log file in the project's asset folder
        string path = Path.Combine(Application.dataPath, logFileName);
        using (StreamWriter writer = File.AppendText(path))
        {
            writer.WriteLine(text);
        }
    }
}
