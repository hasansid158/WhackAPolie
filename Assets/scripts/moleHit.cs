using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class moleHit : MonoBehaviour
{
    public bool[] hitNow;
    RaycastHit2D[] hit;
    [HideInInspector]
    public moleUp upSc;

    //int lives = 2;

    public GameObject gameOverCan, gameOverScore, gameOverHigh, cam, starPar, sound, hugeHamr;
    GameObject fader;
    public bool gameOver, once;

    public AudioClip hitSound, loseSound, godMalletSound;

    public int comboHits;
    bool changeHits;

    public GameObject lvlCompleteTxt, gameOvrImg, replyButText, replyButText2;

    public string[] ranksText;
    public int[] rankPoints;

    [HideInInspector]
    public GameObject bonusGauge;

    scoreSaver sScript;

    //bool cleared;

    butSc bSc;

    public GameObject popperPart;

    GameObject rankTextScreen, scoreText, hearts, heartsBroken, uiCanvas;

    public bool bigMalletTimer, lastHit, paused;

    public GameObject lvlCompCanvas, lvlCompBut, replayBut, quitBut;

    public GameObject[] gameOverRanks;

    public ScrollSnapRect scrolSc;

    float tempOut;
    float tempIn;

    bool popped;
    Touch touch;

    [HideInInspector]
    public bool twoMolesHit;

    [HideInInspector]
    public bool reNow;

    float tempMole;

    float autoTimer;

    [HideInInspector]
    public int multiMoleHits;

    [HideInInspector]
    public bool bonusComplete;

    void Awake()
    {
        uiCanvas = GameObject.FindGameObjectWithTag("uiCanvas");
        uiCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
        uiCanvas.GetComponent<Canvas>().enabled = true;
        heartsBroken = GameObject.FindGameObjectWithTag("heartB");
        hearts = GameObject.FindGameObjectWithTag("heart");
        bonusGauge = GameObject.FindGameObjectWithTag("gauge");
        scoreText = GameObject.FindGameObjectWithTag("scoreTxt");
        rankTextScreen = GameObject.FindGameObjectWithTag("rankText");
        fader = GameObject.FindGameObjectWithTag("fader");

        upSc = GetComponent<moleUp>();
        bSc = GetComponent<butSc>();
        sScript = GameObject.FindGameObjectWithTag("ss").GetComponent<scoreSaver>();
    }

    void Start()
    {
        //tempOut = upSc.outSpeed[upSc.moleNum];
        //tempIn = upSc.inSpeed[upSc.moleNum];

        hit = new RaycastHit2D[6];
        hitNow = new bool[upSc.moles.Length];

        for (int x = 0; x < sScript.gaugeNum; x++)
        {
            bonusGauge.transform.GetChild(x).GetComponent<Slider>().value = bonusGauge.transform.GetChild(x).GetComponent<Slider>().maxValue;
            bonusGauge.transform.GetChild(x).transform.GetChild(4).gameObject.SetActive(true);
        }
        if (sScript.gaugeNum < 5)
        {
            bonusGauge.transform.GetChild(sScript.gaugeNum).GetComponent<Slider>().value = sScript.gaugeScore;
        }

        scoreText.GetComponent<Text>().text = sScript.score.ToString();
        scoreText.transform.GetChild(0).GetComponent<Text>().text = sScript.score.ToString();
        //checkRank();

        for (int a = gameOverRanks.Length - 1; a >= 0; a--)
        {
            gameOverRanks[a].GetComponent<Text>().text = ranksText[13 - a].ToUpper();
        }
    }

    void Update()
    {
        ////Auto
        //if (!gameOver && upSc.started && !bSc.paused && !paused)
        //{
        //    if (autoTimer > upSc.outSpeed[upSc.moleNum] + Random.Range(0.35f, 0.5f))
        //    {
        //        for (int a = 0; a < upSc.moles.Length; a++)
        //        {
        //            if (hitNow[a])
        //            {
        //                if (upSc.twoOn && a == 8)
        //                {
        //                    upSc.twoOn = false;
        //                    upSc.pressed2 = true;
        //                    hitNow[a] = false;
        //                    upSc.moleTime2 = 0;

        //                    if (upSc.lastMole)
        //                    {
        //                        lastHit = true;
        //                        gameEnded();
        //                    }
        //                    else
        //                    {
        //                        if (!twoMolesHit)
        //                        {
        //                            twoMolesHit = true;
        //                        }
        //                        else
        //                        {
        //                            StartCoroutine(upSc.molePoper());
        //                            twoMolesHit = false;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    upSc.pressed = true;
        //                    hitNow[a] = false;

        //                    sScript.score += 150;
        //                    scoreText.GetComponent<Animation>().Play();
        //                    scoreText.GetComponent<Text>().text = sScript.score.ToString();
        //                    scoreText.transform.GetChild(0).GetComponent<Text>().text = sScript.score.ToString();
        //                    upSc.missMole = 0;

        //                    if (upSc.lastMole)
        //                    {
        //                        lastHit = true;
        //                        gameEnded();
        //                    }
        //                    else
        //                    {
        //                        if (!upSc.twoOn)
        //                        {
        //                            StartCoroutine(upSc.molePoper());
        //                        }
        //                        else if (!twoMolesHit)
        //                        {
        //                            twoMolesHit = true;
        //                        }
        //                        else
        //                        {
        //                            StartCoroutine(upSc.molePoper());
        //                            twoMolesHit = false;
        //                        }
        //                    }
        //                }

        //                sound.GetComponent<AudioSource>().PlayOneShot(hitSound, 0.9f);
        //                cam.GetComponent<Animation>().Play();
        //                upSc.moles[a].transform.parent.parent.GetChild(0).GetComponent<Animation>().Play();
        //                upSc.moles[a].GetComponent<Animation>().Play("goInFast");
        //                starPar.transform.position = new Vector3(upSc.moles[a].transform.parent.parent.GetChild(0).transform.position.x - 0.56f, upSc.moles[a].transform.parent.parent.GetChild(0).transform.position.y - 0.25f, 0);
        //                starPar.GetComponent<ParticleSystem>().Emit(10);

        //                comboHits++;
        //                upSc.moles[a].transform.parent.parent.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = comboHits + "X";
        //                upSc.moles[a].transform.parent.parent.GetChild(3).GetChild(0).GetChild(1).GetComponent<Text>().text = comboHits + "X";
        //                upSc.moles[a].transform.parent.parent.GetChild(3).GetChild(0).GetComponent<Animation>().Play();

        //                for (int x = 0; x < 5; x++)
        //                {
        //                    if (sScript.gaugeNum == x)
        //                    {
        //                        if (sScript.gaugeNum < 5)
        //                        {
        //                            sScript.gaugeScore++;
        //                            bonusGauge.transform.GetChild(sScript.gaugeNum).GetComponent<Slider>().value = sScript.gaugeScore;

        //                            if (bonusGauge.transform.GetChild(sScript.gaugeNum).GetComponent<Slider>().value >= bonusGauge.transform.GetChild(sScript.gaugeNum).GetComponent<Slider>().maxValue)
        //                            {
        //                                if (sScript.gaugeNum < 5)
        //                                {
        //                                    bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color = new Color(bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color.r,
        //                                        bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color.g,
        //                                        bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color.b, 1);

        //                                    bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(4).gameObject.SetActive(true);
        //                                    sScript.gaugeScore = 0;
        //                                    sScript.gaugeNum++;
        //                                    x = 5;
        //                                    comboHits = 0;

        //                                    if (sScript.firstSegment)
        //                                    {
        //                                        sScript.firstSegment = false;
        //                                        StartCoroutine("firstSegWait");
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            sScript.gaugeScore++;
        //                        }
        //                    }
        //                }
        //                checkRank();
        //                autoTimer = 0;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log(autoTimer);
        //        autoTimer += Time.deltaTime;
        //    }
        //}


        //Clicking
        if (Input.touchCount > 0 && Input.touchCount <= 5 && !gameOver && upSc.started && !bSc.paused && !paused/* && Input.GetMouseButtonDown(0)*/)
        {
            touch = Input.GetTouch(Input.touchCount - 1);
            hit[Input.touchCount] = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position/*Input.mousePosition*/), Vector2.zero);

            if (touch.phase == TouchPhase.Began || Application.platform == RuntimePlatform.WindowsEditor)
            {
                for (int a = 0; a < upSc.moles.Length; a++)
                {
                    if (hit[Input.touchCount].collider != null && hit[Input.touchCount].collider.gameObject == upSc.moles[a].transform.parent.parent.gameObject)
                    {
                        if (hitNow[a])
                        {
                            if (upSc.twoOn && upSc.moles[a].GetComponent<multiMole>().multiPop)
                            {
                                upSc.moles[a].GetComponent<multiMole>().multiPop = false;
                                hitNow[a] = false;
                                upSc.twoOn = false;

                                if (multiMoleHits > 1)
                                {
                                    if (upSc.twoHits)
                                    {
                                        upSc.twoOn = false;
                                        multiMoleHits = 0;
                                        upSc.twoHits = false;

                                        if (upSc.moleLimit < upSc.moleMax)
                                        {
                                            StartCoroutine(upSc.molePoper());
                                        }
                                        else
                                        {
                                            gameEnded();
                                        }
                                    }
                                }
                                else
                                {
                                    upSc.twoHits = true;
                                    multiMoleHits++;
                                }
                            }
                            else if (upSc.threeOn && upSc.moles[a].GetComponent<multiMole>().multiPop)
                            {
                                upSc.moles[a].GetComponent<multiMole>().multiPop = false;
                                hitNow[a] = false;

                                if (multiMoleHits >= 2)
                                {
                                    multiMoleHits = 0;
                                    upSc.threeOn = false;

                                    if (upSc.moleLimit >= upSc.moleMax)
                                    {
                                        gameEnded();
                                        lastHit = true;
                                    }
                                    else
                                    {
                                        StartCoroutine(upSc.molePoper());
                                    }
                                }
                                else
                                {
                                    multiMoleHits++;
                                }
                            }
                            else if (upSc.fourOn && upSc.moles[a].GetComponent<multiMole>().multiPop)
                            {
                                upSc.moles[a].GetComponent<multiMole>().multiPop = false;
                                hitNow[a] = false;

                                if (multiMoleHits >= 3)
                                {
                                    multiMoleHits = 0;
                                    upSc.fourOn = false;

                                    if (upSc.moleLimit >= upSc.moleMax)
                                    {
                                        gameEnded();
                                        lastHit = true;
                                    }
                                    else
                                    {
                                        StartCoroutine(upSc.molePoper());
                                    }
                                }
                                else
                                {
                                    multiMoleHits++;
                                }
                            }
                            else if (upSc.fiveOn && upSc.moles[a].GetComponent<multiMole>().multiPop)
                            {
                                upSc.moles[a].GetComponent<multiMole>().multiPop = false;
                                hitNow[a] = false;

                                if (multiMoleHits >= 4)
                                {
                                    multiMoleHits = 0;
                                    upSc.fiveOn = false;

                                    if (upSc.moleLimit >= upSc.moleMax)
                                    {
                                        gameEnded();
                                        lastHit = true;
                                    }
                                    else
                                    {
                                        StartCoroutine(upSc.molePoper());
                                    }
                                }
                                else
                                {
                                    multiMoleHits++;
                                }
                            }
                            else
                            {
                                multiMoleHits++;
                                if (upSc.twoOn && !upSc.twoHits)
                                {
                                    upSc.twoHits = true;

                                    if (upSc.moleLimit < upSc.moleMax)
                                    {
                                        StartCoroutine(upSc.molePoper());
                                    }
                                    else
                                    {
                                        gameEnded();
                                    }
                                }
                                else
                                {
                                    upSc.moles[a].GetComponent<multiMole>().multiPop = false;
                                    hitNow[a] = false;

                                    if (upSc.twoOn)
                                    {
                                        if (upSc.twoHits)
                                        {
                                            upSc.twoOn = false;
                                            multiMoleHits = 0;
                                            upSc.twoHits = false;

                                            if (upSc.moleLimit < upSc.moleMax)
                                            {
                                                StartCoroutine(upSc.molePoper());
                                            }
                                            else
                                            {
                                                gameEnded();
                                            }
                                        }
                                        else
                                        {
                                            multiMoleHits++;
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
                                            gameEnded();
                                        }
                                    }
                                }
                            }
                            sScript.score += 150;
                            scoreText.GetComponent<Animation>().Play();
                            scoreText.GetComponent<Text>().text = sScript.score.ToString();
                            scoreText.transform.GetChild(0).GetComponent<Text>().text = sScript.score.ToString();
                            upSc.missMole = 0;

                            sound.GetComponent<AudioSource>().PlayOneShot(hitSound, 0.9f);
                            cam.GetComponent<Animation>().Play();
                            upSc.moles[a].transform.parent.parent.GetChild(0).GetComponent<Animation>().Play();
                            upSc.moles[a].GetComponent<Animation>().Play("goInFast");
                            starPar.transform.position = new Vector3(upSc.moles[a].transform.parent.parent.GetChild(0).transform.position.x - 0.56f, upSc.moles[a].transform.parent.parent.GetChild(0).transform.position.y - 0.25f, 0);
                            starPar.GetComponent<ParticleSystem>().Emit(10);

                            comboHits++;
                            upSc.moles[a].transform.parent.parent.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = comboHits + "X";
                            upSc.moles[a].transform.parent.parent.GetChild(3).GetChild(0).GetChild(1).GetComponent<Text>().text = comboHits + "X";
                            upSc.moles[a].transform.parent.parent.GetChild(3).GetChild(0).GetComponent<Animation>().Play();

                            for (int x = 0; x < 5; x++)
                            {
                                if (sScript.gaugeNum == x)
                                {
                                    if (sScript.gaugeNum < 5)
                                    {
                                        sScript.gaugeScore++;
                                        bonusGauge.transform.GetChild(sScript.gaugeNum).GetComponent<Slider>().value = sScript.gaugeScore;

                                        if (bonusGauge.transform.GetChild(sScript.gaugeNum).GetComponent<Slider>().value >= bonusGauge.transform.GetChild(sScript.gaugeNum).GetComponent<Slider>().maxValue)
                                        {
                                            if (sScript.gaugeNum < 5)
                                            {
                                                bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color = new Color(bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color.r,
                                                    bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color.g,
                                                    bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color.b, 1);

                                                bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(4).gameObject.SetActive(true);
                                                sScript.gaugeScore = 0;
                                                sScript.gaugeNum++;
                                                x = 5;
                                                comboHits = 0;

                                                if (sScript.firstSegment)
                                                {
                                                    sScript.firstSegment = false;
                                                    StartCoroutine("firstSegWait");
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        sScript.gaugeScore++;
                                    }
                                }
                            }
                            checkRank();
                        }
                        else
                        {
                            upSc.moles[a].transform.parent.parent.GetChild(0).GetComponent<Animation>().Play();

                            //lose Streak
                            //gaugeScore = 0;
                            //if (sScript.gaugeNum < 5)
                            //{
                            //    bonusGauge.transform.GetChild(sScript.gaugeNum).GetComponent<Slider>().value = gaugeScore;
                            //}
                            //comboHits = 0;
                        }
                    }
                }
            }
        }
    }

    IEnumerator firstSegWait()
    {
        yield return new WaitForSeconds(0.3f);
        paused = true;
        Time.timeScale = 0;
        GameObject.FindGameObjectWithTag("firstSeg").transform.GetChild(0).gameObject.SetActive(true);
    }

    //public void loseLife()
    //{
    //    sound.GetComponent<AudioSource>().PlayOneShot(loseSound, 0.85f);

    //    if (lives >= 0)
    //    {
    //        hearts.transform.GetChild(lives).gameObject.SetActive(false);
    //        heartsBroken.transform.GetChild(lives).transform.position = hearts.transform.GetChild(lives).transform.position;
    //        heartsBroken.transform.GetChild(lives).gameObject.SetActive(true);
    //        lives--;
    //        if (lives < 0)
    //        {
    //            gameOver = true;
    //            gameOverScore.GetComponent<Text>().text = "SCORE : " + sScript.score;
    //            gameOverScore.transform.GetChild(0).GetComponent<Text>().text = "SCORE : " + sScript.score;

    //            destroyObjs();

    //            gameOvrImg.SetActive(true);
    //            gameOverCan.SetActive(true);

    //            StartCoroutine(loseLifeWait());
    //        }
    //    }
    //}

    IEnumerator loseLifeWait()
    {
        checkRank();
        yield return new WaitForSeconds(0.4f);
        scrolSc.nextText(sScript.rankIndex);
        yield return new WaitForSeconds(1f);
        if (SceneManager.GetActiveScene().name != "S 4 lvl 4")
        {
            replayBut.SetActive(true);
        }
        quitBut.SetActive(true);
    }

    public void gameEnded()
    {
        if (SceneManager.GetActiveScene().name != "S 1 lvl 4" && SceneManager.GetActiveScene().name != "S 2 lvl 4" && SceneManager.GetActiveScene().name != "S 3 lvl 4" && SceneManager.GetActiveScene().name != "S 4 lvl 4")
        {
            gameOver = true;
            StartCoroutine(lvlCompWait());
        }
        else if (bonusComplete)
        {
            gameOver = true;
            StartCoroutine(lvlCompWait());
        }
        else
        {
            lastHit = false;
            upSc.moleLimit = -1;
            StartCoroutine(upSc.molePoper());
        }

        //else if (SceneManager.GetActiveScene().buildIndex == sScript.stageCheckNum)
        //{
        //    lvlCompleteTxt.GetComponent<Text>().text = "STAGE " + sScript.stageCompNum + "\nCOMPLETE!";
        //    lvlCompleteTxt.transform.GetChild(0).GetComponent<Text>().text = "STAGE 1\nCOMPLETE!";
        //    popperPart.GetComponent<ParticleSystem>().Play();
        //    if (sScript.stageCheckNum >= 24)
        //    {
        //        sScript.stageCheckNum = 5;
        //        sScript.stageCompNum = 1;
        //    }
        //    else
        //    {
        //        closeObjs();
        //        sScript.stageCompNum++;
        //        sScript.stageCheckNum += 5;
        //    }
        //}
    }

    IEnumerator lvlCompWait()
    {
        //lvlCompCanvas.SetActive(true);

        if (SceneManager.GetActiveScene().name == "S 1 lvl 4" || SceneManager.GetActiveScene().name == "S 2 lvl 4" || SceneManager.GetActiveScene().name == "S 3 lvl 4" || SceneManager.GetActiveScene().name == "S 4 lvl 4")
        {
            popperPart.GetComponent<ParticleSystem>().Play();
            lvlCompCanvas.SetActive(true);
            yield return new WaitForSeconds(1.35f);
            uiCanvas.GetComponent<Canvas>().enabled = false;
            gameOvrImg.SetActive(false);
            lvlCompleteTxt.SetActive(true);
            gameOverScore.GetComponent<Text>().text = "SCORE : " + sScript.score;
            gameOverScore.transform.GetChild(0).GetComponent<Text>().text = "SCORE : " + sScript.score;
            gameOverCan.SetActive(true);
            StartCoroutine(loseLifeWait());
        }
        else
        {
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(scenePreloaded());
        }
    }

    public void resetOrNext()
    {
        if (!once)
        {
            once = true;
            StartCoroutine(reWait());
        }
    }

    IEnumerator scenePreloaded()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator reWait()
    {
        fader.GetComponent<Animation>().Play("fadeIn");
        yield return new WaitForSeconds(0.6f);

        sScript.score = 0;
        sScript.rankIndex = 14;
        sScript.gaugeScore = 0;

        if (!reNow)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            while (sScript.gaugeNum >= 0)
            {
                if (sScript.gaugeNum < 5)
                {
                    bonusGauge.transform.GetChild(sScript.gaugeNum).GetComponent<Slider>().value = sScript.gaugeScore;
                }

                bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color = new Color32((byte)(bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color.r * 255),
        (byte)(bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color.g * 255),
        (byte)(bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color.b * 255), 140);
                bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(4).gameObject.SetActive(false);

                sScript.gaugeNum--;
            }
            sScript.gaugeNum = 0;
            SceneManager.LoadScene(0);
        }


        //else if (SceneManager.GetActiveScene().buildIndex == 24)
        //{
        //    fader.GetComponent<Animation>().Play("fadeIn");
        //    yield return new WaitForSeconds(0.7f);
        //    destroyObjs();
        //    sScript.score = 0;
        //    sScript.gaugeNum = 0;
        //    sScript.rankIndex = 14;
        //    SceneManager.LoadScene(0);
        //}
    }

    public void hugeMalletHit()
    {
        if (!hugeHamr.GetComponent<Animator>().GetAnimatorTransitionInfo(0).IsName("hugeHammerHit") && !gameOver)
        {
            hugeHamr.GetComponent<Animator>().SetBool("hamDown", true);
            GameObject.FindGameObjectWithTag("lightning").GetComponent<Animator>().SetBool("light", true);
            StartCoroutine(timePauseHam());
            //bigMalletTimer = true;
            StartCoroutine(hammerWait());

            for (int a = 0; a < hitNow.Length; a++)
            {
                if (hitNow[a])
                {
                    if (upSc.twoOn && upSc.moles[a].GetComponent<multiMole>().multiPop)
                    {
                        upSc.moles[a].GetComponent<multiMole>().multiPop = false;
                        hitNow[a] = false;
                        upSc.twoOn = false;

                        if (multiMoleHits > 1)
                        {
                            if (upSc.twoHits)
                            {
                                upSc.twoOn = false;
                                multiMoleHits = 0;
                                upSc.twoHits = false;

                                if (upSc.moleLimit < upSc.moleMax)
                                {
                                    StartCoroutine(upSc.molePoper());
                                }
                                else
                                {
                                    gameEnded();
                                }
                            }
                        }
                        else
                        {
                            upSc.twoHits = true;
                            multiMoleHits++;
                        }
                    }
                    else if (upSc.threeOn && upSc.moles[a].GetComponent<multiMole>().multiPop)
                    {
                        upSc.moles[a].GetComponent<multiMole>().multiPop = false;
                        hitNow[a] = false;

                        if (multiMoleHits >= 2)
                        {
                            multiMoleHits = 0;
                            upSc.threeOn = false;

                            if (upSc.moleLimit >= upSc.moleMax)
                            {
                                gameEnded();
                                lastHit = true;
                            }
                            else
                            {
                                StartCoroutine(upSc.molePoper());
                            }
                        }
                        else
                        {
                            multiMoleHits++;
                        }
                    }
                    else if (upSc.fourOn && upSc.moles[a].GetComponent<multiMole>().multiPop)
                    {
                        upSc.moles[a].GetComponent<multiMole>().multiPop = false;
                        hitNow[a] = false;

                        if (multiMoleHits >= 3)
                        {
                            multiMoleHits = 0;
                            upSc.fourOn = false;

                            if (upSc.moleLimit >= upSc.moleMax)
                            {
                                gameEnded();
                                lastHit = true;
                            }
                            else
                            {
                                StartCoroutine(upSc.molePoper());
                            }
                        }
                        else
                        {
                            multiMoleHits++;
                        }
                    }
                    else if (upSc.fiveOn && upSc.moles[a].GetComponent<multiMole>().multiPop)
                    {
                        upSc.moles[a].GetComponent<multiMole>().multiPop = false;
                        hitNow[a] = false;

                        if (multiMoleHits >= 4)
                        {
                            multiMoleHits = 0;
                            upSc.fiveOn = false;

                            if (upSc.moleLimit >= upSc.moleMax)
                            {
                                gameEnded();
                                lastHit = true;
                            }
                            else
                            {
                                StartCoroutine(upSc.molePoper());
                            }
                        }
                        else
                        {
                            multiMoleHits++;
                        }
                    }
                    else
                    {
                        multiMoleHits++;
                        if (upSc.twoOn && !upSc.twoHits)
                        {
                            upSc.twoHits = true;

                            if (upSc.moleLimit < upSc.moleMax)
                            {
                                StartCoroutine(upSc.molePoper());
                            }
                            else
                            {
                                gameEnded();
                            }
                        }
                        else
                        {
                            upSc.moles[a].GetComponent<multiMole>().multiPop = false;
                            hitNow[a] = false;

                            if (upSc.twoOn)
                            {
                                if (upSc.twoHits)
                                {
                                    upSc.twoOn = false;
                                    multiMoleHits = 0;
                                    upSc.twoHits = false;

                                    if (upSc.moleLimit < upSc.moleMax)
                                    {
                                        StartCoroutine(upSc.molePoper());
                                    }
                                    else
                                    {
                                        gameEnded();
                                    }
                                }
                                else
                                {
                                    multiMoleHits++;
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
                                    gameEnded();
                                }
                            }
                        }
                    }

                    sScript.score += 150;
                    scoreText.GetComponent<Animation>().Play();
                    scoreText.GetComponent<Text>().text = sScript.score.ToString();
                    scoreText.transform.GetChild(0).GetComponent<Text>().text = sScript.score.ToString();
                    upSc.missMole = 0;

                    sound.GetComponent<AudioSource>().PlayOneShot(hitSound, 0.9f);
                    cam.GetComponent<Animation>().Play();
                    upSc.moles[a].GetComponent<Animation>().Play("goInFast");
                    starPar.transform.position = new Vector3(upSc.moles[a].transform.parent.parent.GetChild(0).transform.position.x - 0.56f, upSc.moles[a].transform.parent.parent.GetChild(0).transform.position.y - 0.25f, 0);
                    starPar.GetComponent<ParticleSystem>().Emit(10);

                    comboHits++;
                    upSc.moles[a].transform.parent.parent.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = comboHits + "X";
                    upSc.moles[a].transform.parent.parent.GetChild(3).GetChild(0).GetChild(1).GetComponent<Text>().text = comboHits + "X";
                    upSc.moles[a].transform.parent.parent.GetChild(3).GetChild(0).GetComponent<Animation>().Play();

                    checkRank();
                }
            }

            sScript.gaugeScore = 0;
            if (sScript.gaugeNum < 5)
            {
                bonusGauge.transform.GetChild(sScript.gaugeNum).GetComponent<Slider>().value = sScript.gaugeScore;
            }
            if (sScript.gaugeNum > 0)
            {
                sScript.gaugeNum--;
            }
            if (sScript.gaugeNum < 5)
            {
                bonusGauge.transform.GetChild(sScript.gaugeNum).GetComponent<Slider>().value = sScript.gaugeScore;
            }

            bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color = new Color32((byte)(bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color.r * 255),
    (byte)(bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color.g * 255),
    (byte)(bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(3).GetChild(0).GetComponent<Image>().color.b * 255), 140);
            bonusGauge.transform.GetChild(sScript.gaugeNum).transform.GetChild(4).gameObject.SetActive(false);
        }
    }

    IEnumerator malletSlowMole()
    {
        bigMalletTimer = true;
        tempMole = upSc.outSpeed[upSc.moleNum];
        upSc.outSpeed[upSc.moleNum] = 1.5f;
        StartCoroutine(upSc.molePoper());

        yield return new WaitForSeconds(1.5f);

        upSc.outSpeed[upSc.moleNum] = tempMole;
        bigMalletTimer = false;
    }

    IEnumerator hammerWait()
    {
        yield return new WaitForSeconds(0.15f);
        sound.GetComponent<AudioSource>().PlayOneShot(godMalletSound, 0.8f);
        Handheld.Vibrate();
        yield return new WaitForSeconds(0.6f);
    }

    IEnumerator timePauseHam()
    {
        paused = true;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        GameObject.FindGameObjectWithTag("lightning").GetComponent<Animator>().SetBool("light", false);
        yield return new WaitForSecondsRealtime(0.2f);
        hugeHamr.GetComponent<Animator>().SetBool("hamDown", false);
        paused = false;
        Time.timeScale = 1;
    }

    void checkRank()
    {
        for (int a = rankPoints.Length - 1; a >= 0; a--)
        {
            if (sScript.score >= rankPoints[a])
            {
                rankTextScreen.GetComponent<Text>().text = ranksText[a].ToUpper();
                a = -1;

                if (sScript.oldText != rankTextScreen.GetComponent<Text>().text)
                {
                    sScript.oldText = rankTextScreen.GetComponent<Text>().text;
                    //rankTextScreen.GetComponent<Animation>().Play();
                    sScript.rankIndex--;
                }
            }
        }
    }

    void closeObjs()
    {
        uiCanvas.SetActive(false);
    }

    void heartOn()
    {
        hearts.transform.GetChild(0).gameObject.SetActive(true);
        hearts.transform.GetChild(1).gameObject.SetActive(true);
        hearts.transform.GetChild(2).gameObject.SetActive(true);

        heartsBroken.transform.GetChild(0).gameObject.SetActive(false);
        heartsBroken.transform.GetChild(1).gameObject.SetActive(false);
        heartsBroken.transform.GetChild(2).gameObject.SetActive(false);
    }
}
