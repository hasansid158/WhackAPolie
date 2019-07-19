using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pauseButPointer : MonoBehaviour, IPointerClickHandler
{
    //butSc scripts;

    //void Start()
    //{
    //    scripts = GameObject.FindGameObjectWithTag("scripts").GetComponent<butSc>();
    //}

    public void OnPointerClick(PointerEventData pd)
    {
        if (gameObject.name == "Restart")
        {
            GameObject.FindGameObjectWithTag("scripts").GetComponent<butSc>().reBut();
        }
        else if(gameObject.name == "Exit")
        {
            GameObject.FindGameObjectWithTag("scripts").GetComponent<butSc>().quitBut();
        }
        else
        {
            GameObject.FindGameObjectWithTag("scripts").GetComponent<butSc>().pauseBut();
        }
    }
}
