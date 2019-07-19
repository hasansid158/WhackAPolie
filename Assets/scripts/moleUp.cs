using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class moleUp : MonoBehaviour
{
    public GameObject[] moles;
    [HideInInspector]
    public int moleNum;
    public List<int> molePatternL;

    public bool pressed, pressed2/*, pressed3, pressed4, pressed5*/;
    public float moleTime, moleTime2/*, moleTime3, moleTime4, moleTime5*/;

    public GameObject sound;
    public AudioClip inSound, outSound;

    [HideInInspector]
    public float[] outSpeed, inSpeed;
    public int missMole;

    moleHit hitSc;

    public int moleMax;
    [HideInInspector]
    public int moleLimit;
    public bool started;

    //scoreSaver sScript;
    public bool oneOn, twoOn, threeOn, fourOn, fiveOn, twoHits;
    bool inMolePop;
    //[HideInInspector]
    //public int multiHits;

    bool endOnce;

    void Start()
    {
        moleNum = -1;
        moleLimit = -1;
        //sScript = GameObject.FindGameObjectWithTag("ss").GetComponent<scoreSaver>();
        hitSc = GetComponent<moleHit>();

        outSpeed = new float[9];
        inSpeed = new float[9];

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //Time of moles popping out sequently 
            outSpeed[0] = 0.35f;
            outSpeed[1] = 0.65f;
            outSpeed[2] = 0.1f;
            outSpeed[3] = 0.6f;
            outSpeed[4] = 1.05f;
            outSpeed[5] = 0.6f;
            outSpeed[6] = 0.35f;
            outSpeed[7] = 0.65f;
            outSpeed[8] = 0.1f;

            //Time of moles staying out sequently 
            inSpeed[0] = 1.05f;
            inSpeed[1] = 0.75f;
            inSpeed[2] = 0.95f;
            inSpeed[3] = 1.25f;
            inSpeed[4] = 1.4f;
            inSpeed[5] = 0.8f;
            inSpeed[6] = 0.75f;
            inSpeed[7] = 1.05f;
            inSpeed[8] = 0.75f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //Time of moles popping out sequently 
            outSpeed[0] = 0.25f;
            outSpeed[1] = 1f;
            outSpeed[2] = 0.05f;
            outSpeed[3] = 0.5f;
            outSpeed[4] = 1f;
            outSpeed[5] = 0.5f;
            outSpeed[6] = 0.25f;
            outSpeed[7] = 0.8f;
            outSpeed[8] = 0.05f;

            //Time of moles staying out sequently 
            inSpeed[0] = 0.97f;
            inSpeed[1] = 0.7f;
            inSpeed[2] = 0.87f;
            inSpeed[3] = 1.17f;
            inSpeed[4] = 1.35f;
            inSpeed[5] = 0.75f;
            inSpeed[6] = 0.68f;
            inSpeed[7] = 0.97f;
            inSpeed[8] = 0.7f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            //Time of moles popping out sequently 
            outSpeed[0] = 0.2f;
            outSpeed[1] = 0.5f;
            outSpeed[2] = 0.05f;
            outSpeed[3] = 1f;
            outSpeed[4] = 0.8f;
            outSpeed[5] = 0.35f;
            outSpeed[6] = 0.2f;
            outSpeed[7] = 0.5f;
            outSpeed[8] = 0.65f;

            //Time of moles staying out sequently 
            inSpeed[0] = 0.94f;
            inSpeed[1] = 0.69f;
            inSpeed[2] = 0.84f;
            inSpeed[3] = 1.1f;
            inSpeed[4] = 1.3f;
            inSpeed[5] = 0.7f;
            inSpeed[6] = 0.7f;
            inSpeed[7] = 0.94f;
            inSpeed[8] = 0.67f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            //Time of moles popping out sequently 
            outSpeed[0] = 0;
            outSpeed[1] = 0;
            outSpeed[2] = 0;
            outSpeed[3] = 0;
            outSpeed[4] = 0;
            outSpeed[5] = 0;
            outSpeed[6] = 0;
            outSpeed[7] = 0;
            outSpeed[8] = 0;

            //Time of moles staying out sequently 
            inSpeed[0] = 0.9f;
            inSpeed[1] = 0.8f;
            inSpeed[2] = 0.8f;
            inSpeed[3] = 1.05f;
            inSpeed[4] = 1.3f;
            inSpeed[5] = 0.8f;
            inSpeed[6] = 0.8f;
            inSpeed[7] = 0.94f;
            inSpeed[8] = 0.8f;
        }
        else
        {
            for (int a = 1; a <= 4; a++)
            {
                if (SceneManager.GetActiveScene().name == "S 2 lvl " + a)
                {
                    if(SceneManager.GetActiveScene().name == "S 2 lvl 4")
                    {
                        //Time of moles popping out sequently 
                        outSpeed[0] = 0;
                        outSpeed[1] = 0;
                        outSpeed[2] = 0;
                        outSpeed[3] = 0;
                        outSpeed[4] = 0;
                        outSpeed[5] = 0;
                        outSpeed[6] = 0;
                        outSpeed[7] = 0;
                        outSpeed[8] = 0;

                        //Time of moles staying out sequently 
                        inSpeed[0] = 0.94f;
                        inSpeed[1] = 1.6f;
                        inSpeed[2] = 0.8f;
                        inSpeed[3] = 1.05f;
                        inSpeed[4] = 1.3f;
                        inSpeed[5] = 0.8f;
                        inSpeed[6] = 0.8f;
                        inSpeed[7] = 1.25f;
                        inSpeed[8] = 0.8f;
                    }
                    else
                    {
                        //Time of moles popping out sequently 
                        outSpeed[0] = 0.2f;
                        outSpeed[1] = 1f;
                        outSpeed[2] = 0.05f;
                        outSpeed[3] = 1f;
                        outSpeed[4] = 0.8f;
                        outSpeed[5] = 0.35f;
                        outSpeed[6] = 0.2f;
                        outSpeed[7] = 0.5f;
                        outSpeed[8] = 0.05f;

                        //Time of moles staying out sequently 
                        inSpeed[0] = 0.94f;
                        inSpeed[1] = 1.6f;
                        inSpeed[2] = 0.8f;
                        inSpeed[3] = 1.05f;
                        inSpeed[4] = 1.3f;
                        inSpeed[5] = 0.68f;
                        inSpeed[6] = 0.7f;
                        inSpeed[7] = 1.25f;
                        inSpeed[8] = 0.7f;
                    }
                }
                else if (SceneManager.GetActiveScene().name == "S 3 lvl " + a)
                {
                    if (SceneManager.GetActiveScene().name == "S 3 lvl 4")
                    {
                        //Time of moles popping out sequently 
                        outSpeed[0] = 0;
                        outSpeed[1] = 0;
                        outSpeed[2] = 0;
                        outSpeed[3] = 0;
                        outSpeed[4] = 0;
                        outSpeed[5] = 0;
                        outSpeed[6] = 0;
                        outSpeed[7] = 0;
                        outSpeed[8] = 0;

                        //Time of moles staying out sequently 
                        inSpeed[0] = 1.2f;
                        inSpeed[1] = 1.6f;
                        inSpeed[2] = 0.8f;
                        inSpeed[3] = 1.05f;
                        inSpeed[4] = 1.3f;
                        inSpeed[5] = 1.65f;
                        inSpeed[6] = 1.2f;
                        inSpeed[7] = 1.25f;
                        inSpeed[8] = 0.8f;
                    }
                    else
                    {
                        //Time of moles popping out sequently 
                        outSpeed[0] = 0.2f;
                        outSpeed[1] = 1f;
                        outSpeed[2] = 0.05f;
                        outSpeed[3] = 1f;
                        outSpeed[4] = 0.8f;
                        outSpeed[5] = 0.35f;
                        outSpeed[6] = 0.2f;
                        outSpeed[7] = 0.5f;
                        outSpeed[8] = 0.05f;

                        //Time of moles staying out sequently 
                        inSpeed[0] = 1.2f;
                        inSpeed[1] = 1.6f;
                        inSpeed[2] = 0.75f;
                        inSpeed[3] = 1.05f;
                        inSpeed[4] = 1.3f;
                        inSpeed[5] = 1.65f;
                        inSpeed[6] = 1.2f;
                        inSpeed[7] = 1.25f;
                        inSpeed[8] = 0.7f;
                    }
                }
                else if (SceneManager.GetActiveScene().name == "S 4 lvl " + a)
                {
                    if (SceneManager.GetActiveScene().name == "S 4 lvl 4")
                    {
                        //Time of moles popping out sequently 
                        outSpeed[0] = 0;
                        outSpeed[1] = 0;
                        outSpeed[2] = 0;
                        outSpeed[3] = 0;
                        outSpeed[4] = 0;
                        outSpeed[5] = 0;
                        outSpeed[6] = 0;
                        outSpeed[7] = 0;
                        outSpeed[8] = 0;

                        //Time of moles staying out sequently 
                        inSpeed[0] = 1.2f;
                        inSpeed[1] = 1.6f;
                        inSpeed[2] = 0.8f;
                        inSpeed[3] = 1.05f;
                        inSpeed[4] = 1.3f;
                        inSpeed[5] = 0.8f;
                        inSpeed[6] = 1.2f;
                        inSpeed[7] = 1.25f;
                        inSpeed[8] = 1.3f;
                    }
                    else
                    {
                        //Time of moles popping out sequently 
                        outSpeed[0] = 0.2f;
                        outSpeed[1] = 0.6f;
                        outSpeed[2] = 0.05f;
                        outSpeed[3] = 0.7f;
                        outSpeed[4] = 0.3f;
                        outSpeed[5] = 0.8f;
                        outSpeed[6] = 0.2f;
                        outSpeed[7] = 0.5f;
                        outSpeed[8] = 0.3f;

                        //Time of moles staying out sequently 
                        inSpeed[0] = 1.2f;
                        inSpeed[1] = 1.15f;
                        inSpeed[2] = 0.75f;
                        inSpeed[3] = 1.05f;
                        inSpeed[4] = 1.1f;
                        inSpeed[5] = 1.35f;
                        inSpeed[6] = 1f;
                        inSpeed[7] = 1.15f;
                        inSpeed[8] = 1.25f;
                    }
                }
            }
        }
    }

    public IEnumerator molePoper()
    {
        if (moleLimit < moleMax && !inMolePop)
        {
            if (moleNum < molePatternL.Count - 1)
            {
                moleNum++;
            }
            else
            {
                moleNum = 0;
            }

            inMolePop = true;
            moleLimit++;

            if (!hitSc.gameOver)
            {
                yield return new WaitForSeconds(outSpeed[moleNum]);

                pressed = false;

                sound.GetComponent<AudioSource>().PlayOneShot(outSound, 0.8f);
                moles[molePatternL[moleNum]].GetComponent<Animation>().Play();
                moles[molePatternL[moleNum]].GetComponent<multiMole>().multiUp(inSpeed[moleNum]);
                oneOn = true;

                if (moleNum == 3)
                {
                    moles[8].GetComponent<multiMole>().multiUp(inSpeed[3]);

                    twoOn = true;
                    moleLimit += 1;
                    GetComponent<moleHit>().hitNow[8] = true;
                }
                //Three mole
                else if (moleNum == 4)
                {
                    moles[3].GetComponent<multiMole>().multiUp(inSpeed[4]);
                    moles[5].GetComponent<multiMole>().multiUp(inSpeed[4]);

                    threeOn = true;
                    moleLimit += 2;
                    GetComponent<moleHit>().hitNow[3] = true;
                    GetComponent<moleHit>().hitNow[5] = true;
                }
                for (int a = 1; a <= 4; a++)
                {
                    if (SceneManager.GetActiveScene().name == "S 2 lvl " + a)
                    {
                        //Three mole
                        if (moleNum == 7)
                        {
                            moles[6].GetComponent<multiMole>().multiUp(inSpeed[7]);
                            moles[8].GetComponent<multiMole>().multiUp(inSpeed[7]);

                            threeOn = true;
                            moleLimit += 2;
                            GetComponent<moleHit>().hitNow[6] = true;
                            GetComponent<moleHit>().hitNow[8] = true;
                        }
                        //four mole
                        else if (moleNum == 1)
                        {
                            moles[2].GetComponent<multiMole>().multiUp(inSpeed[1]);
                            moles[7].GetComponent<multiMole>().multiUp(inSpeed[1]);
                            moles[0].GetComponent<multiMole>().multiUp(inSpeed[1]);

                            fourOn = true;
                            moleLimit += 3;
                            GetComponent<moleHit>().hitNow[2] = true;
                            GetComponent<moleHit>().hitNow[7] = true;
                            GetComponent<moleHit>().hitNow[0] = true;
                        }
                    }
                    else if(SceneManager.GetActiveScene().name == "S 3 lvl " + a || SceneManager.GetActiveScene().name == "S 4 lvl " + a)
                    {
                        //Three mole
                        if (moleNum == 7)
                        {
                            moles[6].GetComponent<multiMole>().multiUp(inSpeed[7]);
                            moles[8].GetComponent<multiMole>().multiUp(inSpeed[7]);

                            threeOn = true;
                            moleLimit += 2;
                            GetComponent<moleHit>().hitNow[6] = true;
                            GetComponent<moleHit>().hitNow[8] = true;
                        }
                        //Three mole
                        else if (moleNum == 6)
                        {
                            moles[5].GetComponent<multiMole>().multiUp(inSpeed[6]);
                            moles[4].GetComponent<multiMole>().multiUp(inSpeed[6]);

                            threeOn = true;
                            moleLimit += 2;
                            GetComponent<moleHit>().hitNow[5] = true;
                            GetComponent<moleHit>().hitNow[4] = true;
                        }
                        else if (moleNum == 0)
                        {
                            moles[4].GetComponent<multiMole>().multiUp(inSpeed[0]);
                            moles[1].GetComponent<multiMole>().multiUp(inSpeed[0]);

                            threeOn = true;
                            moleLimit += 2;
                            GetComponent<moleHit>().hitNow[4] = true;
                            GetComponent<moleHit>().hitNow[1] = true;
                        }
                        //four mole
                        else if (moleNum == 1)
                        {
                            moles[2].GetComponent<multiMole>().multiUp(inSpeed[1]);
                            moles[7].GetComponent<multiMole>().multiUp(inSpeed[1]);
                            moles[0].GetComponent<multiMole>().multiUp(inSpeed[1]);

                            fourOn = true;
                            moleLimit += 3;
                            GetComponent<moleHit>().hitNow[2] = true;
                            GetComponent<moleHit>().hitNow[7] = true;
                            GetComponent<moleHit>().hitNow[0] = true;
                        }
                        else if (SceneManager.GetActiveScene().name == "S 4 lvl " + a && moleNum == 5)
                        {
                            moles[7].GetComponent<multiMole>().multiUp(inSpeed[5]);
                            moles[6].GetComponent<multiMole>().multiUp(inSpeed[5]);
                            moles[4].GetComponent<multiMole>().multiUp(inSpeed[5]);
                            moles[1].GetComponent<multiMole>().multiUp(inSpeed[5]);

                            fiveOn = true;
                            moleLimit += 4;
                            GetComponent<moleHit>().hitNow[7] = true;
                            GetComponent<moleHit>().hitNow[4] = true;
                            GetComponent<moleHit>().hitNow[6] = true;
                            GetComponent<moleHit>().hitNow[1] = true;
                        }
                        else if (SceneManager.GetActiveScene().name == "S 4 lvl " + a && moleNum == 8)
                        {
                            moles[7].GetComponent<multiMole>().multiUp(inSpeed[8]);
                            moles[8].GetComponent<multiMole>().multiUp(inSpeed[8]);
                            moles[6].GetComponent<multiMole>().multiUp(inSpeed[8]);

                            fourOn = true;
                            moleLimit += 3;
                            GetComponent<moleHit>().hitNow[7] = true;
                            GetComponent<moleHit>().hitNow[8] = true;
                            GetComponent<moleHit>().hitNow[6] = true;
                        }
                    }
                }

                GetComponent<moleHit>().hitNow[molePatternL[moleNum]] = true;
                inMolePop = false;
            }

        }
    }

}
