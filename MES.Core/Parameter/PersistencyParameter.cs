using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MES.Core.Parameter
{
    public class PersistencyParameter : ParameterBase
    {
        //private string signalDataStore;
        //private string acquiredDataStore;

        private PersistencyType persistencyType = PersistencyType.FileSystem;

        private string dataStore = "C:\\ImageAcquisition";

        //public string SignalDataStore { get { return this.signalDataStore; } set { this.signalDataStore = value; } }

        //public string AcquiredDataStore { get { return this.acquiredDataStore; } set { this.acquiredDataStore = value; } }

        /// <summary>
        /// Mechanism of persistency: File System, RDBMS
        /// </summary>
        [DefaultValue(PersistencyType.FileSystem)]
        public PersistencyType PersistencyType 
        {
            get 
            {
                return this.persistencyType;
            }
            set 
            {
                this.persistencyType = value;
            }
        }

        /// <summary>
        /// The location for the data to be persisted, 
        /// can be either a path in a file system,
        /// or a DB connection string of a RDBMS
        /// </summary>
        [DefaultValue("C:\\ImageAcquisition")]
        public string DataStore 
        {
            get { return this.dataStore; }
            set { this.dataStore = value; }
        }

        public override string ToString()
        {
            //return base.ToString();

            return "Persistency";
        }
    }

    public enum PersistencyType 
    {
        FileSystem = 0,
        RDBMS = 1
    }
}
