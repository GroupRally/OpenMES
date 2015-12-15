using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MES.Core;

namespace MES.Correlator
{
    public class Correlator : ICorrelator
    {
        public DataPair Correlate(DataIdentifier Identifier, DataItem Item) 
        {
            DataPair returnValue = null;

            ObjectCache cache = MemoryCache.Default;

            CacheItem item = cache.GetCacheItem(Identifier.DataUniqueID, ModuleConfiguration.Default_CacheRegionName);

            if (item != null)
	        {
                returnValue = item.Value as DataPair;

                returnValue.Items.Add(Item);

                return returnValue;
            }
            else
            {
                CacheItemPolicy policy = new CacheItemPolicy() 
                {
                     Priority = CacheItemPriority.NotRemovable
                };

                policy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<string>(){ModuleConfiguration.Default_CachingFilePath}));

                returnValue = new DataPair()
                {
                    Identifier = Identifier,
                    Items = new List<DataItem>() {Item}
                };

                item = new CacheItem(Identifier.DataUniqueID, returnValue, ModuleConfiguration.Default_CacheRegionName);

                cache.Add(item, policy);
            }

            return returnValue;
        }
    }
}
