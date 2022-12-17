using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    // Singleton of GameManager
    public static GameManager instance;

    // Holds the amount of Life objects the user has (index is what the Life object is)
    public int[] life;

    // Same as life but with FoodItems
    public int[] food;

    // Not currently used, but will make it so the user cant feed a cow if its planting crops
    public static int menu = 0;

    // Not used anymore, but was so that the user couldnt click when the popup was up. Caused Glitches
    public static bool popup = false;

    // How much life Energy the user has, This will be used for cleansing tiles and upgrading the time machine
    public static int energy = 0;

    // This is the text for the energy
    public TMP_Text energyDisplay;

    // This is the menu for crops
    public GameObject cropsMenu;

    // This is the menu for food
    public GameObject foodMenu;

    // This is the text for the button to change menus
    public TMP_Text buttonText;
    

    private void Awake()
    {

        // Creates the singleton instance, and if you try to make it again then it doesnt work
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }

    // creates the event calls
    void OnEnable()
    {
        
        Plant.onHarvestCrop += OnHarvestCrop;
        Animal.onHarvestAnimal += OnHarvestAnimal;
        Animal.onFeedAnimal += onFeedAnimal;
        TileManager.onCleanse += onCleanse;
    }

    // removes the event calls
    void OnDisable()
    {
        Plant.onHarvestCrop -= OnHarvestCrop;
        Animal.onHarvestAnimal -= OnHarvestAnimal;
        Animal.onFeedAnimal -= onFeedAnimal;
        TileManager.onCleanse -= onCleanse;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // increments energy, gives the user back seeds, and yield for harvesting a plant crop
    public void OnHarvestCrop(Plant crop)
    {
        energy += crop.energyReturn;
        
        life[crop.ID] +=2;
        food[crop.reap] += crop.food;
        display();
    }

    // increments energy and yield for harvesting an animal
    public void OnHarvestAnimal(Animal animal){
        energy += animal.energyReturn;
        food[animal.produce] += animal.quantity;
        display();
    }

    // Changes the energy display to the current energy
    private void display(){
        energyDisplay.text = "Energy: " + energy;
    }

    // Removes the food from food list when you feed an animal
    public void onFeedAnimal(int hunger,int feed){
        food[feed] -= hunger;
        
    }

    // Changes popup to false, was used by buttons in the unity scene
    public void popUpFalse(){
        popup = false;
    }

    // Removes energy equal to the cost of cleansing ( could be used when upgrading as well)
    public void onCleanse(int cost){
        energy -= cost;
        display();
    }

    // This is called by the menu button in the scene. It switches which menus are active and changes text on button
    // Also changes var menu for use in the future
    public void switchMenu(){
        if(foodMenu.activeSelf){
            foodMenu.SetActive(false);
            cropsMenu.SetActive(true);
            buttonText.text = "Food";
            menu = 1;
        }
        else{
            foodMenu.SetActive(true);
            cropsMenu.SetActive(false);
            buttonText.text = "Crops";
            menu = 0;
        }
    }
}
