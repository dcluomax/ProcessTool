using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ProcessTool.MemoryMethods
{
    class WriteMemory
    {
        internal static bool Write(int processHandle, IntPtr baseAddress, byte[] buffer)
        {
            if (!Native.VirtualProtectEx(processHandle, baseAddress, buffer.Length, (int)Native.MemoryProtection.ReadWrite, out var oldProtection))
            {
                throw new Exception("Allow write to memory region failed.");
            }
            if (!Native.WriteProcessMemory(processHandle, baseAddress, buffer, buffer.Length, IntPtr.Zero))
            {
                throw new Exception("Write to address failed");
            }
            if (!Native.VirtualProtectEx(processHandle, baseAddress, buffer.Length, oldProtection, out _))
            {
                throw new Exception("Failed to restore memory protection.");
            }

            return true;
        }

        internal static bool Write(int processHandle, IntPtr baseAddress, float value)
        {
            return Write(processHandle, baseAddress, BitConverter.GetBytes(value));
        }

        internal static bool Write(int processHandle, IntPtr baseAddress, int value)
        {
            return Write(processHandle, baseAddress, BitConverter.GetBytes(value));
        }

        internal static bool Write(int processHandle, IntPtr baseAddress, string value)
        {
            return Write(processHandle, baseAddress, Encoding.Unicode.GetBytes(value));
        }

        internal static bool Write<TStructure>(int processHandle, IntPtr baseAddress, TStructure structure) where TStructure : struct
        {
            var size = Marshal.SizeOf(typeof(TStructure));
            var buffer = new byte[size];
            var structureAddress = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structure, structureAddress, true);
            Marshal.Copy(structureAddress, buffer, 0, size);
            Marshal.FreeHGlobal(structureAddress);
            return Write(processHandle, baseAddress, buffer);
        }
    }
}
