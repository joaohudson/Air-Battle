using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSView : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private float wait = 0f;

    // Update is called once per frame
    void Update()
    {
        if(wait > 0f)
        {
            wait -= Time.deltaTime;
            return;
        }

        text.text = $"FPS: {(int)(1f / Time.deltaTime)}";
        wait = 0.25f;

    }
}
