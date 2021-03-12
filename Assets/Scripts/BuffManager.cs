using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterState))]
public class BuffManager : MonoBehaviour
{
    [SerializeField]
    private Material buffIndicator;

    private CharacterState state;
    private IEnumerator resetBuff = null;

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<CharacterState>();
        state.OnChangeBuff += OnChangeBuff;
    }

    private void OnChangeBuff()
    {
        if (state.Buff != CharacterBuff.NONE)
        {
            if(resetBuff != null)
            {
                StopCoroutine(resetBuff);
            }

            resetBuff = ResetBuff();
            StartCoroutine(resetBuff);
        }

        if (buffIndicator == null)
            return;

        string keyEmissionColor = "_EmissionColor";

        switch (state.Buff)
        {
            case CharacterBuff.ATTACK:
                buffIndicator.SetColor(keyEmissionColor, Color.red);
                break;

            case CharacterBuff.DEFENSE:
                buffIndicator.SetColor(keyEmissionColor, Color.blue);
                break;

            case CharacterBuff.LIFE_DRAIN:
                buffIndicator.SetColor(keyEmissionColor, Color.magenta);
                break;

            case CharacterBuff.SPEED:
                buffIndicator.SetColor(keyEmissionColor, Color.green);
                break;

            default:
                buffIndicator.SetColor(keyEmissionColor, Color.black);
                break;
        }
    }

    IEnumerator ResetBuff()
    {
        yield return new WaitForSeconds(CharacterBuffValues.BUFF_DURATION);
        state.Buff = CharacterBuff.NONE;
        resetBuff = null;
    }
}
