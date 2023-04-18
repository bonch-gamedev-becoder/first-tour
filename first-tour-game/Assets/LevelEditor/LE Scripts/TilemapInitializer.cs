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
            if (category.name == "Wall")
            {
                obj.AddComponent<TilemapCollider2D>();
                obj.tag = "blockingLayer";
                obj.layer = 7;
            }
            else if (category.name == "WallBreakable")
            {
                obj.AddComponent<TilemapCollider2D>();
                obj.tag = "blockingLayerBreakable";
                obj.layer = 12;
            }
            else if (category.name == "FloorBlock")
            {
                TilemapCollider2D col = obj.AddComponent<TilemapCollider2D>();
                col.isTrigger = true;
                obj.tag = "BoostBlock";
                obj.layer = 11;
            }

            obj.transform.SetParent(grid);

            //sorting layer
            tr.sortingOrder = category.SortingOrder;

            category.Tilemap = map;
        }
    }
}
