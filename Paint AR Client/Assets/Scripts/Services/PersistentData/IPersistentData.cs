namespace Services.PersistentData
{
    public interface IPersistentData
    {
        void SetValue(string key, string value);
        string GetValue(string key);
        void RemoveValue(string key);
        bool HasValue(string key);
        void Save();
        void Load();
        void Clear();
    }
}