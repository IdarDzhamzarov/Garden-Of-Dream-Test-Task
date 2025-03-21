using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private BuildingPlacer buildingPlacer;
    [SerializeField] private DataManager dataManager;

    private void Start()
    {
        InitializeGridManager();
        InitializeUIManager();
        InitializeBuildingPlacer();

        LoadBuildings();
    }

    private void InitializeGridManager()
    {
        gridManager.InitializeGrid();
        Debug.Log("GridManager initialized.");
    }

    private void InitializeUIManager()
    {
        uiManager.Initialize(buildingPlacer);
        Debug.Log("UIManager initialized.");
    }

    private void InitializeBuildingPlacer()
    {
        buildingPlacer.Initialize(gridManager, dataManager);
        Debug.Log("BuildingPlacer initialized.");
    }

    private void LoadBuildings()
    {
        foreach (var buildingData in dataManager.GetBuildingsData())
        {
            GameObject building = Instantiate(buildingPlacer.GetBuildingPrefab(buildingData.buildingIndex));
            building.transform.position = buildingData.position;
            gridManager.OccupyCell(buildingData.position);
        }
        Debug.Log("Buildings loaded.");
    }
}