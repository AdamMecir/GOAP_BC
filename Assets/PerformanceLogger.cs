using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class PerformanceLogger : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private string filePath;
    public GameManager gamemanager;
    void Start()
    {
        string directoryPath = @"C:\Users\admme\GOAP_BC";
        Directory.CreateDirectory(directoryPath);  // Ensure the directory exists

        filePath = Path.Combine(directoryPath, "performance_log.txt");

        // Log system information once at the start
        LogSystemInfo();

        StartCoroutine(LogPerformanceData());
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    IEnumerator LogPerformanceData()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            LogData();
        }
    }

    private void LogSystemInfo()
    {
        string systemInfo = "System Information:\n";
        systemInfo += $"Processor: {SystemInfo.processorType}, {SystemInfo.processorCount} cores\n";
        systemInfo += $"GPU: {SystemInfo.graphicsDeviceName}, {SystemInfo.graphicsMemorySize} MB\n";
        systemInfo += $"RAM: {SystemInfo.systemMemorySize} MB\n";
        systemInfo += $"Operating System: {SystemInfo.operatingSystem}\n";
        systemInfo += "TIME    FPS    MB   FRAMERATE(ms)   NPCcount  \n";
        File.AppendAllText(filePath, systemInfo);
        UnityEngine.Debug.Log("System Info logged: " + systemInfo);
    }

    private void LogData()
    {
        float memoryUsage = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong() / (1024.0f * 1024.0f); // Memory in MB
        int fps = Mathf.RoundToInt(1.0f / deltaTime);
        int frameTime = Mathf.RoundToInt(deltaTime * 1000.0f); // Frame time in milliseconds

        string text = $"{Time.timeSinceLevelLoad:F2}   {fps}   {memoryUsage:F2}   {frameTime}   {gamemanager.Count}\n";

        File.AppendAllText(filePath, text);
        UnityEngine.Debug.Log("Performance Data logged: " + text);
    }
}
