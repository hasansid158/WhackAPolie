﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleBg : MonoBehaviour
{

    // Use this for initialization
    void Awake() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize * 2.01f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        transform.localScale = new Vector3( xWidth.x + 0.12f, xWidth.y , xWidth.z);
        //transform.localScale.x = worldScreenWidth / width;
        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight;
        //transform.localScale.y = worldScreenHeight / height;
    }

}
