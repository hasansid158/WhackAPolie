using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleSc : MonoBehaviour
{
    public GameObject[] moles;
    public int moleNum;
    public List<int> molePatternL;

    float moleTime;

    public GameObject sound, fader;
    public AudioClip inSound, outSound;

    public float outSpeed, inSpeed;

    bool once;

    void Start()
    {
        StartCoroutine(molePoper());
    }

    void Update()
    {
        moleTime += Time.deltaTime;

        if (moleTime > inSpeed)
        {
            moleTime = 0;

            sound.GetComponent<AudioSource>().PlayOneShot(inSound, 0.75f);
            moles[molePatternL[moleNum]].GetComponent<Animation>().Play("goIn");

            if (moleNum < molePatternL.Count - 1)
            {
                moleNum++;
            }
            else
            {
                moleNum = 0;
            }
            StartCoroutine(molePoper());
        }
    }

    public IEnumerator molePoper()
    {
        yield return new WaitForSeconds(outSpeed);
        moleTime = 0;
        sound.GetComponent<AudioSource>().PlayOneShot(outSound, 0.8f);
        moles[molePatternL[moleNum]].GetComponent<Animation>().Play();
    }

    public void playNow()
    {
        if (!once)
        {
            once = true;
            StartCoroutine(playWait());
        }
    }

    IEnumerator playWait()
    {
        fader.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(1);
    }
}
