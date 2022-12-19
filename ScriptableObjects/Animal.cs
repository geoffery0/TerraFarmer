using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Framework for the animal objects
    [CreateAssetMenu]
public class Animal : Life
{
    // Each object is unique! Name your animals if you want
    public string named;

    // Refers to how much it needs to be fed    
    public int hunger;

    // Currently not used
    public readonly int bred;

    // This holds the index for its favorite food
    public int fav;
    // This holds whether the animal is an herbavore or a carnivore or an omnivore
    public int diet;
    // This holds the ID of what the animal produces
    public int produce;
    // This holds the ID of how much the animal produces
    public int quantity;
    // This event increments energy and food based on what this animal produces
    public static event UnityAction<Animal> onHarvestAnimal;
    // This event removes Food according to what you feed it
    public static event UnityAction<int,int> onFeedAnimal;

    // This function checks if it can be fed, and how much it ages as well as if it produces energy and food
    public void harvest(FoodItem food)
    {
        // Checks if theres enough food and it falls within the animals diet
        if(GameManager.instance.food[fav] >= hunger && food.group == diet){
            // If the food is its favorite then it ages twice
            var temp = age;
            if(food.type == fav){
                age+=1;
            }
            age+=1;
            onFeedAnimal?.Invoke(hunger,food.type);
            // If the animal is grown up and its the second time its fed or it was fed its favorite food, it will produce food and energy
            if (((tile.Length - age)%2 == 0 || age-temp==2) && age >= tile.Length-2)
            {
                onHarvestAnimal?.Invoke(this);

            }
        }
    }
}
