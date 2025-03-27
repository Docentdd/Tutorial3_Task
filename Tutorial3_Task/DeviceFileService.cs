using System.Text;

namespace Tutorial3_Task;

public class DeviceFileService
{
    public void SaveDevices(string outputPath)
    {
        StringBuilder devicesSb = new();
    
        foreach (var storedDevice in _devices)
        {
            if (storedDevice is Smartwatch smartwatchCopy)
            {
                devicesSb.AppendLine($"{smartwatchCopy.Id},{smartwatchCopy.Name}," +
                                     $"{smartwatchCopy.IsEnabled},{smartwatchCopy.BatteryLevel}%");
            }
            else if (storedDevice is PersonalComputer pcCopy)
            {
                devicesSb.AppendLine($"{pcCopy.Id},{pcCopy.Name}," +
                                     $"{pcCopy.IsEnabled},{pcCopy.OperatingSystem}");
            }
            else
            {
                var embeddedCopy = storedDevice as Embedded;
                devicesSb.AppendLine($"{embeddedCopy.Id},{embeddedCopy.Name}," +
                                     $"{embeddedCopy.IsEnabled},{embeddedCopy.IpAddress}," +
                                     $"{embeddedCopy.NetworkName}");
            }
        }
        
        File.WriteAllLines(outputPath, devicesSb.ToString().Split('\n'));
    }
    
    private readonly DeviceParserSOLID _deviceParser = new DeviceParserSOLID();
    private readonly List<Device> _devices;
    
    public void LoadDevicesFromFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            try
            {
                var device = _deviceParser.ParseDevice(line);
                _devices.Add(device);
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong during parsing this line: {line}. The exception message: {ex.Message}");
            }
        }
    }
}