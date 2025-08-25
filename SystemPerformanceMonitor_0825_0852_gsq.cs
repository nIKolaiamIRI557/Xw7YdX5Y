// 代码生成时间: 2025-08-25 08:52:56
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace SystemPerformanceMonitor
{
    /// <summary>
    /// A class to monitor system performance metrics.
    /// </summary>
    public class PerformanceMonitor
    {
        /// <summary>
        /// Gets the system's CPU usage as a percentage.
        /// </summary>
        /// <returns>The CPU usage percentage.</returns>
        public double GetCpuUsage()
        {
            try
            {
                var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                return cpuCounter.NextValue();
            }
            catch (Exception ex)
            {
                // Handle any potential errors with the PerformanceCounter
                Console.WriteLine($"Error retrieving CPU usage: {ex.Message}");
                return -1; // Return -1 to indicate an error
            }
        }

        /// <summary>
        /// Gets the system's memory usage as a percentage.
        /// </summary>
        /// <returns>The memory usage percentage.</returns>
        public double GetMemoryUsage()
        {
            try
            {
                var memoryCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
                return memoryCounter.NextValue();
            }
            catch (Exception ex)
            {
                // Handle any potential errors with the PerformanceCounter
                Console.WriteLine($"Error retrieving memory usage: {ex.Message}");
                return -1; // Return -1 to indicate an error
            }
        }

        /// <summary>
        /// Gets the system's disk usage.
        /// </summary>
        /// <param name="driveName">The drive to monitor (e.g., "C:").</param>
        /// <returns>The disk usage in bytes.</returns>
        public long GetDiskUsage(string driveName)
        {
            try
            {
                var drives = DriveInfo.GetDrives().Where(d => d.Name == driveName);
                if (drives.Any())
                {
                    return drives.First().TotalSize - drives.First().AvailableFreeSpace;
                }
                else
                {
                    Console.WriteLine($"Drive '{driveName}' not found.");
                    return -1; // Return -1 to indicate an error
                }
            }
            catch (Exception ex)
            {
                // Handle any potential errors with the DriveInfo
                Console.WriteLine($"Error retrieving disk usage: {ex.Message}");
                return -1; // Return -1 to indicate an error
            }
        }

        /// <summary>
        /// Gets the system's network usage.
        /// </summary>
        /// <param name="interfaceName">The network interface to monitor (e.g., "Ethernet").</param>
        /// <returns>A tuple containing sent and received bytes.</returns>
        public (long SentBytes, long ReceivedBytes) GetNetworkUsage(string interfaceName)
        {
            try
            {
                var counter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", interfaceName);
                long sentBytes = (long)counter.NextValue();
                counter.CounterName = "Bytes Received/sec";
                long receivedBytes = (long)counter.NextValue();
                return (sentBytes, receivedBytes);
            }
            catch (Exception ex)
            {
                // Handle any potential errors with the PerformanceCounter
                Console.WriteLine($"Error retrieving network usage: {ex.Message}");
                return (-1, -1); // Return -1 to indicate an error
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PerformanceMonitor monitor = new PerformanceMonitor();

            // Display CPU usage
            Console.WriteLine($"CPU Usage: {monitor.GetCpuUsage()}%");

            // Display memory usage
            Console.WriteLine($"Memory Usage: {monitor.GetMemoryUsage()}%");

            // Display disk usage for drive C:
            Console.WriteLine($"Disk Usage on C: {monitor.GetDiskUsage("C:") / 1024 / 1024 / 1024} GB");

            // Display network usage for Ethernet interface
            var networkUsage = monitor.GetNetworkUsage("Ethernet");
            Console.WriteLine($"Network Sent: {networkUsage.SentBytes / 1024 / 1024} MB");
            Console.WriteLine($"Network Received: {networkUsage.ReceivedBytes / 1024 / 1024} MB");
        }
    }
}