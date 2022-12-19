using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// The framework for plant objects
[CreateAssetMenu]
public class Plant : Life
{
    // This event allows the allocation of energy and food when harvested
    public static event UnityAction<Plant> onHarvestCrop;
    // This event destroys this object
    public static event UnityAction<Vector3Int> destroyCrop;

    // This is the ID refrence of what food it produces when harvested
    public int food;
    // This expresses how much food you get for harvesting it
    public int reap;
    
    // This is called by TileManager to check if its harvestable
    public void harvest(Vector3Int crop){
        // Harvests and destroys crop at location if clicked on after fully grown
        // Should we have the user click on crops manually to harvest them even if the days go by automatically?
        if(age==tile.Length){
            onHarvestCrop?.Invoke(this);
            destroyCrop?.Invoke(crop);
        }
    }

    

}
