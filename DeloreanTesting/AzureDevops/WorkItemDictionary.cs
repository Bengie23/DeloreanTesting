using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System.Collections.Concurrent;

namespace DeloreanTesting.AzureDevops
{
    public class WorkItemDictionary
    {
        private static readonly ConcurrentDictionary<int, WorkItem> dictionary = new ConcurrentDictionary<int, WorkItem>();
        private WorkItemDictionary()
        {
        }

        public WorkItem this[int key]
        {
            get
            {
                if (dictionary.TryGetValue(key, out WorkItem workItem))
                {
                    return workItem;    
                }
                
                throw new KeyNotFoundException("WorkItem not found");
                
            }
            set
            {
                dictionary.TryAdd(key, value);
            }
        }
    }
}
