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
        position -= gridOffset;

        int x = Mathf.RoundToInt(position.x / cellSize);
        int y = Mathf.RoundToInt(position.y / cellSize);

        return new Vector2(x * cellSize, y * cellSize) + gridOffset;
    }

    public bool IsCellOccupied(Vector2 position)
    {
        position -= gridOffset;

        int x = Mathf.RoundToInt(position.x / cellSize);
        int y = Mathf.RoundToInt(position.y / cellSize);

        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize)
            return true;

        return grid[x, y];
    }

    public void OccupyCell(Vector2 position)
    {
        position -= gridOffset;

        int x = Mathf.RoundToInt(position.x / cellSize);
        int y = Mathf.RoundToInt(position.y / cellSize);

        if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
            grid[x, y] = true;
    }

    public void FreeCell(Vector2 position)
    {
        position -= gridOffset;

        int x = Mathf.RoundToInt(position.x / cellSize);
        int y = Mathf.RoundToInt(position.y / cellSize);

        if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
            grid[x, y] = false;
    }
}