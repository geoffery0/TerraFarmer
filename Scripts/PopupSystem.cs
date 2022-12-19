using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// NOT CURRENTLY USED
public class PopupSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpCow;
    public TMP_Text popUpPlant;
    

    public void PopUp(){
        popUpBox.SetActive(true);
        popUpCow.text = "Cows:"+ GameManager.instance.life[0];
        popUpPlant.text = "Plant Seeds:" + GameManager.instance.life[1];
        GameManager.popup = true;
        animator.SetTrigger("pop");
    }

}
