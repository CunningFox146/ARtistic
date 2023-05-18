using UnityEngine;

namespace Services.PersistentData
{
    public class PlayerPrefsPersistentData : IPersistentData
    {
        public void SetValue(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
        
        public string GetValue(string key)
        {
            return HasValue(key) ? PlayerPrefs.GetString(key) : null;
        }
        
        public void RemoveValue(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public bool HasValue(string key)
            => PlayerPrefs.HasKey(key);

        public void Save()
        {
            PlayerPrefs.Save();
        }

        public void Load()
        {
        }

        public void Clear()
        {
            Debug.Log("Clear");
            PlayerPrefs.DeleteAll();
            Save();
        }
    }
}