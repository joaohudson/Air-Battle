using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterState))]
public class GroundDeath : MonoBehaviour
{
    private CharacterState state;

    private void Start()
    {
        state = GetComponent<CharacterState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            state.Health = 0;//kill
        }
    }
}
