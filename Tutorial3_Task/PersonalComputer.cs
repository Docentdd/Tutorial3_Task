namespace Tutorial3_Task;

class PersonalComputer : Device
{
    public string? OperatingSystem { get; set; }
    /// <summary>
    /// Initializes a new personal computer device
    /// </summary>
    /// <param name="operatingSystem">
    /// OS name (null allowed but prevents device activation)
    /// </param>
    /// <exception cref="ArgumentException">Thrown for invalid ID format</exception>
    public PersonalComputer(string id, string name, bool isEnabled, string? operatingSystem) : base(id, name, isEnabled)
    {
        if (!CheckId(id))
        {
            throw new ArgumentException("Invalid ID value. Required format: P-1", id);
        }
        
        OperatingSystem = operatingSystem;
    }

    public override void TurnOn()
    {
        if (OperatingSystem is null)
        {
            throw new EmptySystemException();
        }

        base.TurnOn();
    }

    public override string ToString()
    {
        string enabledStatus = IsEnabled ? "enabled" : "disabled";
        string osStatus = OperatingSystem is null ? "has not OS" : $"has {OperatingSystem}";
        return $"PC {Name} ({Id}) is {enabledStatus} and {osStatus}";
    }

    private bool CheckId(string id) => id.Contains("P-");
}