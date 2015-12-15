﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.Core
{
    public class DataItem
    {
        /// <summary>
        /// Byte array of acquired data
        /// </summary>
        public byte[] DataBytes { get; set; }

        /// <summary>
        /// Description and remark of acquired data
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Size of acquired data
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Time when the data is created
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Identifier of the device that was acquiring the data
        /// </summary>
        public string DeviceID { get; set; }

        /// <summary>
        /// Location or placement of the device for acquiring the data
        /// </summary>
        public string LocationID { get; set; }

        /// <summary>
        /// Parameters set when acquiring the data
        /// </summary>
        public List<DataParameter> DataParameters { get; set; }

        /// <summary>
        /// Get the value of a parameter by supplying its name
        /// </summary>
        /// <param name="ParameterName">Name of the parameter</param>
        /// <returns>Value of the parameter</returns>
        public string GetParameterValue(string ParameterName) 
        {
            string parameterValue = null;

            if (this.DataParameters != null)
            {
                List<DataParameter> dataParams = this.DataParameters.Where<DataParameter>(o => o.Name.ToLower() == ParameterName.ToLower()).ToList();

                if ((dataParams != null) && (dataParams.Count > 0))
                {
                    parameterValue = dataParams[0].Value;
                }
            }

            return parameterValue;
        }
    }
}
