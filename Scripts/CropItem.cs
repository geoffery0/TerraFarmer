using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class CropItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int type;

    public TMP_Text Amount;

    [SerializeField]
    private string designation;

    public static event UnityAction<int> createTileLife;

    void Update(){
        Amount.text = designation + ": " + GameManager.instance.life[type];
    }


    public void OnPointerClick(PointerEventData eventData){
        
        createTileLife?.Invoke(type);
        Amount.text = designation + ": " + GameManager.instance.life[type];
    }
}
