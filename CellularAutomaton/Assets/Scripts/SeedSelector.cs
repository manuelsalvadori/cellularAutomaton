using UnityEngine;
using Unity.Mathematics;
public class SeedSelector : MonoBehaviour
{
    private bool active = false;

    private void OnMouseDown()
    {
        var scale = 10.0f / GridGenerator.Instance.size.y;
        var coords = transform.localPosition / scale;
        active = !active;
        GridGenerator.Instance.grid[new int2((int)coords.x, (int)coords.y)].SetAlive(active);
        GridGenerator.Instance.grid[new int2((int)coords.x, (int)coords.y)].Update();
    }
}