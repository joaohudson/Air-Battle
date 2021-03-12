using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{

    [SerializeField]
    private float convergeSpeed = 20f;
    [SerializeField]
    private float collectRange = 10f;
    [SerializeField]
    private ItemInfo item;

    private CharacterState playerState;

    private void Start()
    {
        playerState = PlayerController.Instance.State;
    }

    private void Update()
    {
        if (!ValidateCollect(item.itemType))
            return;

        if (Vector3.Distance(PlayerController.Instance.transform.position, transform.position) > collectRange)
            return;

        Vector3 dir = PlayerController.Instance.transform.position - transform.position;
        dir.Normalize();

        transform.position += convergeSpeed * Time.deltaTime * dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!ValidateCollect(item.itemType))
            return;

        if (other.CompareTag("Player"))
        {
            if(item.itemType == ItemType.BUFF)
            {
                playerState.Buff = item.buff;
            }
            else//Heal
            {
                CombatLogic.Heal(playerState, item.healAmount);
            }

            Destroy(gameObject);
        }
    }

    private bool ValidateCollect(ItemType type)
    {
        if(type == ItemType.BUFF)
        {
            return playerState.Buff == CharacterBuff.NONE;
        }
        else
        {
            return playerState.Health < playerState.info.health;
        }
    }
}
