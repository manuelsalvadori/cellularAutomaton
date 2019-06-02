using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject quadPrefab;
    public int2 size = new int2(18,10);
    public Dictionary<int2, Cell> grid { get; private set; }

    public static GridGenerator Instance;

    private List<Cell> neighboorsCache = new List<Cell>();

    void Awake()
    {
        var scale = 10.0f / size.y;
        grid = new Dictionary<int2, Cell>();
        transform.position = new Vector3((-size.x / 2 + 0.5f) * scale, (-size.y / 2 + 0.5f) * scale);
        InitGrid();

        Instance = this;
    }

    private void InitGrid()
    {
        var scale = 10.0f / size.y;
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                var cellQuad = Instantiate(quadPrefab, transform);
                var localPos = new Vector3(i * scale, j * scale, 0f);
                cellQuad.transform.localPosition = localPos;
                cellQuad.transform.localScale = Vector3.one * scale;

                int2 coords = new int2(i, j);
                grid.Add(coords, new Cell(coords, cellQuad));
            }
        }
    }

    private readonly int2[] neighboors =
    {
        new int2(-1,-1),
        new int2(-1, 0),
        new int2(-1, 1),
        new int2(0,-1),
        new int2(0, 1),
        new int2(1,-1),
        new int2(1, 0),
        new int2(1, 1),
    };

    public List<Cell> GetNeighboors(int2 coords)
    {
        neighboorsCache.Clear();
        foreach (var neighboor in neighboors)
        {
            var neighboorCoords = coords + neighboor;

            bool2 test = neighboorCoords < int2.zero;
            bool2 test2 = neighboorCoords >= size;

            if (test.x || test.y || test2.x || test2.y)
                continue;

            neighboorsCache.Add(grid[neighboorCoords]);
        }
        return neighboorsCache;
    }
}
