using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace ProcessTool.MemoryMethods
{
    internal static class Native
    {
        #region kernel32
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool ReadProcessMemory(int processHandle, IntPtr baseAddress, byte[] buffer, int size, IntPtr bytesReadBuffer);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr VirtualAllocEx(int processHandle, IntPtr baseAddress, int size, int allocationType, int protection);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool VirtualFreeEx(int processHandle, IntPtr baseAddress, int size, int freeType);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool VirtualProtectEx(int processHandle, IntPtr baseAddress, int size, int newProtection, out int oldProtection);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool VirtualQueryEx(int processHandle, IntPtr baseAddress, out MemoryBasicInformation buffer, int length);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool WriteProcessMemory(int processHandle, IntPtr baseAddress, byte[] buffer, int size, IntPtr bytesWrittenBuffer);
        #endregion

        #region flags
        [Flags]
        internal enum MemoryAllocation
        {
            Commit = 0x01000,
            Reserve = 0x02000,
            Release = 0x08000
        }

        [Flags]
        internal enum MemoryProtection
        {
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            Execute = 0x010,
            ExecuteRead = 0x020,
            ExecuteReadWrite = 0x040,
            ExecuteWriteCopy = 0x080,
            Guard = 0x0100,
            NoCache = 0x0200,
            WriteCombine = 0x0400
        }

        [Flags]
        internal enum MemoryRegionType
        {
            MemoryImage = 0x01000000
        }
        #endregion

        #region objs
        [StructLayout(LayoutKind.Sequential)]
        internal struct MemoryBasicInformation
        {
            public readonly IntPtr BaseAddress;
            private readonly IntPtr AllocationBase;
            private readonly uint AllocationProtect;
            public readonly IntPtr RegionSize;
            public readonly uint State;
            public readonly uint Protect;
            public readonly uint Type;
        }
        #endregion
    }
}
