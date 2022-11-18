using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int[] life;
    public static int food;
    public static bool popup = false;
    public static int energy = 0;
    public TMP_Text energyDisplay;

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

    public void OnHarvestCrop(Life crop)
    {
        energy += crop.energyReturn;
        
        life[crop.ID] +=2;
        food += 1;
        display();
    }

    public void OnHarvestAnimal(Life animal){
        energy += animal.energyReturn;
        display();
    }

    private void display(){
        energyDisplay.text = "Energy: " + energy + "\nFood: " + food;
    }

    public void onFeedAnimal(int hunger){
        food-= hunger;
        display();
    }

    public void popUpFalse(){
        popup = false;
    }

    public void onCleanse(int cost){
        energy -= cost;
        display();
    }
}
