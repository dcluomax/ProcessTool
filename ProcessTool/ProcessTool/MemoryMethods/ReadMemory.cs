using System;
using System.Runtime.InteropServices;

namespace ProcessTool.MemoryMethods
{
    class ReadMemory
    {
        internal static byte[] Read(int processHandle, IntPtr baseAddress, int size)
        {
            var buffer = new byte[size];
            int prevProtection = ProtectMemory.Protect(processHandle, baseAddress, buffer.Length, (int)Native.MemoryProtection.ReadWrite);
            if (!Native.ReadProcessMemory(processHandle, baseAddress, buffer, buffer.Length, IntPtr.Zero))
            {
                throw new Exception("Failed to read memory from the process");
            }
            ProtectMemory.Protect(processHandle, baseAddress, buffer.Length, prevProtection);
            return buffer;
        }

        internal static T ReadObject<T>(int processHandle, IntPtr baseAddress) where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            var buffer = Read(processHandle, baseAddress, size);
            var bufferAddress = Marshal.AllocHGlobal(buffer.Length);
            Marshal.Copy(buffer, 0, bufferAddress, buffer.Length);
            var structure = (T)Marshal.PtrToStructure(bufferAddress, typeof(T));
            Marshal.FreeHGlobal(bufferAddress);
            return structure;
        }
    }
}
