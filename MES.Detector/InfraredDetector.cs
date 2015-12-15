using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using ImageAcquisition.Core;

namespace ImageAcquisition.Detector
{
    public class InfraredDetector : DetectorBase
    {
        private KinectSensor depthSensor;

        //The value that could be configured to reflect the actual distance (in millimeters) between the object to the depth sensor
        public static int DefaultDepth = ModuleConfiguration.Default_InfraredDepth;

        public override void Detect()
        {
            //base.Detect();

            foreach (KinectSensor sensor in KinectSensor.KinectSensors)//Loops all of the sensors of the Kinect device connected 
            {
                if (sensor.Status == KinectStatus.Connected)//If the current sensor in with state of "Connected" 
                {
                    //Takes the current sensor as the depth sensor 
                    //(of cource, in this case, the image sensor and the microphone array should all be turned of)
                    this.depthSensor = sensor;

                    break;
                }
            }

            if (this.depthSensor != null)
            {
                this.depthSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);

                //Attaches an event handler to the depth sensor's FrameReady event, which fires when a depth frame is captured
                this.depthSensor.DepthFrameReady += new EventHandler<DepthImageFrameReadyEventArgs>(depthSensor_DepthFrameReady);

                //Starts the depth sensor
                this.depthSensor.Start();
            }
        }

        public override void Stop()
        {
            //base.Stop();

            if (this.depthSensor != null)
            {
                this.depthSensor.Stop();
            }
        }

        /// <summary>
        /// Fires when a depth frame is captured by the Kinect's depth sensor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void depthSensor_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (DepthImageFrame frame = e.OpenDepthImageFrame())//Opens the depth frame sent along with the event argument
            {
                //Reads out all of the captured depth image pixels to an array
                DepthImagePixel[] depthImagePixels = frame.GetRawPixelData();

                for (int i = 0; i < depthImagePixels.Length; i++)//Loops the captured depth image pixel array 
                {
                    if (depthImagePixels[i].Depth == DefaultDepth)//If the depth (the distance, in millimeters, from the image pixel to the sensor) meets the configured value, then:
                    {
                        //Generates an unqiue obejct ID for the current object
                        DataIdentifier dataIdentifier = new DataIdentifier() { DataUniqueID = Guid.NewGuid().ToString()};

                        //Notifies the receivers
                        base.Notify(dataIdentifier);

                        //break;
                    }
                }
            }
        }

    }
}
