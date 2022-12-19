using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// This is the base class for both Animal and Plant
    [CreateAssetMenu]
public abstract class Life : ScriptableObject
{
    // These are the sprites
    public TileBase[] tile;
    // This is how "old" it is
    public int age;
    // This is how much energy it returns from being harvested
    public int energyReturn;
    // Every life has an ID
    public int ID;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
