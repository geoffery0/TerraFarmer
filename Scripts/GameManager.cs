using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int[] life;
    public int[] food;
    public static int menu = 0;
    public static bool popup = false;
    public static int energy = 0;
    public TMP_Text energyDisplay;
    public GameObject cropsMenu;
    public GameObject foodMenu;
    public TMP_Text buttonText;
    

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }

    void OnEnable()
    {
        
        Plant.onHarvestCrop += OnHarvestCrop;
        Animal.onHarvestAnimal += OnHarvestAnimal;
        Animal.onFeedAnimal += onFeedAnimal;
        TileManager.onCleanse += onCleanse;
    }

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

    public void OnHarvestCrop(Plant crop)
    {
        energy += crop.energyReturn;
        
        life[crop.ID] +=2;
        food[crop.reap] += crop.food;
        display();
    }

    public void OnHarvestAnimal(Animal animal){
        energy += animal.energyReturn;
        food[animal.produce] += animal.quantity;
        display();
    }

    private void display(){
        energyDisplay.text = "Energy: " + energy;
    }

    public void onFeedAnimal(int hunger,int feed){
        food[feed] -= hunger;
        
    }

    public void popUpFalse(){
        popup = false;
    }

    public void onCleanse(int cost){
        energy -= cost;
        display();
    }

    public void switchMenu(){
        if(foodMenu.activeSelf){
            foodMenu.SetActive(false);
            cropsMenu.SetActive(true);
            buttonText.text = "Crops";
            menu = 1;
        }
        else{
            foodMenu.SetActive(true);
            cropsMenu.SetActive(false);
            buttonText.text = "Food";
            menu = 0;
        }
    }
}
