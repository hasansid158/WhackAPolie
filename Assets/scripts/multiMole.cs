using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class multiMole : MonoBehaviour
{
    [HideInInspector]
    public float moleDownTime, moleTimeLimit;
    [HideInInspector]
    public bool multiPop, multiTimeNow;

    moleUp upSc;
    moleHit hitSc;
    scoreSaver sScript;

    void Start()
    {
        upSc = GameObject.FindGameObjectWithTag("scripts").GetComponent<moleUp>();
        hitSc = GameObject.FindGameObjectWithTag("scripts").GetComponent<moleHit>();
        sScript = GameObject.FindGameObjectWithTag("ss").GetComponent<scoreSaver>();
        moleTimeLimit = 10;
    }

    void Update()
    {
        if (multiPop)
        {
            moleDownTime += Time.deltaTime;
        }
        if (moleDownTime >= moleTimeLimit && multiTimeNow)
        {
            moleDownTime = 0;
            multiPop = false;
            multiTimeNow = false;
            GetComponent<Animation>().Play("goIn");

            for (int a = 0; a < upSc.moles.Length; a++)
            {
                if (upSc.moles[a] == gameObject)
                {
                    hitSc.hitNow[a] = false;
                }
            }

            transform.parent.parent.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "Missed";
            transform.parent.parent.GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>().text = "Missed";
            transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Animation>().Play();

            hitSc.multiMoleHits++;

            hitSc.comboHits = 0;

            sScript.gaugeScore = 0;
            if (sScript.gaugeNum < 5)
            {
                hitSc.bonusGauge.transform.GetChild(sScript.gaugeNum).GetComponent<Slider>().value = sScript.gaugeScore;
            }

            if (hitSc.multiMoleHits > 4 && upSc.fiveOn)
            {
                upSc.fiveOn = false;
                hitSc.multiMoleHits = 0;

                if (upSc.moleLimit < upSc.moleMax)
                {
                    StartCoroutine(upSc.molePoper());
                }
                else
                {
                    hitSc.gameEnded();
                }
            }
            else if (hitSc.multiMoleHits > 3 && upSc.fourOn)
            {
                upSc.fourOn = false;
                hitSc.multiMoleHits = 0;

                if (upSc.moleLimit < upSc.moleMax)
                {
                    StartCoroutine(upSc.molePoper());
                }
                else
                {
                    hitSc.gameEnded();
                }
            }
            else if (hitSc.multiMoleHits > 2 && upSc.threeOn)
            {
                upSc.threeOn = false;
                hitSc.multiMoleHits = 0;

                if (upSc.moleLimit < upSc.moleMax)
                {
                    StartCoroutine(upSc.molePoper());
                }
                else
                {
                    hitSc.gameEnded();
                }
            }
            else if (hitSc.multiMoleHits > 1 && upSc.twoOn)
            {
                if (upSc.twoHits)
                {
                    upSc.twoOn = false;
                    hitSc.multiMoleHits = 0;
                    upSc.twoHits = false;

                    if (upSc.moleLimit < upSc.moleMax)
                    {
                        StartCoroutine(upSc.molePoper());
                    }
                    else
                    {
                        hitSc.gameEnded();
                    }
                }
                else
                {
                    upSc.twoHits = true;
                }
            }
            else
            {
                if (upSc.twoOn)
                {
                    if (upSc.twoHits)
                    {
                        upSc.twoOn = false;
                        hitSc.multiMoleHits = 0;
                        upSc.twoHits = false;

                        if (upSc.moleLimit < upSc.moleMax)
                        {
                            StartCoroutine(upSc.molePoper());
                        }
                        else
                        {
                            hitSc.gameEnded();
                        }
                    }
                    else
                    {
                        upSc.twoHits = true;
                    }
                }
                else
                {
                    if (upSc.moleLimit < upSc.moleMax)
                    {
                        StartCoroutine(upSc.molePoper());
                    }
                    else
                    {
                        hitSc.gameEnded();
                    }
                }
            }
        }
    }


    public void multiUp(float inSpeed)
    {
        GetComponent<Animation>().Play();
        GetComponent<multiMole>().moleDownTime = 0;
        hitSc.multiMoleHits = 0;
        GetComponent<multiMole>().moleTimeLimit = inSpeed;
        GetComponent<multiMole>().multiTimeNow = true;
        GetComponent<multiMole>().multiPop = true;
    }
}
