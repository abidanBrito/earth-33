using UnityEngine;

public class DataSaveLoad
{
    public void Load<T>(string keyname, ref T data)
    {
        string json = PlayerPrefs.GetString(keyname, "{}");
        // Debug.Log("jsonLoad: " + json);
        JsonUtility.FromJsonOverwrite(json, data);
    }

    public void Save(string keyname, object data)
    {
        string json = JsonUtility.ToJson(data);
        // Debug.Log("jsonSave: " + json);
        PlayerPrefs.SetString(keyname, json);
    }

    public void Clear(string keyname)
    {
        PlayerPrefs.DeleteKey(keyname);
    }
}
