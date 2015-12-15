using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MES.Core;

namespace MES.Detector
{
    public class TimerDetector : DetectorBase
    {
        private Timer timer;

        private bool isTesting = false;

        public event TimerDetectorCallBack TimerCallBack;

        public override void Detect()
        {
            //base.Detect();

            this.timer = new Timer()
            {
                AutoReset = true,
                Interval = ModuleConfiguration.Default_TimerInterval
            };

            this.timer.Elapsed += timer_Elapsed;

            this.timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string data = e.SignalTime.ToString("yyyy-MM-dd-hh-mm-ss-ffffzzz").Replace("+", "A").Replace(":", "-");//e.SignalTime.ToLongDateString();

            DataIdentifier dataIdentifier = new DataIdentifier() 
            {
                DataUniqueID = Guid.NewGuid().ToString(),
                RawData = new object[]{data}
            };

            if (!this.isTesting)
            {
                base.Notify(dataIdentifier);
            }

            if (this.TimerCallBack != null)
            {
                this.TimerCallBack(this, new TimerDetectorEventArgs() { DataIdentifier = dataIdentifier });
            }
            //else
            //{
            //    base.Notify(dataIdentifier);
            //}
        }

        public override void Stop()
        {
            base.Stop();

            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer.Close();
            }

            if (this.isTesting)
            {
                this.isTesting = false;
            }
        }

        public override byte[] Test(out object[] OutputData)
        {
            //return base.Test(out OutputData);

            this.isTesting = true;

            this.timer = new Timer()
            {
                AutoReset = true,
                Interval = ModuleConfiguration.Default_TimerInterval
            };

            this.timer.Elapsed += timer_Elapsed;

            this.timer.Start();

            //OutputData = null;

            //return null;

            return base.Test(out OutputData);
        }
    }

    public class TimerDetectorEventArgs : EventArgs
    {
        public DataIdentifier DataIdentifier{get;set;}
    }

    public delegate void TimerDetectorCallBack(object sender, TimerDetectorEventArgs e);
}
