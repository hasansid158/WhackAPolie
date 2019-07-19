using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class butSc : MonoBehaviour
{
    public GameObject startCan, go;
    moleUp upSc;
    public bool paused;
    public GameObject[] count123;
    Canvas pauseCan;

    AudioSource hornAudioS;

    moleHit hitSc;
    bool reOnce;

    void Start()
    {
        Application.targetFrameRate = 60;

        pauseCan = GameObject.FindGameObjectWithTag("pauseCan").GetComponent<Canvas>();
        pauseCan.GetComponent<Canvas>().worldCamera = Camera.main;
        hitSc = GetComponent<moleHit>();
        hornAudioS = GameObject.FindGameObjectWithTag("hornS").GetComponent<AudioSource>();
        upSc = GetComponent<moleUp>();
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().name == "S 1 lvl 4" || SceneManager.GetActiveScene().name == "S 2 lvl 4" || SceneManager.GetActiveScene().name == "S 3 lvl 4" || SceneManager.GetActiveScene().name == "S 4 lvl 4")
        {
            startCan.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "S 1 lvl 1" || SceneManager.GetActiveScene().name == "S 2 lvl 1" || SceneManager.GetActiveScene().name == "S 3 lvl 1" || SceneManager.GetActiveScene().name == "S 4 lvl 1")
        {
            StartCoroutine(countDown());
        }
        else
        {
            upSc.started = true;
            StartCoroutine(upSc.molePoper());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void quitBut()
    {
        Application.Quit();
    }

    public void playBut()
    {
        StartCoroutine(countDown());
        startCan.SetActive(false);
    }

    public void pauseBut()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0;
            pauseCan.enabled = true;
        }
        else
        {
            paused = false;
            Time.timeScale = 1;
            pauseCan.enabled = false;
            reOnce = false;
        }
    }

    IEnumerator countDown()
    {
        for (int a = 0; a < count123.Length; a++)
        {
            count123[a].GetComponent<Animation>().Play();
            if (a > 0)
            {
                hornAudioS.PlayOneShot(hornAudioS.clip);
            }
            yield return new WaitForSeconds(0.85f);
        }
        go.GetComponent<Animation>().Play();
        hornAudioS.pitch = 1.3f;
        hornAudioS.volume = 0.85f;
        hornAudioS.PlayOneShot(hornAudioS.clip);
        yield return new WaitForSeconds(0.5f);
        upSc.started = true;
        StartCoroutine(upSc.molePoper());
    }

    public void oKBut()
    {
        GameObject.FindGameObjectWithTag("firstSeg").transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1;
        hitSc.paused = false;
    }

    public void reBut()
    {
        if (!reOnce)
        {
            reOnce = true;
            hitSc.reNow = true;

            paused = false;
            Time.timeScale = 1;
            pauseCan.enabled = false;
            reOnce = false;

            StartCoroutine(hitSc.reWait());
        }
    }
}
