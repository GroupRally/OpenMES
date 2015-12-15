using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MES.Core
{
    [DataContract, Serializable]
    public class DataParameter
    {
        /// <summary>
        /// Name of the parameter
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Value of the parameter
        /// </summary>
        [DataMember]
        public string Value { get; set; }
    }
}
