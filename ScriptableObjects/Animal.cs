using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    [CreateAssetMenu]
public class Animal : Life
{
    public string named;
    
    public int hunger;
    public readonly int bred;

    public static event UnityAction<Life> onHarvestAnimal;
    public static event UnityAction<int> onFeedAnimal;

    public void harvest()
    {
        if(GameManager.food >= hunger){
            age+=1;
            onFeedAnimal?.Invoke(hunger);
            
            if ((tile.Length - 1 - age)%2 == 0 && age >= tile.Length)
            {
                onHarvestAnimal?.Invoke(this);

            }
        }
    }
}
