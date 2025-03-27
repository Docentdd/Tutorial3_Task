namespace Tutorial3_Task;

public interface IDeviceParserSOLID
{
    Device Parse(string line);
}
public static class GlobalVariables
{
    public const int MinimumRequiredElements = 4;
    public const int IndexPosition = 0;
    public const int DeviceNamePosition = 1;
    public const int EnabledStatusPosition = 2;
}

public class DeviceParserSOLID
{
    private readonly Dictionary<string, IDeviceParserSOLID> _parsers = new()
    {
        { "P-", new PersonalComputerParser() },
        { "SW-", new SmartwatchParser() },
        { "ED-", new EmbeddedParser() }
    };
    /// <summary>
    /// Parses a CSV line into a device object
    /// </summary>
    /// <param name="line">CSV line to parse</param>
    /// <returns>Initialized Device object</returns>
    /// <exception cref="ArgumentException">Thrown for invalid data format</exception>
    public Device ParseDevice(string line)
    {
        foreach (var parser in _parsers)
        {
            if (line.StartsWith(parser.Key))
            {
                return parser.Value.Parse(line);
            }
        }

        throw new ArgumentException("Unknown device type");
    }

// SmartwatchParser.cs
    public class SmartwatchParser : IDeviceParserSOLID
    {
        private const int BatteryPosition = 3;

        public Device Parse(string line)
        {
            var infoSplits = line.Split(',');

            if (infoSplits.Length < GlobalVariables.MinimumRequiredElements)
            {
                throw new ArgumentException($"Corrupted line ", line);
            }

            if (bool.TryParse(infoSplits[GlobalVariables.EnabledStatusPosition], out bool _) is false)
            {
                throw new ArgumentException($"Corrupted line : can't parse enabled status for smartwatch.",
                    line);
            }

            if (int.TryParse(infoSplits[BatteryPosition].Replace("%", ""), out int _) is false)
            {
                throw new ArgumentException($"Corrupted line : can't parse battery level for smartwatch.",
                    line);
            }

            return new Smartwatch(infoSplits[GlobalVariables.IndexPosition],
                infoSplits[GlobalVariables.DeviceNamePosition],
                bool.Parse(infoSplits[GlobalVariables.EnabledStatusPosition]),
                int.Parse(infoSplits[BatteryPosition].Replace("%", "")));
        }
    }

// PersonalComputerParser.cs
    public class PersonalComputerParser : IDeviceParserSOLID
    {
        private const int SystemPosition = 3;

        public Device Parse(string line)
        {
            var infoSplits = line.Split(',');

            if (infoSplits.Length < GlobalVariables.MinimumRequiredElements)
            {
                throw new ArgumentException($"Corrupted line ", line);
            }

            if (bool.TryParse(infoSplits[GlobalVariables.EnabledStatusPosition], out bool _) is false)
            {
                throw new ArgumentException($"Corrupted line : can't parse enabled status for computer.",
                    line);
            }

            return new PersonalComputer(infoSplits[GlobalVariables.IndexPosition],
                infoSplits[GlobalVariables.DeviceNamePosition],
                bool.Parse(infoSplits[GlobalVariables.EnabledStatusPosition]), infoSplits[SystemPosition]);
        }
    }

// EmbeddedParser.cs
    public class EmbeddedParser : IDeviceParserSOLID
    {
        private const int IpAddressPosition = 3;
        private const int NetworkNamePosition = 4;

        public Device Parse(string line)
        {
            var infoSplits = line.Split(',');

            if (infoSplits.Length < GlobalVariables.MinimumRequiredElements)
            {
                throw new ArgumentException($"Corrupted line ", line);
            }

            if (bool.TryParse(infoSplits[GlobalVariables.EnabledStatusPosition], out bool _) is false)
            {
                throw new ArgumentException(
                    $"Corrupted line : can't parse enabled status for embedded device.", line);
            }

            return new Embedded(infoSplits[GlobalVariables.IndexPosition],
                infoSplits[GlobalVariables.DeviceNamePosition],
                bool.Parse(infoSplits[GlobalVariables.EnabledStatusPosition]), infoSplits[IpAddressPosition],
                infoSplits[NetworkNamePosition]);
        }
    }
}