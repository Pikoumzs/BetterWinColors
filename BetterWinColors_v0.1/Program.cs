using Microsoft.Win32;
using System;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BetterWinColors_v0._1
{
    internal class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SystemParametersInfo(
            uint uiAction,
            uint uiParam,
            IntPtr pvParam,
            uint fWinIni);

        private const uint SPI_SETNONCLIENTMETRICS = 0x002A;
        private const uint SPIF_UPDATEINIFILE = 0x01;
        private const uint SPIF_SENDCHANGE = 0x02;

        static void Main(string[] args)
        {
            Console.WriteLine("=== BetterWinColors v0.1 ===");
            Console.WriteLine("What would you like to change?");
            Console.WriteLine("1 - Highlight");
            Console.WriteLine("2 - HotTrackingColor");
            Console.WriteLine("3 - Both");
            Console.Write("Your choice : ");

            string choice = Console.ReadLine();

            string highlight = null;
            string hotTracking = null;

            if (choice == "1" || choice == "3")
            {
                highlight = AskForColor("Highlight");
            }

            if (choice == "2" || choice == "3")
            {
                hotTracking = AskForColor("HotTrackingColor");
            }

            if (highlight == null && hotTracking == null)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Colors", true))
                {
                    if (key == null)
                    {
                        Console.WriteLine("Unable to open the registry key.");
                        return;
                    }

                    if (highlight != null)
                        key.SetValue("Highlight", highlight);

                    if (hotTracking != null)
                        key.SetValue("HotTrackingColor", hotTracking);
                }

                RefreshSystem();

                Console.WriteLine("\nColor(s) successfully updated.");
                AskForRestart();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
            }
        }

        static string AskForColor(string name)
        {
            Console.WriteLine($"\nEnter the RGB values ​​for {name} :");

            int r = AskChannel("R");
            int g = AskChannel("G");
            int b = AskChannel("B");

            return $"{r} {g} {b}";
        }

        static int AskChannel(string channel)
        {
            while (true)
            {
                Console.Write($"{channel} (0-255) : ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int value) && value >= 0 && value <= 255)
                    return value;

                Console.WriteLine("Invalid value. Retry.");
            }
        }

        static void RefreshSystem()
        {
            SystemParametersInfo(
                SPI_SETNONCLIENTMETRICS,
                0,
                IntPtr.Zero,
                SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
        }

        static void AskForRestart()
        {
            Console.Write("\nReboot system now ? (Y/N) : ");
            string input = Console.ReadLine()?.Trim().ToUpper();

            if (input == "Y")
            {
                Console.WriteLine("Processing reboot...");
                Process.Start(new ProcessStartInfo
                {
                    FileName = "shutdown",
                    Arguments = "/r /t 0",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
            }
            else
            {
                Console.WriteLine("Bye.");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
        }
    }
}