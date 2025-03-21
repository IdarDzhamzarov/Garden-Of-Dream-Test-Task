using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private GameObject[] buildingPrefabs;
    [SerializeField] private LayerMask buildingLayerMask;

    private GameObject currentBuilding;
    private int selectedBuildingIndex = -1;

    private bool isPlacing = false;
    private bool isDeleting = false;

    private GridManager gridManager;
    private DataManager dataManager;

    public void Initialize(GridManager gridManager, DataManager dataManager)
    {
        this.gridManager = gridManager;
        this.dataManager = dataManager;
    }

    private void Start()
    {
        GameInput.Instance.OnMouseClickAction += GameInput_OnMouseClickAction;
    }

    private void Update()
    {
        if (isPlacing && currentBuilding != null)
        {
            MoveBuildingWithMouse();
        }
    }

    private void GameInput_OnMouseClickAction(object sender, System.EventArgs e)
    {
        if (isPlacing && currentBuilding != null)
        {
            PlaceBuilding();
        }
        else if (isDeleting)
        {
            TryDeleteBuilding();
        }
    }

    public void SelectBuilding(int index)
    {
        selectedBuildingIndex = index;
    }

    public void SetPlacementMode(bool placing, bool deleting)
    {
        isPlacing = placing;
        isDeleting = deleting;


        if (isPlacing && selectedBuildingIndex != -1)
        {
            currentBuilding = Instantiate(buildingPrefabs[selectedBuildingIndex]);
        }
        else if (isDeleting)
        {
            currentBuilding = null;
        }
        else
        {
            isPlacing = false;
            isDeleting = false;
        }
    }

    private void MoveBuildingWithMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 gridPosition = gridManager.GetNearestPointOnGrid(mousePosition);

        if (!gridManager.IsCellOccupied(gridPosition))
        {
            currentBuilding.transform.position = gridPosition;
        }
    }

    private void PlaceBuilding()
    {
        Vector2 position = currentBuilding.transform.position;

        if (!gridManager.IsCellOccupied(position))
        {
            gridManager.OccupyCell(position);
            currentBuilding = null;
            isPlacing = false;
            dataManager.SaveBuilding(selectedBuildingIndex, position);
        }
    }

    private void TryDeleteBuilding()
    {
        if (!isDeleting) return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, buildingLayerMask);

        if (hit.collider != null)
        {
            gridManager.FreeCell(hit.transform.position);
            Destroy(hit.collider.gameObject);
            dataManager.RemoveBuilding(mousePosition);
        }
    }

    public GameObject GetBuildingPrefab(int index)
    {
        if (index >= 0 && index < buildingPrefabs.Length)
        {
            return buildingPrefabs[index];
        }
        Debug.LogError("Invalid building index: " + index);
        return null;
    }
}