using System.Collections.Generic;

namespace WhatsBack.SharedKernal.ResourcesReader
{
    public class ResourceFileData
    {
        public string Key { get; set; }
        public Dictionary<string, string> LocalizedValue { get; set; }
    }
}
