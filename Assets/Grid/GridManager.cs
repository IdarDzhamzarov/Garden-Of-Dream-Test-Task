using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridSize = 10;
    [SerializeField] private float cellSize = 1f;

    private bool[,] grid;
    private Vector2 gridOffset;

    public void InitializeGrid()
    {
        grid = new bool[gridSize, gridSize];

        gridOffset = new Vector2(
            -gridSize * cellSize * 0.5f + cellSize * 0.5f,
            -gridSize * cellSize * 0.5f + cellSize * 0.5f
        );
    }

    public Vector2 GetNearestPointOnGrid(Vector2 position)
    {
        Vector2Int cellIndex = GetCellIndex(position);
        return new Vector2(cellIndex.x * cellSize, cellIndex.y * cellSize) + gridOffset;
    }

    public bool IsCellOccupied(Vector2 position)
    {
        Vector2Int cellIndex = GetCellIndex(position);

        if (IsIndexOutOfBounds(cellIndex))
            return true;

        return grid[cellIndex.x, cellIndex.y];
    }

    public void OccupyCell(Vector2 position)
    {
        Vector2Int cellIndex = GetCellIndex(position);

        if (!IsIndexOutOfBounds(cellIndex))
            grid[cellIndex.x, cellIndex.y] = true;
    }

    public void FreeCell(Vector2 position)
    {
        Vector2Int cellIndex = GetCellIndex(position);

        if (!IsIndexOutOfBounds(cellIndex))
            grid[cellIndex.x, cellIndex.y] = false;
    }

    private Vector2Int GetCellIndex(Vector2 position)
    {
        position -= gridOffset;

        int x = Mathf.RoundToInt(position.x / cellSize);
        int y = Mathf.RoundToInt(position.y / cellSize);

        return new Vector2Int(x, y);
    }

    private bool IsIndexOutOfBounds(Vector2Int cellIndex)
    {
        return cellIndex.x < 0 || cellIndex.x >= gridSize || cellIndex.y < 0 || cellIndex.y >= gridSize;
    }
}