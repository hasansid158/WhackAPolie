using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiSaver : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }        
    }

    public void closeHugeMallet()
    {
        GameObject.FindGameObjectWithTag("scripts").GetComponent<butSc>().oKBut();
    }
}
