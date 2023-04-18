using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TilemapInitializer : Singleton<TilemapInitializer>
{
    [SerializeField] List<BuildingCategory> categoriesToCreateTilemapFor;
    [SerializeField] Transform grid;
    private void Start()
    {
        CreateMaps();
    }

    private void CreateMaps()
    {
        foreach (BuildingCategory category in categoriesToCreateTilemapFor)
        {
            //Creating Tilemap
            GameObject obj = new GameObject("Tilemap_" + category.name);
            Tilemap map = obj.AddComponent<Tilemap>();
            TilemapRenderer tr = obj.AddComponent<TilemapRenderer>();

            obj.transform.SetParent(grid);

            //sorting layer
            tr.sortingOrder = category.SortingOrder;

            category.Tilemap = map;
        }
    }
}
