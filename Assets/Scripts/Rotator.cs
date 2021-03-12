using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 axi = Vector3.up;
    [SerializeField]
    private float speed = 40f;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(Time.time * speed, axi);
    }
}
