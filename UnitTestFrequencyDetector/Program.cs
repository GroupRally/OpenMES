using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using xiApi.NET;

namespace UnitTestFrequencyDetector
{
    class Program
    {
        static xiCam camera;

        static void Main(string[] args)
        {
            DetectFrequency();

            //TestCamera();
        }

        static void DetectFrequency() 
        {
            camera = new xiCam();

            try
            {
                int cameraCount = camera.GetNumberDevices();

                camera.OpenDevice(cameraCount - 1);

                //camera.SetParam(PRM.BUFFER_POLICY, BUFF_POLICY.SAFE);

                // Set device exposure to 2 milliseconds(2000 microseconds(us))
                int exposure_us = 2000;

                exposure_us = 1975;

                //exposure_us = 800;//50;//150;//200;

                //exposure_us = 11;//291;//369;

                camera.SetParam(PRM.EXPOSURE, exposure_us);

                // Set device gain to 5 decibels
                float gain_db = 5;

                //gain_db = 30;

                //gain_db = 28.7f;//28.3f;

                camera.SetParam(PRM.GAIN, gain_db);

                // Set image output format to monochrome 8 bit
                camera.SetParam(PRM.IMAGE_DATA_FORMAT, IMG_FORMAT.MONO8);

                //camera.SetParam(PRM.IMAGE_DATA_FORMAT, IMG_FORMAT.RAW8);

                //camera.SetParam(PRM.AEAG, 1);

                ////camera.SetParam(PRM.EXPOSURE_MAX, 200);
                //camera.SetParam(PRM.EXPOSURE_MAX, 1);
                ////camera.SetParam(PRM.GAIN_MAX, 16);
                //camera.SetParam(PRM.GAIN_MAX, 25);
                //camera.SetParam(PRM.AEAG_LEVEL, 40);

                camera.StartAcquisition();

                Console.WriteLine(camera.GetParamString(xiApi.NET.PRM.DEVICE_NAME));

                Console.WriteLine(camera.GetParamString(xiApi.NET.PRM.DEVICE_SN));

                string command = "y";

                //string defaultBufferSize = camera.GetParamString(PRM.ACQ_BUFFER_SIZE);

                //int bufferSize = 53248000;//int.Parse(defaultBufferSize);

                //byte[] imageBytes = new byte[bufferSize];

                //MemoryStream stream;

                FileStream fileStream;

                Bitmap bitmap = null;

                System.Collections.ArrayList barcodes = new System.Collections.ArrayList();

                int scanningTimes = 75;

                //Image image;

                while (command.ToLower() == "y")
                {
                    //camera.GetImage(imageBytes, 5000);

                    //stream = new MemoryStream();

                    //stream.Write(imageBytes, 0, imageBytes.Length);

                    //image = Image.FromStream(stream);

                    //bitmap = new Bitmap(image);

                    camera.GetImage(out bitmap, 5000);

                    //bitmap = BarcodeImaging.RotateImage(bitmap, 1);

                    barcodes = new System.Collections.ArrayList();

                    BarcodeImaging.FullScanPage(ref barcodes, bitmap, scanningTimes);

                    //if ((barcodes != null) && (barcodes.Count > 0))
                    {
                        Console.WriteLine(DateTime.Now);

                        for (int i = 0; i < barcodes.Count; i++)
                        {
                            Console.WriteLine(barcodes[i]);
                        }

                        string filePath = String.Format("d:\\ximeaCameraTest\\{0}", DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));

                        //string filePath = String.Format("d:\\ximeaCameraTest\\{0}", DateTime.Now.ToString("yyyy-MM-dd"));

                        Directory.CreateDirectory(filePath);

                        fileStream = new FileStream(String.Format("{0}\\{1}.bmp", filePath, Guid.NewGuid().ToString()), FileMode.Create, FileAccess.Write, FileShare.Write);

                        bitmap.Save(fileStream, System.Drawing.Imaging.ImageFormat.Bmp);

                        fileStream.Flush();

                        fileStream.Close();
                    }

                    //Console.WriteLine("Continue? (Y/N)");

                    //command = Console.ReadLine();
                }

                camera.StopAcquisition();

                camera.CloseDevice();

                Console.WriteLine("Press any key to exit...");

                Console.Read();
            }
            catch (Exception ex)
            {
                camera.CloseDevice();
                Console.WriteLine(ex.ToString());
                Console.Read();
            }
            finally
            {
                camera.CloseDevice();
            }
        }

        static void TestCamera()
        {
            camera = new xiCam();

            try
            {
                int cameraCount = camera.GetNumberDevices();

                camera.OpenDevice(cameraCount - 1);

                //camera.SetParam(PRM.BUFFER_POLICY, BUFF_POLICY.SAFE);

                // Set device exposure to 2 milliseconds
                int exposure_us = 2000;

                exposure_us = 200;

                camera.SetParam(PRM.EXPOSURE, exposure_us);

                // Set device gain to 5 decibels
                float gain_db = 5;

                gain_db = 28.3f;//0;

                camera.SetParam(PRM.GAIN, gain_db);

                // Set image output format to monochrome 8 bit
                camera.SetParam(PRM.IMAGE_DATA_FORMAT, IMG_FORMAT.MONO8);

                //camera.SetParam(PRM.IMAGE_DATA_FORMAT, IMG_FORMAT.RAW8);

                //camera.SetParam(PRM.AEAG, 1);

                //camera.SetParam(PRM.EXPOSURE_MAX, 200);
                //camera.SetParam(PRM.GAIN_MAX, 16);
                //camera.SetParam(PRM.AEAG_LEVEL, 40);

                camera.StartAcquisition();

                Console.WriteLine(camera.GetParamString(xiApi.NET.PRM.DEVICE_NAME));

                Console.WriteLine(camera.GetParamString(xiApi.NET.PRM.DEVICE_SN));

                string command = "y";

                //string defaultBufferSize = camera.GetParamString(PRM.ACQ_BUFFER_SIZE);

                //int bufferSize = 53248000;//int.Parse(defaultBufferSize);

                //byte[] imageBytes = new byte[bufferSize];

                //MemoryStream stream;

                FileStream fileStream;

                Bitmap bitmap = null;

                //Image image;

                string filePath = "";

                while (command.ToLower() == "y")
                {
                    //camera.GetImage(imageBytes, 5000);

                    //stream = new MemoryStream();

                    //stream.Write(imageBytes, 0, imageBytes.Length);

                    //image = Image.FromStream(stream);

                    //bitmap = new Bitmap(image);

                    camera.GetImage(out bitmap, 5000);

                    filePath = String.Format("d:\\ximeaCameraTest\\{0}", DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));

                    Directory.CreateDirectory(filePath);

                    fileStream = new FileStream(String.Format("{0}\\{1}.bmp", filePath, Guid.NewGuid().ToString()), FileMode.Create, FileAccess.Write, FileShare.Write);

                    bitmap.Save(fileStream, System.Drawing.Imaging.ImageFormat.Bmp);

                    fileStream.Flush();

                    fileStream.Close();

                    Console.WriteLine("Continue? (Y/N)");

                    command = Console.ReadLine();
                }

                camera.StopAcquisition();

                camera.CloseDevice();

                Console.WriteLine("Press any key to exit...");

                Console.Read();
            }
            catch (Exception ex)
            {
                camera.CloseDevice();
                Console.WriteLine(ex.ToString());
                Console.Read();
            }
            finally
            {
                camera.CloseDevice();
            }
        }
    }
}
