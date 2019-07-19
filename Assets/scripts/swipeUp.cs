using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipeUp : MonoBehaviour
{
    moleHit hitSc;
    scoreSaver sScript;
    moleUp upSc;

    bool clicked;
    float timer = 0.2f;

    Vector3 mouseLastPos;

    void Start()
    {
        upSc = GetComponent<moleUp>();
        sScript = GameObject.FindGameObjectWithTag("ss").GetComponent<scoreSaver>();
        hitSc = GetComponent<moleHit>();
    }

    void Update()
    {
        if (upSc.started && !hitSc.paused)
        {
            if (sScript.gaugeNum > 0)
            {
                if (clicked)
                {
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    else
                    {
                        clicked = false;
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {

                    if (!clicked)
                    {
                        mouseLastPos = Input.mousePosition;
                        clicked = true;
                        timer = 0.2f;
                    }
                    else if (timer > 0)
                    {
                        clicked = false;
                        if (Input.mousePosition.x <= mouseLastPos.x + 20 && Input.mousePosition.x >= mouseLastPos.x - 20 && Input.mousePosition.y <= mouseLastPos.y + 20 && Input.mousePosition.y >= mouseLastPos.y - 20)
                        {
                            hitSc.hugeMalletHit();
                        }
                    }

                }
            }
        }
    }
}
