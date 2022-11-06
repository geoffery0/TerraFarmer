using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private Tilemap life;

    [SerializeField]
    private List<TileData> TileDatas;

    [SerializeField]
    private List<Life> Lifes;

    private Dictionary<TileBase, TileData> dataFromTiles;

    void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach(var tileData in TileDatas){
            foreach(var tile in tileData.tiles){
                dataFromTiles.Add(tile,tileData);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = map.WorldToCell(mousePosition);

            
            TileBase clickedTile = map.GetTile(gridPosition);

            Life occupied = dataFromTiles[clickedTile].occupied;


            life.SetTile(gridPosition,occupied.tile);
        }
    }

    public Life getTileOccupied(Vector2 worldPosition){
        Vector3Int gridposition = map.WorldToCell(worldPosition);

        TileBase tile = map.GetTile(gridposition);

        if(tile == null){
            return null;
        }

        Life occupied  = dataFromTiles[tile].occupied;

        return occupied;
    }
}
