namespace Tutorial3_Task;

public class DeviceRepository
{
    private const int MaxCapacity = 15;
    private List<Device> _devices = new(capacity: MaxCapacity);
    /// <summary>
    /// Adds a new device to the repository
    /// </summary>
    /// <param name="newDevice">Device to add</param>
    /// <exception cref="ArgumentException">Thrown if device ID already exists</exception>
    /// <exception cref="Exception">Thrown when repository is full</exception>
    public void AddDevice(Device newDevice)
    {
        foreach (var storedDevice in _devices)
        {
            if (storedDevice.Id.Equals(newDevice.Id))
            {
                throw new ArgumentException($"Device with ID {storedDevice.Id} is already stored.", nameof(newDevice));
            }
        }

        if (_devices.Count >= MaxCapacity)
        {
            throw new Exception("Device storage is full.");
        }
        
        _devices.Add(newDevice);
    }
    /// <summary>
    /// Updates an existing device with matching ID
    /// </summary>
    /// <param name="editDevice">Modified device data</param>
    /// <exception cref="ArgumentException">
    /// Thrown if device not found or type mismatch
    /// </exception>
    public void EditDevice(Device editDevice)
    {
        var targetDeviceIndex = _devices.FindIndex(device => device.Id.Equals(editDevice.Id));

        if (targetDeviceIndex == -1)
        {
            throw new ArgumentException($"Device with ID {editDevice.Id} is not stored.", nameof(editDevice));
        }
        
        var editDeviceType = editDevice.GetType().Name;
        var targetDevice = _devices[targetDeviceIndex];
        
        if (editDeviceType != targetDevice.GetType().Name)
        {
            throw new ArgumentException($"Type mismatch between devices. " +
                                        $"Target device has type {targetDevice.GetType().Name}");
        }
        _devices[targetDeviceIndex] = editDevice;

        // if (editDevice is Smartwatch)
        // {
        //     if (_devices[targetDeviceIndex] is Smartwatch)
        //     {
        //         _devices[targetDeviceIndex] = editDevice;
        //     }
        //     else
        //     {
        //         throw new ArgumentException($"Type mismatch between devices. " +
        //                                     $"Target device has type {_devices[targetDeviceIndex].GetType().Name}");
        //     }
        // }
        //
        // if (editDevice is PersonalComputer)
        // {
        //     if (_devices[targetDeviceIndex] is PersonalComputer)
        //     {
        //         _devices[targetDeviceIndex] = editDevice;
        //     }
        //     else
        //     {
        //         throw new ArgumentException($"Type mismatch between devices. " +
        //                                     $"Target device has type {_devices[targetDeviceIndex].GetType().Name}");
        //     }
        // }
        //
        // if (editDevice is Embedded)
        // {
        //     if (_devices[targetDeviceIndex] is Embedded)
        //     {
        //         _devices[targetDeviceIndex] = editDevice;
        //     }
        //     else
        //     {
        //         throw new ArgumentException($"Type mismatch between devices. " +
        //                                     $"Target device has type {_devices[targetDeviceIndex].GetType().Name}");
        //     }
        // }
    }
    public void RemoveDeviceById(string deviceId)
    {
        // Device? targetDevice = null;
        var targetDeviceIndex = _devices.FindIndex(device => device.Id.Equals(deviceId));
        // foreach (var storedDevice in _devices)
        // {
        //     if (storedDevice.Id.Equals(deviceId))
        //     {
        //         targetDevice = storedDevice;
        //         break;
        //     }
        // }
        if (_devices[targetDeviceIndex] == null)
        {
            throw new ArgumentException($"Device with ID {deviceId} is not stored.", nameof(deviceId));
        }
        _devices.Remove(_devices[targetDeviceIndex]);
    }
    public Device? GetDeviceById(string id)
    {
        foreach (var storedDevice in _devices)
        {
            if (storedDevice.Id.Equals(id))
            {
                return storedDevice;
            }
        }

        return null;
    }
    
    public void ShowAllDevices()
    {
        foreach (var storedDevices in _devices)
        {
            Console.WriteLine(storedDevices.ToString());
        }
    }
}