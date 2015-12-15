using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using xiApi.NET;

namespace UnitTestExposusreEnumeration
{
    class Program
    {
        static xiCam camera;

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter duration per exposure:");

            int durationPerExposure = 30, exposureMin = 5, exposureMax = 20000;

            float gain = 5.00f;

            while (!int.TryParse(Console.ReadLine(), out durationPerExposure))
            {
                Console.WriteLine("Please enter duration per exposure:");
            }

            Console.WriteLine("Please enter min value of exposure:");

            while (!int.TryParse(Console.ReadLine(), out exposureMin))
            {
                Console.WriteLine("Please enter min value of exposure:");
            }

            Console.WriteLine("Please enter max value of exposure:");

            while (!int.TryParse(Console.ReadLine(), out exposureMax))
            {
                Console.WriteLine("Please enter max value of exposure:");
            }

            Console.WriteLine("Please enter the value of gain:");

            while (!float.TryParse(Console.ReadLine(), out gain))
            {
                Console.WriteLine("Please enter the value of gain:");
            }

            enumerateExposure(durationPerExposure, exposureMin, exposureMax, gain);

            //enumerateExposure(30, 5, 20000);

            Console.WriteLine("Press any key to exit...");

            Console.Read();
        }

        static void enumerateExposure(int durationPerExposure, int exposureMin, int exposureMax, float gain) 
        {
            camera = new xiCam();

            int numberOfDevices = camera.GetNumberDevices();

            if (numberOfDevices > 0)
            {
                camera.OpenDevice(0);

                camera.SetParam(PRM.GAIN, gain);

                camera.SetParam(PRM.IMAGE_DATA_FORMAT, xiApi.NET.IMG_FORMAT.MONO8);

                camera.StartAcquisition();

                Bitmap bitmap = null;

                string cameraStorePath = "D:\\IndustrialCameraTest\\XIMEA\\ExposureTest";

                cameraStorePath = String.Format("{0}\\{1}", cameraStorePath, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));

                if (!Directory.Exists(cameraStorePath))
                {
                    Directory.CreateDirectory(cameraStorePath);
                }

                string filePath = "";

                for (int i = exposureMin; i < (exposureMax + 1); i++)
                {
                    camera.SetParam(PRM.EXPOSURE, i);

                    for (int j = 0; j < durationPerExposure; j++)
                    {
                        camera.GetImage(out bitmap, 5000);

                        filePath = String.Format("{0}\\{1}\\{2}\\{3}", cameraStorePath, i, j, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        using (FileStream stream = new FileStream(String.Format("{0}\\{1}.bmp", filePath, Guid.NewGuid().ToString()), FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            bitmap.Save(stream, ImageFormat.Bmp);
                        }
                    }
                }

                camera.StopAcquisition();

                camera.CloseDevice();
            }
        }
    }
}
