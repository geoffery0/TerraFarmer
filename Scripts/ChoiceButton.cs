using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceButton : MonoBehaviour
{
    public UnityEvent buttonClick;
    
    void Awake(){
        if(buttonClick == null){
            buttonClick = new UnityEvent();
        }
    }
    
    private void OnMouseUp() {
        
        buttonClick.Invoke();
    }
}
