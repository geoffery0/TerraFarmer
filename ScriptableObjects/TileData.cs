using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// This is the land tile info
    [CreateAssetMenu]
public class TileData : ScriptableObject
{
    // These are the sprites that count as this object(if you wanted to hvae multiple different sprites considered grass for example)
    public TileBase[] tiles;

    // This tells the game if it needs to be cleansed or can be planted on
    public bool arable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
