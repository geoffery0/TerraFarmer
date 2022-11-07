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
    private Dictionary<Vector3Int, Life> lifeOnTile;

    void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach(var tileData in TileDatas){
            foreach(var tile in tileData.tiles){
                dataFromTiles.Add(tile,tileData);
            }
        }

        lifeOnTile = new Dictionary<Vector3Int, Life>();

        for (int n = map.cellBounds.xMin; n < map.cellBounds.xMax; n++)
        {
            for (int p = map.cellBounds.yMin; p < map.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)map.transform.position.y));
                
                if (map.HasTile(localPlace))
                {
                    //Tile at "place"
                    lifeOnTile.Add(localPlace,null);
                }
                else
                {
                    //No tile at "place"
                }
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

            if(lifeOnTile[gridPosition] == null){
                lifeOnTile[gridPosition] = Object.Instantiate(Lifes[2]);
            }
            

            Life occupied = lifeOnTile[gridPosition];
            if(occupied.GetType() == typeof(Animal)){
                Animal animal = (Animal)occupied;
                animal.age+=1;
                print("This " + animal + " is " + animal.age);
            }

            life.SetTile(gridPosition,occupied.tile);
        }
    }

    public Life getTileLife(Vector2 worldPosition){
        Vector3Int gridPosition = map.WorldToCell(worldPosition);

        TileBase tile = map.GetTile(gridPosition);

        if(tile == null){
            return null;
        }

        Life occupied  = lifeOnTile[gridPosition];

        return occupied;
    }
}
