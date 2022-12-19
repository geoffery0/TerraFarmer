using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class CropItem : MonoBehaviour, IPointerClickHandler
{
    // This is in refrence to the index that it is
    [SerializeField]
    private int type;

    // This allows you to show how much it is visually
    public TMP_Text Amount;

    // This is the name
    [SerializeField]
    private string designation;

    // This allows you to store what index this is in tilemanager
    public static event UnityAction<int> createTileLife;

    // always updating what the text shows
    void Update(){
        Amount.text = designation + ": " + GameManager.instance.life[type];
    }

    // Sends the index to tilemanager 
    public void OnPointerClick(PointerEventData eventData){
        
        createTileLife?.Invoke(type);
    }
}
