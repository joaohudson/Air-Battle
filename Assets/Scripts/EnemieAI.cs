using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterState))]
[RequireComponent(typeof(WeaponController))]
public class EnemieAI : MonoBehaviour
{
    private enum Behaviour
    {
        IDLE, RUNNING, ATTACK
    }

    [SerializeField]
    private GameObject destroyedVersionPrefab;

    private CharacterState player;
    private Behaviour behaviour;
    private CharacterState state;
    private WeaponController weapon;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Instance.gameObject.GetComponent<CharacterState>();
        behaviour = Behaviour.IDLE;
        state = GetComponent<CharacterState>();
        weapon = GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Morte
        if(state.Health == 0)
        {
            Die();
        }
        else if(player.Health == 0)
        {
            behaviour = Behaviour.IDLE;
        }

        switch (behaviour)
        {
            case Behaviour.IDLE:
                UpdateIdle();
                break;

            case Behaviour.RUNNING:
                UpdateRunning();
                break;

            case Behaviour.ATTACK:
                UpdateAttack();
                break;
        }
    }

    private void UpdateRunning()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        //Se está na área do ataque, muda para o comportamento atacando.
        if (distanceToPlayer <= state.info.attackRange)
        {
            behaviour = Behaviour.ATTACK;
            return;
        }

        //Se está fora da área de visão, muda para comportamento inativo.
        if(distanceToPlayer >= state.info.visionRange)
        {
            behaviour = Behaviour.IDLE;
            return;
        }

        Vector3 toPlayer = player.transform.position - transform.position;
        toPlayer.Normalize();

        transform.rotation = Quaternion.LookRotation(toPlayer);
        transform.position += state.Speed * Time.deltaTime * transform.forward;
    }

    private void UpdateAttack()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        Quaternion toPlayer = Quaternion.LookRotation((player.transform.position - transform.position).normalized);

        if(distanceToPlayer >= state.info.attackRange)
        {
            behaviour = Behaviour.RUNNING;
            return;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, toPlayer, Time.deltaTime);

        weapon.Fire();
    }

    private void UpdateIdle()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if(distanceToPlayer <= state.info.visionRange && player.Health > 0)
        {
            behaviour = Behaviour.RUNNING;
        }
    }

    private void Die()
    {
        var drops = state.info.drops;
        Instantiate(destroyedVersionPrefab, transform.position, transform.rotation);
        
        foreach(var drop in drops)
        {
            if (Util.Chance(drop.dropChance))
            {
                Instantiate(drop.item, transform.position, Quaternion.identity);
                break;
            }

        }

        PlayerController.Instance.AddScore(state.info.scores);
        EnemieManager.Instance.DeathResgiter();

        enabled = false;
        Destroy(gameObject);
    }
}
