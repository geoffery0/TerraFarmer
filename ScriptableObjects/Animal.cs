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
    public int fav;

    public int produce;
    public int quantity;

    public static event UnityAction<Animal> onHarvestAnimal;
    public static event UnityAction<int,int> onFeedAnimal;

    public void harvest()
    {
        if(GameManager.instance.life[fav] >= hunger){
            age+=1;
            onFeedAnimal?.Invoke(hunger,fav);
            
            if ((tile.Length - 1 - age)%2 == 0 && age >= tile.Length-1)
            {
                onHarvestAnimal?.Invoke(this);

            }
        }
    }
}
