using System;

namespace ProcessTool.MemoryMethods
{
    class ProtectMemory
    {
        internal static int Protect(int processHandle, IntPtr baseAddress, int size, int protection)
        {
            int prevProtection;
            if (!Native.VirtualProtectEx(processHandle, baseAddress, size, protection, out prevProtection))
            {
                throw new Exception($"Failed to protect memory to {protection} in the process");
            }

            return prevProtection;
        }
    }
}
