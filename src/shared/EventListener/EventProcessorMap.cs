using System;
using System.Collections.Generic;
using System.Text;

namespace EventListener
{
    public class EventProcessorMap
    {
        IDictionary<string, IProcessEvents> maps;

        public EventProcessorMap() : this(new Dictionary<string, IProcessEvents>())
        {
        }

        public EventProcessorMap(IDictionary<string, IProcessEvents> maps)
        {
            this.maps = maps;
        }

        public IEnumerable<string> Keys => this.maps.Keys;

        public IEnumerable<IProcessEvents> Values => this.maps.Values;

        public IProcessEvents this[string key]
        {
            get
            {
                return maps[key];
            }
        }

        public void Add(string key, IProcessEvents value)
        {
            this.maps.Add(key, value);
        }

        public void Remove(string key)
        {
            this.maps.Remove(key);
        }

    }
}
