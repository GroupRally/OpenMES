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
    /// <summary>
    /// An interface responsible for signal device manipulations and configurations
    /// </summary>
    public interface IDetector
    {
        /// <summary>
        /// Sets signal device and/or program to be active and ready for detection
        /// </summary>
        void Detect();

        /// <summary>
        /// Sends out signal to notify listeners once a target has been detected
        /// </summary>
        /// <param name="Identifier">Signal data to be sent out</param>
        void Notify(DataIdentifier Identifier);

        /// <summary>
        /// Sets signal device and/or program to be inactive and ready for closing
        /// </summary>
        void Stop();

        /// <summary>
        /// Starts a test to verify that signal device hardware is working properly and configurations are correct
        /// </summary>
        /// <param name="OutputData">Additional outputs as the product of testing</param>
        /// <returns>Outputs as the product of testing</returns>
        byte[] Test(out object[] OutputData);
    }
}
