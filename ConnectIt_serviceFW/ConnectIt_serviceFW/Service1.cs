using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ConnectIt_serviceFW
{
    public partial class Service1: ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            Open_ConenctNow_CaptureScreenComm();
        }
 
        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }
        private static void Open_ConenctNow_CaptureScreenComm()
        {
            // Specify the path to the executable file
            string pathToExe = @"C:\Users\MdZawadHossain\Desktop\p4724ConnNow\Srcp4724ConnNow\CaptureScreenComm\CaptureScreenComm\bin\Release\CaptureScreenComm.exe";
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string scriptRelativePath = @"CaptureComm\CaptureScreenComm.exe";
            //string pathToExe = Path.Combine(appDirectory, scriptRelativePath);
            WriteToFile(pathToExe);
            // Create a new process start info
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = pathToExe; // Set the executable file name
            startInfo.Arguments = ""; // Set any arguments if needed
            startInfo.WindowStyle = ProcessWindowStyle.Normal; // Set the window style

            try
            {
                // Start the process
                using (Process exeProcess = Process.Start(startInfo))
                {
                    // Optionally, wait for the process to exit
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur
                WriteToFile($"An error occurred {pathToExe}: " + ex.Message);
            }
        }

        public static void WriteToFile(string Message)
        {
            string programDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "ConnectIt");
            if (!Directory.Exists(programDataPath))
            {
                Directory.CreateDirectory(programDataPath);
            }
            string filepath = Path.Combine(programDataPath, "ConnectItServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt");
            if (!File.Exists(filepath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

    }
}
