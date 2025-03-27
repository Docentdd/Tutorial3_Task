namespace Tutorial3_Task;

public class DeviceController
{
    private readonly DeviceRepository _deviceRepository;

    public DeviceController(DeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }
    /// <summary>
    /// Turns off a device by ID
    /// </summary>
    /// <param name="id">Device identifier</param>
    /// <exception cref="ArgumentException">Thrown if device not found</exception>
    public void TurnOffDevice(string id)
    {
        if (_deviceRepository.GetDeviceById(id) is null)
        {
            throw new ArgumentException($"Device with ID {id} is not stored.");
        }
        var device = _deviceRepository.GetDeviceById(id);
        device.TurnOff();
    }
    /// <summary>
    /// Turns on a device by ID 
    /// </summary>
    /// <param name="id">Device identifier</param>
    /// <exception cref="KeyNotFoundException">Thrown if device not found</exception>
    public void TurnOnDevice(string id)
    {
        if (_deviceRepository.GetDeviceById(id) is null)
        {
            throw new ArgumentException($"Device with ID {id} is not stored.");
        }
        var device = _deviceRepository.GetDeviceById(id);
        device.TurnOn();
    }
}