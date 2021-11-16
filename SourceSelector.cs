using NAudio.CoreAudioApi;


public class SourceSelector
{
    private static MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
    private static List<MMDevice> deviceList = deviceEnumerator
        .EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.All).ToList();

    public static MMDevice PromptUserToSelectDevice()
    {
        Console.Out.WriteLine("Please select one of the following input devices:");
        printDeviceList();

        var idx = promtUserToInputDesiredDeviceIntoConsole();
        return deviceList[idx];
    }

    private static void printDeviceList()
    {
        var enumerator = new MMDeviceEnumerator();
        var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.All).ToList();

        for (var i = 0; i < devices.Count; i++)
        {
            var wasapi = devices[i];
            Console.Out.WriteLine($"{i})\t{wasapi.FriendlyName} ({wasapi.State})");
        }
    }
    private static int promtUserToInputDesiredDeviceIntoConsole()
    {
        String? inp;
        int? idx = null;

        do
        {
            inp = Console.In.ReadLine();
            if (inp == null) continue;

            try
            {
                idx = int.Parse(inp);
                break;
            }
            catch { }

            Console.WriteLine($"Invalid device index \"{inp}\", please enter an interger between 0, and {deviceList.Count}");
        } while (idx == null);

        return (int)idx;
    }
}