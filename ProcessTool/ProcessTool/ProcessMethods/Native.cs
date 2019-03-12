using System;
using System.Diagnostics;
using static ProcessTool.ProcessTool;

namespace ProcessTool.ProcessMethods
{
    class Native
    {
        internal static Process GetProcess(string processName) {
            if (string.IsNullOrWhiteSpace(processName))
            {
                throw new ArgumentException("Invalid Process Name.");
            }
            Process process;
            try
            {
                process = Process.GetProcessesByName(processName)[0];
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception($"No process found with name {processName}.");
            }
            return process;
        }

        internal static Process GetProcess(int processId)
        {
            if (processId < 1)
            {
                throw new ArgumentException("Invalid Process Id.");
            }
            Process process;
            try
            {
                process = Process.GetProcessById(processId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"No process found with id {processId}.");
            }
            return process;
        }

        internal static Process StartProcess(string path, string args, ProcessStartWindowType? windowType) {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = path;
            startInfo.Arguments = args;

            switch (windowType) {
                case ProcessStartWindowType.Hidden:
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    break;
                case ProcessStartWindowType.Maximized:
                    startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    break;
                case ProcessStartWindowType.Minimized:
                    startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    break;
                case ProcessStartWindowType.Normal:
                default:
                    break;
            }

            Process processTemp = new Process();
            processTemp.StartInfo = startInfo;
            processTemp.EnableRaisingEvents = true;
            try
            {
                 processTemp.Start();
            }
            catch (Exception e)
            {
                throw e;
            }
            return processTemp;
        }

    }
}