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
    public int diet;

    public int produce;
    public int quantity;

    public static event UnityAction<Animal> onHarvestAnimal;
    public static event UnityAction<int,int> onFeedAnimal;

    public void harvest(FoodItem food)
    {
        if(GameManager.instance.food[fav] >= hunger && food.group == diet){
            
            var temp = age;
            if(food.type == fav){
                age+=1;
            }
            age+=1;
            onFeedAnimal?.Invoke(hunger,food.type);
            
            if (((tile.Length - 2 - age)%2 == 0 || age-temp==2) && age >= tile.Length-2)
            {
                onHarvestAnimal?.Invoke(this);

            }
        }
    }
}
