using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WhatsBack.SharedKernal.Enum;

namespace WhatsBack.SharedKernal.ResourcesReader
{
    public abstract class BaseFileReader
    {
        #region Vars
        private List<ResourceFileData> ResourceData { get; set; }
        #endregion
        public BaseFileReader(LocalizationType localizationType)
        {
            LoadData(localizationType);
        }
        #region Load Data
        private void LoadData(LocalizationType localizationType)
        {
            string fileName = localizationType.ToString();
            var rootDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            ResourceData = JsonConvert.DeserializeObject<List<ResourceFileData>>(File.ReadAllText($@"{rootDir}\ResourceFiles\{fileName}.json"));
        }
        #endregion
        #region Get Data
        protected Dictionary<string, string> GetKeyValue(string key)
        {
            return ResourceData.FirstOrDefault(k => k.Key == key).LocalizedValue;
        }
        protected string GetKeyValue(string key, string culture)
        {
            if (string.IsNullOrEmpty(culture) || !System.Enum.IsDefined(typeof(Language), culture))
            {
                culture = Language.en.ToString();
            }

            string value = string.Empty;

            Dictionary<string, string> rData = ResourceData.FirstOrDefault(k => k.Key.ToLower() == key.ToLower()).LocalizedValue;

            if (rData != null)
            {
                if (rData.Count >= default(byte))
                {
                    KeyValuePair<string, string> kv = rData.ElementAt((int)System.Enum.Parse(typeof(Language), culture) - 1);
                    value = kv.Value;
                }
            }
            return value;
        }
        #endregion
    }
}
