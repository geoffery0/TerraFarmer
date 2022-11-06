using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
   

    private static MapManager _instance;
    private static MapManager Instance;

    public TileBase GreenFullTilePrefab;
    public TileBase BlueFullTilePrefab;

    public Button button;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Tilemap tileMap = gameObject.GetComponentInChildren<Tilemap>();
        button.onClick.AddListener(onRegenerate);
        
        // BoundsInt bounds = tileMap.cellBounds;
        //
        // for (int z = bounds.max.z; z > bounds.min.z; z--)
        // {
        //     for (int y = bounds.min.y; y < bounds.max.y; y++)
        //     {
        //         for (int x = bounds.min.x; x < bounds.max.x; x++)
        //         {
        //             var tileLocation = new Vector3Int(x, y, z);
        //
        //             if (tileMap.HasTile(tileLocation))
        //             {
        //                 
        //             }
        //         }
        //     }
        // }

        // for (int x = 0; x < 10; x++)
        // {
        //     for (int y = 0; y < 10; y++)
        //     {
        //         Vector3Int position = new Vector3Int(x, y, 0);
        //         if (Random.Range(0f, 1f) < 0.5)
        //         {
        //             tileMap.SetTile(position, GreenFullTilePrefab);
        //         }
        //         else
        //         {
        //             tileMap.SetTile(position, BlueFullTilePrefab);
        //         }
        //     }
        // }

        


    }
    int count = 0;

    void Update()
    {
        count++;

        if (count % 60 == 0)
        {
            onRegenerate();
        }
        
    }

    void onRegenerate()
    {
        Tilemap tileMap = gameObject.GetComponentInChildren<Tilemap>();
        
        tileMap.ClearAllTiles();
        
        for (int x = 0; x < 50; x++)
        {
            for (int y = 0; y < 50; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (Random.Range(0f, 1f) < 0.5)
                {
                    tileMap.SetTile(position, GreenFullTilePrefab);
                }
                else
                {
                    tileMap.SetTile(position, BlueFullTilePrefab);
                }
            }
        }
        
        Vector3 cameraPosition = new Vector3(25 * 0.25f, 25 * 0.5f, Camera.main.transform.position.z);
        Camera.main.transform.position = cameraPosition;
        
    }

}
