using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterState))]
public class FlyMotor : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem turbine;

    private float hAngle = 0f, vAngle = 0f;
    private CharacterState state;

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<CharacterState>();
    }

    // Update is called once per frame
    void Update()
    {
        float dh = Input.GetAxis("Horizontal") * Time.deltaTime * 50f;
        float dv = Input.GetAxis("Vertical") * Time.deltaTime * 30f;
        float speed = state.Speed;
        bool stoped = true;

        vAngle = Mathf.Clamp(vAngle + dv, -30f, 30f);
        hAngle += dh;

        if (Input.GetKey(KeyCode.M))
        {
            stoped = false;
            PlayTurbine();
            transform.position += Time.deltaTime *  speed * transform.forward;
        }

        if (Input.GetKey(KeyCode.N))
        {
            stoped = false;
            PlayTurbine();
            transform.position += Time.deltaTime * -speed * transform.forward;
        }

        if (stoped)
        {
            StopTurbine();
        }

        transform.rotation = Quaternion.AngleAxis(hAngle, Vector3.up) * Quaternion.AngleAxis(vAngle, Vector3.left);
    }

    private void PlayTurbine()
    {
        if (turbine.isStopped)
        {
            turbine.Play();
        }
    }

    private void StopTurbine()
    {
        if (turbine.isPlaying)
        {
            turbine.Stop();
        }
    }
}
