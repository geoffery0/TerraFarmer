using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class TileManager : MonoBehaviour
{
    // Tilemap for land tiles
    [SerializeField]
    private Tilemap map;

    // Tilemap for crop tiles
    [SerializeField]
    private Tilemap life;

    // List of scriptable TileData objects (land tiles)
    [SerializeField]
    private List<TileData> TileDatas;

    // List of scriptable Life objects (crop tiles)
    [SerializeField]
    private List<Life> Lifes;

    // The arable tile for when you cleanse a wasteland
    [SerializeField]
    private TileBase Arable;

    // Dictionary to relate the specific TileBase to its corresponding TileData
    private Dictionary<TileBase, TileData> dataFromTiles;

    // Dictionary to express which tiles have crops on them (Life)
    private Dictionary<Vector3Int, Life> lifeOnTile;

    // This expresses when a user selects a tile (currently not used)
    private Vector3Int selectedTile;

    // This refers to which crop the player has chosen by index (eg cow:0)
    private int selectedChoice = -1;

    // Holds a FoodItem object when the user clicks on a food to feed the animal
    private FoodItem selectedFood = null;

    // This event calls the GameManager to remove that much energy (currently hardcoded at 60)
    public static event UnityAction<int> onCleanse;

    //Used to initialize the dictionaries
    void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        // Finds each TileData object and reads each TileBase it holds and attributes the TileBase as the key
        foreach(var tileData in TileDatas){
            foreach(var tile in tileData.tiles){
                dataFromTiles.Add(tile,tileData);
            }
        }

        lifeOnTile = new Dictionary<Vector3Int, Life>();

        // Goes through the bounds of the map and initializes a null value at each existing land tile 
        // Will need to add new dictionary instances if new land is generated
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

    // Adds event function calls when enabled
    void OnEnable()
    {
        CropItem.createTileLife += createTileLife;
        Plant.destroyCrop +=destroyCrop;
        FoodItem.feedTileLife += feedTileLife;
    }

    // Removes event function calls when disabled
    void OnDisable()
    {
        CropItem.createTileLife -= createTileLife;
        Plant.destroyCrop -=destroyCrop;
        FoodItem.feedTileLife -= feedTileLife;
    }


    // Update is called once per frame
    void Update()
    {
        // Checking for a mouse click (popup is an artifact that could be used in the future)
        if (Input.GetMouseButtonDown(0) && GameManager.popup == false)
        {
            // Grabs the grid position of the mouse click
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = map.WorldToCell(mousePosition);

            // Grabs the TileBase at gridPosition
            TileBase clickedTile = map.GetTile(gridPosition);

            // Checks if the tile is not arable (wasteland) and if the user has enough energy, converts it into an arable land tile
            if(!dataFromTiles[clickedTile].arable && GameManager.energy>=60){
                map.SetTile(gridPosition,Arable);
                onCleanse?.Invoke(60);
            }

            // This codeblock instantiates crops (Life), sets it to its first tile and decrements from availible crops
            if(lifeOnTile[gridPosition] == null && dataFromTiles[clickedTile].arable && selectedChoice != -1){

                // Checks if there is enough crop to place/plant and if not returns
                // Popup is an artifact at the moment
                if (GameManager.instance.life[selectedChoice] == 0 || gridPosition == null || lifeOnTile[gridPosition] != null)
                {
                    GameManager.popup = false;
                    return;
                }

                lifeOnTile[gridPosition] = Object.Instantiate(Lifes[selectedChoice]);

                Life occupied = lifeOnTile[gridPosition];

                life.SetTile(gridPosition, occupied.tile[0]);
                GameManager.instance.life[selectedChoice] -= 1;
                GameManager.popup = false;
                
            }

            // This thing
            if(lifeOnTile[gridPosition] != null){
                
                
                if(lifeOnTile[gridPosition] is Plant){
                    Life occupied = lifeOnTile[gridPosition];
                    occupied.age += 1;
                    print("This " + occupied + " is " + occupied.age);
                    Plant plant =(Plant)lifeOnTile[gridPosition];
                    plant.harvest(gridPosition);
                }
                else if (lifeOnTile[gridPosition] is Animal && selectedFood!=null)
                {
                    Animal animal = (Animal)lifeOnTile[gridPosition];
                    animal.harvest(selectedFood);
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
        selectedChoice = choice;
        
    }

    private void changeSprite(Vector3Int gridPosition){
        if(lifeOnTile[gridPosition].age<=lifeOnTile[gridPosition].tile.Length){
            life.SetTile(gridPosition, lifeOnTile[gridPosition].tile[lifeOnTile[gridPosition].age]);
        }
    }

    public void destroyCrop(Vector3Int crop){
        Destroy(lifeOnTile[crop]);
        life.SetTile(crop, null);
    }

    public void feedTileLife(FoodItem food){
        selectedFood = food;
    }
}
