using System;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision.Motion;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting motion detection...");
        StartCamera();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        StopCamera();
    }

    static VideoCaptureDevice videoSource = null;
    static MotionDetector motionDetector = null;

    static void StartCamera()
    {
        var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        if (videoDevices.Count == 0)
            throw new ApplicationException("No video devices found");

        try
        {
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();

            motionDetector = new MotionDetector(
                new TwoFramesDifferenceDetector(),
                new MotionBorderHighlighting());

            Console.WriteLine("Camera started successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error initializing camera: " + ex.Message);
        }
    }

    static void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        using (var bitmap = (System.Drawing.Bitmap)eventArgs.Frame.Clone())
        {
            float motionLevel = motionDetector.ProcessFrame(bitmap);
            if (motionLevel > 0) // Adjust sensitivity as needed
            {
                Console.WriteLine($"{motionLevel.ToString()}");
            }
        }
    }

    static void StopCamera()
    {
        if (videoSource != null && videoSource.IsRunning)
        {
            videoSource.SignalToStop();
            videoSource.WaitForStop();
            videoSource = null;
            Console.WriteLine("Camera stopped.");
        }
    }
}
