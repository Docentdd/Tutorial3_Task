namespace Tutorial3_Task;

class Smartwatch : Device, IPowerNotify
{
    private int _batteryLevel;
    /// <summary>
    /// Battery level percentage (0-100)
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown if value is outside valid range
    /// </exception>
    public int BatteryLevel
    {
        get => _batteryLevel;
        set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException("Invalid battery level value. Must be between 0 and 100.", nameof(value));
            }
            
            _batteryLevel = value;
            if (_batteryLevel < 20)
            {
                Notify();
            }
        }
    }
    /// <summary>
    /// Initializes a new Smart Watch device
    /// </summary>
    /// <param name="batteryLevel">Initial battery level</param>
    /// <exception cref="ArgumentException">Thrown for invalid ID format</exception>
    public Smartwatch(string id, string name, bool isEnabled, int batteryLevel) : base(id, name, isEnabled)
    {
        if (CheckId(id))
        {
            throw new ArgumentException("Invalid ID value. Required format: SW-1", id);
        }
        BatteryLevel = batteryLevel;
    }

    public void Notify()
    {
        Console.WriteLine($"Battery level is low. Current level is: {BatteryLevel}");
    }

    public override void TurnOn()
    {
        if (BatteryLevel < 11)
        {
            throw new EmptyBatteryException();
        }

        base.TurnOn();
        BatteryLevel -= 10;

        if (BatteryLevel < 20)
        {
            Notify();
        }
    }

    public override string ToString()
    {
        string enabledStatus = IsEnabled ? "enabled" : "disabled";
        return $"Smartwatch {Name} ({Id}) is {enabledStatus} and has {BatteryLevel}%";
    }
    
    private bool CheckId(string id) => id.Contains("E-");
}