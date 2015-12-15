using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Threading;
using ImageAcquisition.Acquirer;
using ImageAcquisition.Communication;
using ImageAcquisition.Core;
using ImageAcquisition.Utility;

namespace AcquisitionStationDemo
{
    public partial class FormMain1 : Form
    {
        public FormMain1()
        {
            InitializeComponent();
        }

        public void Initialize(object deviceInstance) 
        {
            this.xiAcquirer = new XIAcquirer();
            this.xiAcquirer.SetDeviceInstance(deviceInstance);

            this.isDeviceInitialized = true;
        }

        private bool isDeviceInitialized;

        private CommunicationManager communicationManager = null;

        private XIAcquirer xiAcquirer = null;

        private List<string> acquiredImagePaths = new List<string>();

        delegate void SetControlCallBack(DataPair Pair);

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialogImageStore.ShowDialog(this) == DialogResult.OK)
            {
                this.textBoxDefaultImageStore.Text = this.folderBrowserDialogImageStore.SelectedPath;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.SetDefaultImageStore();

            this.listBoxImagesAcquiredAndSaved.DataSource = this.acquiredImagePaths;
        }

        private void SetDefaultImageStore() 
        {
            string imageStore = System.Configuration.ConfigurationManager.AppSettings.Get("DefaultImageStore").ToString();

            if (!Directory.Exists(imageStore))
            {
                try
                {
                    Directory.CreateDirectory(imageStore);
                }
                catch (Exception ex)
                {
                }
            }

            this.textBoxDefaultImageStore.Text = imageStore;
        }

        private void startListener() 
        {
            this.communicationManager = new CommunicationManager() 
            {
                 LocalAddress = ConfigurationManager.AppSettings.Get("LocalAddress"),
                 RemoteAddress = ConfigurationManager.AppSettings.Get("RemoteAddress"),
                 LocalPort = int.Parse(ConfigurationManager.AppSettings.Get("LocalPort")),
                 RemotePort = int.Parse(ConfigurationManager.AppSettings.Get("RemotePort")),
                 TimeToLive = int.Parse(ConfigurationManager.AppSettings.Get("TimeToLive"))
            };

            this.communicationManager.Start();

            byte[] bytesReceived = null;

            int bytesCount = -1;

            while (true)
            {
               bytesReceived = this.communicationManager.Receive(out bytesCount);

               if ((bytesCount > 0) && (bytesReceived != null) && (bytesReceived.Length > 0))
               {
                   DataIdentifier identifier = CommonUtility.BinaryDeserialize(bytesReceived) as DataIdentifier; //JsonUtility.JsonDeserialize(bytesReceived, typeof(DataIdentifier), null, null) as DataIdentifier;

                   DataPair pair = this.doAcquisition(identifier);

                   this.saveDataPair(pair);

                   Thread thread = new Thread(this.threadProcSafe);

                   thread.Start(pair);
               }
            }
        }

        private void startAcquisition() 
        {
            if (this.xiAcquirer == null)
            {
                this.xiAcquirer = new XIAcquirer();
            }

            this.xiAcquirer.Open();

            //this.xiAcquirer.SetDeviceInstance(this.getDeviceInstance());

            this.xiAcquirer.Start();
        }

        //private object getDeviceInstance() 
        //{
        //    object deviceInstance = null;

        //    byte[] deviceInstanceBytes = null;

        //    string deviceInstanceStateLocation = ConfigurationManager.AppSettings.Get("DeviceInstanceStateLocation");

        //    deviceInstanceStateLocation = deviceInstanceStateLocation.EndsWith("\\") ? deviceInstanceStateLocation.Substring(0, (deviceInstanceStateLocation.Length - 1)) : deviceInstanceStateLocation;

        //    if (!Directory.Exists(deviceInstanceStateLocation))
        //    {
        //        Directory.CreateDirectory(deviceInstanceStateLocation);
        //    }

        //    string deviceInstanceStateResourceName = ConfigurationManager.AppSettings.Get("DefaultDeviceInstanceStateResourceName");

        //    string filePath = String.Format("{0}\\{1}", deviceInstanceStateLocation, deviceInstanceStateResourceName);

        //    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        //    {
        //        deviceInstanceBytes = new byte[fileStream.Length];
        //        fileStream.Read(deviceInstanceBytes, 0, deviceInstanceBytes.Length);
        //    }

        //    if (deviceInstanceBytes != null)
        //    {
        //        deviceInstance = CommonUtility.BinaryDeserialize(deviceInstanceBytes);
        //    }

        //    return deviceInstance;
        //}

        private DataPair doAcquisition(DataIdentifier identifier) 
        {
            DataItem dataItem = this.xiAcquirer.Acquire();

            DataPair pair = new DataPair()
            {
                Identifier = identifier,
                Items = new List<DataItem>() {dataItem}
            };

            return pair;
        }

        private void stopAcquisition() 
        {
            if (this.xiAcquirer != null)
            {
                this.xiAcquirer.Stop();
                this.xiAcquirer.Close();
            }
        }

        private void saveDataPair(DataPair pair) 
        {
            string imageStore = this.textBoxDefaultImageStore.Text;

            imageStore = imageStore.EndsWith("\\") ? imageStore.Substring(0, (imageStore.Length - 1)) : imageStore;

            string imageDirectory = String.Format("{0}\\{1}", imageStore, pair.Identifier.DataUniqueID);

            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            string filePath;

            FileStream fileStream;

            foreach (DataItem item in pair.Items)
            {
                filePath = String.Format("{0}\\{1}.bmp", imageDirectory, item.CreationTime.ToString("yyyy-MM-dd-hh-mm-ss"));

                fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write);

                fileStream.Write(pair.Items[0].DataBytes, 0, pair.Items[0].DataBytes.Length);

                fileStream.Flush();

                fileStream.Close();

                this.acquiredImagePaths.Add(filePath);
            }
        }

        // This method is executed on the worker thread and makes 
        // a thread-safe call on the controls. 
        private void threadProcSafe(object pair)
        {
            this.setControls(pair as DataPair);
        }

        private void setControls(DataPair pair) 
        {
            if (this.pictureBoxImagePreview.InvokeRequired)
            {
                SetControlCallBack setControlCallBack = new SetControlCallBack(this.setControls);
                this.Invoke(setControlCallBack, pair);
            }
            else
            {
                this.pictureBoxImagePreview.Image = Bitmap.FromStream(new MemoryStream(pair.Items[0].DataBytes));
                this.pictureBoxImagePreview.Refresh();
            }

            if (this.listBoxImagesAcquiredAndSaved.InvokeRequired)
            {
                SetControlCallBack setControlCallBack = new SetControlCallBack(this.setControls);
                this.Invoke(setControlCallBack, pair);
            }
            else
            {
                this.listBoxImagesAcquiredAndSaved.Refresh();
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //this.startListener();

            Thread thread = new Thread(new ThreadStart(this.startListener));

            thread.SetApartmentState(ApartmentState.MTA);

            thread.Start();

            if (!this.isDeviceInitialized)
            {
                this.startAcquisition();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.stopAcquisition();

            if (this.communicationManager != null)
            {
                this.communicationManager.Close();
            }
        }
    }
}
