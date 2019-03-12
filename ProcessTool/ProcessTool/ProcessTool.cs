using System;
using System.Diagnostics;
using ProcessTool.ProcessMethods;

namespace ProcessTool
{
    public class ProcessTool
    {
        [Flags]
        public enum ProcessStartWindowType
        {
            Minimized,
            Maximized,
            Normal,
            Hidden
        }

        public Process Target { get; set; }

        #region ctor
        public ProcessTool(int processId)
        {
            this.Target = Native.GetProcess(processId);
        }

        public ProcessTool(string path, string args, ProcessStartWindowType? style)
        {
            this.Target = Native.StartProcess(path, args, style);
        }

        public ProcessTool(string processName)
        {
            this.Target = Native.GetProcess(processName);
        }
        #endregion
        #region reset target
        public void SetTargetProcess(int processId)
        {
            this.Target = Native.GetProcess(processId);
        }

        public void SetTargetProcess(string processName)
        {
            this.Target = Native.GetProcess(processName);
        }
        public void SetTargetProcess(string path, string args, ProcessStartWindowType? style)
        {
            this.Target = Native.StartProcess(path, args, style);
        }
        #endregion
        #region Memory.ProtectMemory
        public int ProtectMemory(IntPtr baseAddress, int size, int protection)
        {
            return MemoryMethods.ProtectMemory.Protect(this.Target.Id, baseAddress, size, protection);
        }
        #endregion
        #region Memory.ReadMemory
        public byte[] Read(IntPtr baseAddress, int size)
        {
            return MemoryMethods.ReadMemory.Read(this.Target.Id, baseAddress, size);
        }

        public T Read<T>(IntPtr baseAddress) where T : struct
        {
            return MemoryMethods.ReadMemory.ReadObject<T>(this.Target.Id, baseAddress);
        }
        #endregion
        #region Memory.WriteMemory
        public bool Write(IntPtr baseAddress, byte[] value)
        {
            return MemoryMethods.WriteMemory.Write(this.Target.Id, baseAddress, value);
        }
        public bool Write(IntPtr baseAddress, int value)
        {
            return MemoryMethods.WriteMemory.Write(this.Target.Id, baseAddress, value);
        }
        public bool Write(IntPtr baseAddress, float value)
        {
            return MemoryMethods.WriteMemory.Write(this.Target.Id, baseAddress, value);
        }
        public bool Write(IntPtr baseAddress, string value)
        {
            return MemoryMethods.WriteMemory.Write(this.Target.Id, baseAddress, value);
        }
        public bool Write<T>(IntPtr baseAddress, T value) where T : struct
        {
            return MemoryMethods.WriteMemory.Write<T>(this.Target.Id, baseAddress, value);
        }
        #endregion
    }
}
