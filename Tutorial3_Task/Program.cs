

namespace Tutorial3_Task;

class EmptySystemException : Exception
{
    public EmptySystemException() : base("Operation system is not installed.") { }
}

class EmptyBatteryException : Exception
{
    public EmptyBatteryException() : base("Battery level is too low to turn it on.") { }
}

class ConnectionException : Exception
{
    public ConnectionException() : base("Wrong netowrk name.") { }
}

interface IPowerNotify
{
    void Notify();
}





class Program
{
    public static void Main()
    {
        try
        {
            
            DeviceRepository repository = new DeviceRepository();
            DeviceController controller = new DeviceController(repository);
            DeviceParserSOLID parser = new DeviceParserSOLID();
            
            string[] lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                try
                {
                    Device device = parser.ParseDevice(line);
                    repository.AddDevice(device);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line: {line}. Message: {ex.Message}");
                }
            }

            Console.WriteLine("Devices presented after file read.");
            repository.ShowAllDevices();

            // Add new PC
            Console.WriteLine("Create new computer and add it to device store.");
            PersonalComputer computer = new PersonalComputer("P-2", "ThinkPad T440", false, null);
            repository.AddDevice(computer);

            Console.WriteLine("Let's try to enable this PC");
            try
            {
                controller.TurnOnDevice("P-2");
            }
            catch (EmptySystemException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.WriteLine("Devices presented after all operations.");
            repository.ShowAllDevices();
            
            // Edit PC to add OS
            Console.WriteLine("Let's install OS for this PC");
            PersonalComputer editComputer = new PersonalComputer("P-2", "ThinkPad T440", true, "Arch Linux");
            repository.EditDevice(editComputer);
            Console.WriteLine("_________________________");
            Console.WriteLine("Devices presented after all operations.");
            repository.ShowAllDevices();

            Console.WriteLine("Let's try to enable this PC");
            controller.TurnOnDevice("P-2");
            Console.WriteLine("Devices presented after all operations.");
            repository.ShowAllDevices();
            Console.WriteLine("Let's turn off this PC");
            controller.TurnOffDevice("P-2");
            Console.WriteLine("Devices presented after all operations.");
            repository.ShowAllDevices();
            
            // Remove PC
            Console.WriteLine("Delete this PC");
            repository.RemoveDeviceById("P-2");

            Console.WriteLine("Devices presented after all operations.");
            repository.ShowAllDevices();

            // Save devices to file (if needed, requires adjusting DeviceFileService)
            // fileService.SaveDevices("output.txt");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

// class Program
// {
//     public static void Main()
//     {
//         try
//         {
//             DeviceManager deviceManager = new("input.txt");
//             // DeviceRepository repository = new DeviceRepository();
//             // DeviceFileService deviceFileService = new();
//             // deviceFileService.LoadDevicesFromFile("input.txt");
//             
//             
//             
//             Console.WriteLine("Devices presented after file read.");
//             deviceManager.ShowAllDevices();
//             
//             Console.WriteLine("Create new computer with correct data and add it to device store.");
//             {
//                 PersonalComputer computer = new("P-2", "ThinkPad T440", false, null);
//                 deviceManager.AddDevice(computer);
//             }
//             
//             Console.WriteLine("Let's try to enable this PC");
//             try
//             {
//                 deviceManager.TurnOnDevice("P-2");
//             }
//             catch (EmptySystemException ex)
//             {
//                 Console.WriteLine(ex.Message);
//             }
//             
//             Console.WriteLine("Let's install OS for this PC");
//             
//             PersonalComputer editComputer = new("P-2", "ThinkPad T440", true, "Arch Linux");
//             deviceManager.EditDevice(editComputer);
//             
//             Console.WriteLine("Let's try to enable this PC");
//             deviceManager.TurnOnDevice("P-2");
//             
//             Console.WriteLine("Let's turn off this PC");
//             deviceManager.TurnOffDevice("P-2");
//             
//             Console.WriteLine("Delete this PC");
//             deviceManager.RemoveDeviceById("P-2");
//             
//             Console.WriteLine("Devices presented after all operations.");
//             deviceManager.ShowAllDevices();
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine(ex.Message);
//         }
//     }
// }