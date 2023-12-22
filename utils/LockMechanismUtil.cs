using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.service.commands;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebSocketReverseShellDotNet.utils
{
    internal class LockMechanismUtil
    {
        public static bool allowWrite = true;

        public static int writeTimeMilliseconds = 250;



        public static void StartLockMechanism()
        {

            try
            {
                // check if the lock file changes within the last 10 seconds
                if (!CheckFileChanges(3, 250))
                {
                    Console.WriteLine("Starting lock File");
                    StartLock();
 
                }
                else
                {
                    Console.WriteLine("lock File is running");

                    Environment.Exit(0);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        static void StartLock()
        {
            string lockFileLocation = GetLockFileLocation();
            CreateLockFileIfItDoesntExist(lockFileLocation);
            ScheduleFileWrite(lockFileLocation);

        }

        static void ScheduleFileWrite(string filePath)
        {
            // Create a CancellationTokenSource to stop the scheduled task if needed
            var cancellationTokenSource = new CancellationTokenSource();

            // Create a TaskScheduler
            var taskScheduler = TaskScheduler.Default;

            // Schedule the task to run every 4 seconds
            Task.Factory.StartNew(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    // Generate a random 20-digit number
                    long randomNumber = GenerateRandomNumber();

                    // Write the number to the file
                    WriteNumberToFile(filePath, randomNumber.ToString());

                    // Wait for 1 second before the next run
                    await Task.Delay(TimeSpan.FromMilliseconds(writeTimeMilliseconds));
                }
            }, cancellationTokenSource.Token, TaskCreationOptions.LongRunning, taskScheduler);
        }


        public static string GetLockFileLocation()
        {
            return OSUtil.GetSystemTempDir() + Constants.LOCK_FILE_NAME;
        }
        static void CreateLockFileIfItDoesntExist(string path)
        {
            // Check if the file exists
            bool fileExists = File.Exists(path);

            if (fileExists)
            {
                Console.WriteLine("The file exists.");
            }
            else
            {
                File.WriteAllText(path, "File content");
                Console.WriteLine("The file does not exist.");
            }
        }
        
        static void WriteNumberToFile(string path, string number)
        {
            if (!allowWrite)
            {
                return;
            }

            try
            {
                // Write the number to the file, overriding existing content
                File.WriteAllText(path, number);
                Console.WriteLine("Number written to file: " + number);
            }
            catch (IOException e)
            {
               /* Console.Error.WriteLine("Error writing to the file: " + e.Message);
                Console.Error.WriteLine(e.StackTrace);*/
            }
        }
        static long GenerateRandomNumber()
        {
            Random random = new Random();
            // Generate a random 20-digit number
            long baseNumber = 1_000_000_000_000_000_000L;
            long range = 9_000_000_000_000_000_000L;
            return baseNumber + (long)(random.NextDouble() * range);
        }

        static bool CheckFileChanges(int numChecks, int intervalMilliSeconds)
        {
            if (!File.Exists(GetLockFileLocation())) return false;
            string filePath = GetLockFileLocation();
            string initialHash = GetFileHash(filePath);

            try
            {
                for (int i = 0; i < numChecks; i++)
                {
                    string currentHash = GetFileHash(filePath);

                    if (!initialHash.Equals(currentHash))
                    {
                        return true; // File changed
                    }

                    Thread.Sleep(TimeSpan.FromMilliseconds(intervalMilliSeconds));
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error checking file changes: " + e.Message);
            }

            return false; // File did not change within the specified number of checks
        }

        static string GetFileHash(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
        }



    }
}
