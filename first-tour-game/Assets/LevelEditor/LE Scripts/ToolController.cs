using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolController : Singleton<ToolController>
{
    List<Tilemap> tilemaps = new List<Tilemap>();
    private void Start()
    {
        List<Tilemap> maps = FindObjectsOfType<Tilemap>().ToList();
        maps.ForEach(map =>
        {
            if (map.name.Contains("Tilemap_"))
            {
                tilemaps.Add(map);
            }
        });

        tilemaps.Sort((a, b) =>
        {
            TilemapRenderer aRenderer = a.GetComponent<TilemapRenderer>();
            TilemapRenderer bRenderer = b.GetComponent<TilemapRenderer>();

            return bRenderer.sortingOrder.CompareTo(aRenderer.sortingOrder);
        });
    }
    public void Eraser(Vector3Int position)
    {
        Debug.Log("Use eraser");
        tilemaps.Any(map =>
        {
            if (map.HasTile(position))
            {
                map.SetTile(position, null);
                //return true;
            }
            return false;
        });
    }
}
