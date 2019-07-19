using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreSaver : MonoBehaviour
{
    [HideInInspector]
    public int score, gaugeNum, rankIndex,gaugeScore,stageCompNum,stageCheckNum;
    [HideInInspector]
    public bool firstSegment = true;
    [HideInInspector]
    public string oldText;


    void Awake()
    {
        DontDestroyOnLoad(this);
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        stageCheckNum = 5;
        stageCompNum = 1;
        rankIndex = 14;
    }
}
