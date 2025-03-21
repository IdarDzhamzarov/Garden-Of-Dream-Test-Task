using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class BuildingData
{
    public int buildingIndex;
    public Vector3 position;
}

public class DataManager : MonoBehaviour
{
    private List<BuildingData> buildingsData = new List<BuildingData>();
    private string savePath;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/buildings.json";
        LoadData();
    }

    public void SaveBuilding(int index, Vector3 position)
    {
        buildingsData.Add(new BuildingData { buildingIndex = index, position = position });
        SaveData();
    }

    public void RemoveBuilding(Vector3 position)
    {
        buildingsData.RemoveAll(b => b.position == position);
        SaveData();
    }

    public List<BuildingData> GetBuildingsData()
    {
        return buildingsData;
    }

    private void SaveData()
    {
        var wrapper = new SerializationWrapper<BuildingData> { data = buildingsData };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, json);
    }

    private void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            buildingsData = JsonUtility.FromJson<SerializationWrapper<BuildingData>>(json).data;
        }
    }
}

[System.Serializable]
public class SerializationWrapper<T>
{
    public List<T> data;
}
