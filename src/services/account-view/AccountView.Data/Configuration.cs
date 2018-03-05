using System;
using System.Collections.Generic;
using System.Text;

namespace AccountView.Data
{
    public class Configuration
    {
        public int Id { get; set; }
        public bool LoadFromEventStore { get; set; }
    }
}
