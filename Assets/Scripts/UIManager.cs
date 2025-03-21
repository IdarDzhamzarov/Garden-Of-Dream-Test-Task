using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button[] buildingButtons;
    [SerializeField] private Button placeButton;
    [SerializeField] private Button deleteButton;

    private BuildingPlacer buildingPlacer;

    public void Initialize(BuildingPlacer buildingPlacer)
    {
        this.buildingPlacer = buildingPlacer;

        for (int i = 0; i < buildingButtons.Length; i++)
        {
            int index = i;
            buildingButtons[i].onClick.AddListener(() => SelectBuilding(index));
        }

        placeButton.onClick.AddListener(() => buildingPlacer.SetPlacementMode(true, false));
        deleteButton.onClick.AddListener(() => buildingPlacer.SetPlacementMode(false, true));
    }

    private void SelectBuilding(int buildingIndex)
    {
        buildingPlacer.SelectBuilding(buildingIndex);
    }
}
