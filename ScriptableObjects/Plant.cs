using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Plant : Life
{
    public static event UnityAction<Life> onHarvestCrop;
    public static event UnityAction<Vector3Int> destroyCrop;
    
    public void harvest(Vector3Int crop){
        if(age==tile.Length){
            onHarvestCrop?.Invoke(this);
            destroyCrop?.Invoke(crop);
        }
    }

    

}
