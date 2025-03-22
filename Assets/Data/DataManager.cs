using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BuildingData
{
    public int buildingIndex;
    public Vector3 position;

    public override bool Equals(object obj)
    {
        if (obj is BuildingData other)
        {
            return buildingIndex == other.buildingIndex && position == other.position;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return buildingIndex.GetHashCode() ^ position.GetHashCode();
    }
}

public class DataManager : MonoBehaviour
{
    private HashSet<BuildingData> buildingsData = new HashSet<BuildingData>();
    private string savePath;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/buildings.json";
        LoadData();
    }

    public void SaveBuilding(int index, Vector3 position)
    {
        var newBuilding = new BuildingData { buildingIndex = index, position = position };

        if (buildingsData.Add(newBuilding))
        {
            SaveData();
        }
    }

    public void RemoveBuilding(Vector3 position)
    {
        var buildingToRemove = buildingsData.FirstOrDefault(b => b.position == position);
        if (buildingToRemove != null)
        {
            buildingsData.Remove(buildingToRemove);
            SaveData();
        }
    }

    public List<BuildingData> GetBuildingsData()
    {
        return new List<BuildingData>(buildingsData);
    }

    private void SaveData()
    {
        var wrapper = new SerializationWrapper<BuildingData> { data = new List<BuildingData>(buildingsData) };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, json);
    }

    private void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            var wrapper = JsonUtility.FromJson<SerializationWrapper<BuildingData>>(json);

            if (wrapper != null && wrapper.data != null)
            {
                buildingsData = new HashSet<BuildingData>(wrapper.data);
            }
            else
            {
                Debug.LogError("Failed to load buildings data: File is empty or corrupted.");
            }
        }
        else
        {
            Debug.Log("No save file found. Starting with empty data.");
        }
    }
}

[System.Serializable]
public class SerializationWrapper<T>
{
    public List<T> data;
}