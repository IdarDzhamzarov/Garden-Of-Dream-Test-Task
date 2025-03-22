using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private BuildingPlacer buildingPlacer;
    [SerializeField] private DataManager dataManager;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private BuildingPrefabData buildingPrefabData;

    private void Start()
    {
        InitializeGridManager();
        InitializeGameInput();
        InitializeUIManager();
        InitializeBuildingPlacer();

        LoadBuildings();
    }

    private void InitializeGridManager()
    {
        gridManager.InitializeGrid();
        Debug.Log("GridManager initialized.");
    }

    private void InitializeGameInput()
    {
        gameInput.InitializeInput();
    }

    private void InitializeUIManager()
    {
        uiManager.Initialize(buildingPlacer);
        Debug.Log("UIManager initialized.");
    }

    private void InitializeBuildingPlacer()
    {
        buildingPlacer.Initialize(gridManager, dataManager, buildingPrefabData);
        Debug.Log("BuildingPlacer initialized.");
    }

    private void LoadBuildings()
    {
        foreach (var buildingData in dataManager.GetBuildingsData())
        {
            GameObject building = Instantiate(buildingPlacer.GetBuildingPrefab(buildingData.buildingIndex));

            if (building != null) 
            {
                building.transform.position = buildingData.position;
                gridManager.OccupyCell(buildingData.position);
            }
        }
        Debug.Log("Buildings loaded.");
    }
}