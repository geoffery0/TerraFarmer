using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FoodItem : MonoBehaviour, IPointerClickHandler
{
    
    public int type;
    public int group;

    public TMP_Text Amount;

    [SerializeField]
    private string designation;

    [SerializeField]
    private int saturation;

    public static event UnityAction<FoodItem> feedTileLife;


    // Update is called once per frame
    void Update()
    {
        Amount.text = designation + ": " + GameManager.instance.food[type];
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        feedTileLife?.Invoke(this);
        Amount.text = designation + ": " + GameManager.instance.food[type];
    }
}
