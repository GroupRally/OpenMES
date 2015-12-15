using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MES.Core.Parameter
{
    public class TimerParameter : ParameterBase
    {
        private int timerInterval = 8000;

        /// <summary>
        /// The interval in milliseconds that determines how often a timer is trigered
        /// </summary>
        [DefaultValue(8000)]
        public int TimerInterval { get { return this.timerInterval; } set { this.timerInterval = value; } }

        public override string ToString()
        {
            //return base.ToString();

            return "Timer";
        }
    }
}
