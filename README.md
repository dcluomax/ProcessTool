# ProcessTool

[in progress] A C# version of tool set.

# Ctor
```csharp
ProcessTool pt = new ProcessTool(processId);

ProcessTool pt = new ProcessTool(processName);

ProcessTool pt = new ProcessTool(path,args,windowstyle?);
```
# Methods
```csharp
// Read
byte[] data = pt.ReadMemory(baseAddress, size);

Food sushi = pt.ReadObject<Food>(baseAddress);

// Write
bool success = pt.Write<Food>(baseAddress,sushi);

bool success = pt.Write(baseAddress,15);

bool success = pt.Write(baseAddress,15.1);

bool success = pt.Write(baseAddress,"string");

int OldProtection = pt.ProtectMemory(baseAddress, size, newProtection);
```
