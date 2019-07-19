using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bonusSc : MonoBehaviour
{
    moleHit hitSc;
    moleUp upSc;
    float timer;
    public Text sliderText;
    public Image fillImg;

    bool once;
    bool onceAnim;

    void Start()
    {
        timer = 30;
        hitSc = GameObject.FindGameObjectWithTag("scripts").GetComponent<moleHit>();
        upSc = GameObject.FindGameObjectWithTag("scripts").GetComponent<moleUp>();
    }

    void Update()
    {
        if (onceAnim)
        {
            onceAnim = false;
            fillImg.GetComponent<Animation>().Play();
        }

        if (!hitSc.paused && upSc.started)
        {
            if (timer > 0)
            {
                if (!onceAnim)
                {
                    onceAnim = true;
                }

                timer -= Time.deltaTime;
                GetComponent<Slider>().value = timer;
                sliderText.text = timer.ToString("f0");
            }
            else
            {
                if (!once)
                {
                    once = true;
                    fillImg.gameObject.transform.parent.GetComponent<Animation>().Play();
                    hitSc.bonusComplete = true;
                    upSc.moleLimit = 40;
                }
            }
        }
    }
}
