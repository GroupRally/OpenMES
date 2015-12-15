using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Drawing;
using MES.Core;
using MES.Detector;
using MES.Utility;
using xiApi.NET;

namespace UnitTest
{
    class Program
    {
        static xiCam camera;

        static void Main(string[] args)
        {
            //DataIdentifier identifier = new DataIdentifier();

            ////DataContractJsonSerializer serializer = new DataContractJsonSerializer(identifier.GetType());

            ////MemoryStream stream = new MemoryStream();

            ////serializer.WriteObject(stream, identifier);

            ////stream.Flush();

            ////byte[] bytes = stream.GetBuffer();

            //byte[] bytes = JsonUtility.JsonSerialize(identifier, null, null);

            //string json = System.Text.Encoding.Default.GetString(bytes);

            //Console.WriteLine(json);

            ////Console.WriteLine(bytes);

            //identifier = (DataIdentifier)(JsonUtility.JsonDeserialize(bytes, typeof(DataIdentifier), null, null));

            //Console.Read();

            TestCamera();

            //object regKey = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\XIMEA\API_SoftwarePackage", "Path", "Not found!");

            //Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"\SOFTWARE\XIMEA\API_SoftwarePackage");

            //object value = key.GetValue("Path");

            //Console.WriteLine(value);

            //Console.Read();
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

                gain_db = 0;

                camera.SetParam(PRM.GAIN, gain_db);

                // Set image output format to monochrome 8 bit
                //camera.SetParam(PRM.IMAGE_DATA_FORMAT, IMG_FORMAT.MONO8);

                camera.SetParam(PRM.IMAGE_DATA_FORMAT, IMG_FORMAT.RAW8);

                camera.SetParam(PRM.AEAG, 1);

                camera.SetParam(PRM.EXPOSURE_MAX, 200);
                camera.SetParam(PRM.GAIN_MAX, 16);
                camera.SetParam(PRM.AEAG_LEVEL, 40);

                camera.StartAcquisition();

                Console.WriteLine(camera.GetParamString(xiApi.NET.PRM.DEVICE_NAME));

                Console.WriteLine(camera.GetParamString(xiApi.NET.PRM.DEVICE_SN));

                Console.WriteLine(camera.GetParamString(xiApi.NET.PRM.DEVICE_INSTANCE_PATH));

                Console.WriteLine(camera.GetParamString(xiApi.NET.PRM.DEVICE_TYPE));

                Console.WriteLine(camera.GetParamString(PRM.DOWNSAMPLING));

                Console.WriteLine(camera.GetParamString(PRM.DOWNSAMPLING_TYPE));

                Console.WriteLine(camera.GetParamString(PRM.WIDTH));

                Console.WriteLine(camera.GetParamString(PRM.HEIGHT));

                Console.WriteLine(camera.GetParamString(PRM.FRAMERATE));

                Console.WriteLine(camera.GetParamString(PRM.AUTO_BANDWIDTH_CALCULATION));

                Console.WriteLine(camera.GetParamString(PRM.BUFFER_POLICY));

                Console.WriteLine(camera.GetParamString(PRM.BPC));

                Console.WriteLine(camera.GetParamString(PRM.LIMIT_BANDWIDTH));

                Console.WriteLine(camera.GetParamString(PRM.AVAILABLE_BANDWIDTH));

                Console.WriteLine(camera.GetParamInt(PRM.OFFSET_X));

                Console.WriteLine(camera.GetParamInt(PRM.OFFSET_Y));

                string command = "y";

                //string defaultBufferSize = camera.GetParamString(PRM.ACQ_BUFFER_SIZE);

                //int bufferSize = 53248000;//int.Parse(defaultBufferSize);

                //byte[] imageBytes = new byte[bufferSize];

                //MemoryStream stream;

                FileStream fileStream;

                Bitmap bitmap = null;

                //Image image;

                while (command.ToLower() == "y")
                {
                    //camera.GetImage(imageBytes, 5000);

                    //stream = new MemoryStream();

                    //stream.Write(imageBytes, 0, imageBytes.Length);

                    //image = Image.FromStream(stream);

                    //bitmap = new Bitmap(image);

                    camera.GetImage(out bitmap, 5000);

                    string filePath = String.Format("e:\\ximeaCameraTest\\{0}", DateTime.Now.ToString("yyyy-MM-dd"));

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
