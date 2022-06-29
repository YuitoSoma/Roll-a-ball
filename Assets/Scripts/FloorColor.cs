using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColor : MonoBehaviour
{
    private float timeReset = 3;
    private float time = 0;
    private bool flag;

    void Start()
    {
        StartCoroutine("FloorColorChange");
        time = 0;
        flag = true;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (!flag)
        {
            if (time > timeReset)
            {
                StartCoroutine("FloorColorChange");
                time = 0;
                flag = true;
            }
        }

        if (flag)
        {
            if (time > timeReset)
            {
                StartCoroutine("FloorColorChange");
                time = 0;
                flag = false;
            }
        }
    }
        // Update is called once per frame
    void FloorColorChange()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();   // è∞ÇÃêFÇïœÇ¶ÇÈ
    }
}
