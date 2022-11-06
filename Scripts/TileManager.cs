using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private List<TileData> TileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;
    // Start is called before the first frame update

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

            int occupied = dataFromTiles[clickedTile].occupied;


            print( clickedTile + " is occupied by " + occupied);
        }
    }

    public int getTileOccupied(Vector2 worldPosition){
        Vector3Int gridposition = map.WorldToCell(worldPosition);

        TileBase tile = map.GetTile(gridposition);

        if(tile == null){
            return 0;
        }

        int occupied  = dataFromTiles[tile].occupied;

        return occupied;
    }
}
