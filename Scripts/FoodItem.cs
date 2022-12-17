using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FoodItem : MonoBehaviour, IPointerClickHandler
{
    // Refers to the index it is
    public int type;

    // If its plant(0) or animal(1)
    public int group;

    // Shows how much there is of this
    public TMP_Text Amount;

    // The name of the food
    [SerializeField]
    private string designation;

    // The amount it fills an animal(not currently used)
    [SerializeField]
    private int saturation;

    // Allows you to store the food in tileManager
    public static event UnityAction<FoodItem> feedTileLife;


    // Update is called once per frame
    void Update()
    {
        // updates text
        Amount.text = designation + ": " + GameManager.instance.food[type];
    }

    // Sends this to tileManager
    public void OnPointerClick(PointerEventData eventData)
    {
        
        feedTileLife?.Invoke(this);
    }
}
