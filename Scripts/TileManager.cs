using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

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

    [SerializeField]
    private TileBase Arable;

    private Dictionary<TileBase, TileData> dataFromTiles;
    private Dictionary<Vector3Int, Life> lifeOnTile;

    private Vector3Int selectedTile;

    public static event UnityAction<int> onCleanse;

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

    void OnEnable()
    {
        Select.createTileLife += createTileLife;
        Plant.destroyCrop +=destroyCrop;
    }

    void OnDisable()
    {
        Select.createTileLife -= createTileLife;
        Plant.destroyCrop -=destroyCrop;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.popup == false)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = map.WorldToCell(mousePosition);

            
            TileBase clickedTile = map.GetTile(gridPosition);

            if(!dataFromTiles[clickedTile].arable && GameManager.energy>=60){
                map.SetTile(gridPosition,Arable);
                onCleanse?.Invoke(60);
            }

            if(lifeOnTile[gridPosition] == null && dataFromTiles[clickedTile].arable){
                selectedTile = gridPosition;
                //PopupSystem pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopupSystem>();
                //pop.PopUp();
                
            }

            if(lifeOnTile[gridPosition] != null){
                
                
                if(lifeOnTile[gridPosition] is Plant){
                    Life occupied = lifeOnTile[gridPosition];
                    occupied.age += 1;
                    print("This " + occupied + " is " + occupied.age);
                    Plant plant =(Plant)lifeOnTile[gridPosition];
                    plant.harvest(gridPosition);
                }
                else if (lifeOnTile[gridPosition] is Animal)
                {
                    Animal animal = (Animal)lifeOnTile[gridPosition];
                    animal.harvest();
                }
                changeSprite(gridPosition);
            }
            
            

            
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

    public void createTileLife(int choice){
        if(GameManager.instance.life[choice] == 0 || selectedTile == null || lifeOnTile[selectedTile] != null){
            GameManager.popup = false;
            return;
        }
        lifeOnTile[selectedTile] = Object.Instantiate(Lifes[choice]);

        Life occupied = lifeOnTile[selectedTile];
        
        
        

        life.SetTile(selectedTile, occupied.tile[0]);
        GameManager.instance.life[choice]-=1;
        GameManager.popup = false;
    }

    private void changeSprite(Vector3Int gridPosition){
        if(lifeOnTile[gridPosition].age<lifeOnTile[gridPosition].tile.Length){
            life.SetTile(gridPosition, lifeOnTile[gridPosition].tile[lifeOnTile[gridPosition].age]);
        }
    }

    public void destroyCrop(Vector3Int crop){
        Destroy(lifeOnTile[crop]);
        life.SetTile(crop, null);
    }
}
