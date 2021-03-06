using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource wallsound;
    // Start is called before the first frame update
    void Start()
    {
        wallsound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();   // 壁のの色を変える
            wallsound.Play();
        }
    }
}
