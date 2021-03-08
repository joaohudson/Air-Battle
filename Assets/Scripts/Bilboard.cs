using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toCam = transform.position - mainCamera.transform.position;
        toCam.Normalize();

        transform.rotation = Quaternion.LookRotation(toCam);
    }
}
