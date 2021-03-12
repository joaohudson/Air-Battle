using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStatus))]
public class StatusManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem iceParticles;
    [SerializeField]
    private ParticleSystem vulnerableParticles;
    [SerializeField]
    private ParticleSystem slowedParticles;
    [SerializeField]
    private ParticleSystem fireParticles;

    private CharacterState state;
    private CharacterStatus previousStatus = default;
    private IEnumerator statusTimer, continuosDamage;
    
    private void Start()
    {
        state = GetComponent<CharacterState>();
        state.OnChangeStatus += OnChangeStatus;
    }

    private void OnChangeStatus()
    {
        if(state.Status != CharacterStatus.NONE)
        {
            if (statusTimer != null)
                StopCoroutine(statusTimer);

            statusTimer = StatusTimer();
            StartCoroutine(statusTimer);
        }

        if(state.Status == CharacterStatus.BURNING)
        {
            if (continuosDamage != null)
                StopCoroutine(continuosDamage);

            continuosDamage = ContinuosDamage(CharacterStatusValues.FIRE_DAMAGE);
            StartCoroutine(continuosDamage);
        }


        switch (previousStatus)
        {
            case CharacterStatus.ICED:
                iceParticles.Stop();
                break;

            case CharacterStatus.SLOWED:
                slowedParticles.Stop();
                break;

            case CharacterStatus.VUNERABLE:
                vulnerableParticles.Stop();
                break;

            case CharacterStatus.BURNING:
                fireParticles.Stop();
                break;
        }

        switch (state.Status)
        {
            case CharacterStatus.ICED:
                iceParticles.Play();
                break;

            case CharacterStatus.SLOWED:
                slowedParticles.Play();
                break;

            case CharacterStatus.VUNERABLE:
                vulnerableParticles.Play();
                break;

            case CharacterStatus.BURNING:
                fireParticles.Play();
                break;
        }

        previousStatus = state.Status;
    }

    private IEnumerator ContinuosDamage(int damage)
    {
        var delay = new WaitForSeconds(CharacterStatusValues.DAMAGE_PERIOD);
        int iterations = (int)(CharacterStatusValues.STATUS_DURATION / CharacterStatusValues.DAMAGE_PERIOD);

        for(int i = 0; i < iterations; i++)
        {
            yield return delay;
            CombatLogic.TakeDamage(state, null, damage);
        }

        continuosDamage = null;
    }

    private IEnumerator StatusTimer()
    {
        yield return new WaitForSeconds(CharacterStatusValues.STATUS_DURATION);
        state.Status = CharacterStatus.NONE;
        statusTimer = null;
    }
}
