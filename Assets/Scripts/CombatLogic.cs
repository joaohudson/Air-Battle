using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLogic
{
    /// <summary>
    /// Causa dano a um personagem.
    /// </summary>
    /// <param name="state">O estado do personagem alvo.</param>
    /// <param name="ownState">O estado do provedor deste ataque.</param>
    /// <param name="damage">O dano a ser causado.</param>
    /// <param name="effect">O efeito de status causado.</param>
    public static void TakeDamage(CharacterState state, CharacterState ownState, int damage, CharacterStatus effect = CharacterStatus.NONE)
    {
        //Dano dobrado quando vulnerável.
        if (state.Status == CharacterStatus.VUNERABLE)
            damage = (int)(CharacterStatusValues.VULNERABLE_DAMAGE_FACTOR * damage);

        //Dano reduzido quando com buff de defesa.
        if (state.Buff == CharacterBuff.DEFENSE)
            damage = (int)(damage * CharacterBuffValues.DEFENSE_FACTOR);

        state.Health -= damage;

        if(effect != CharacterStatus.NONE)
            state.Status = effect;

        if(ownState != null && ownState.Buff == CharacterBuff.LIFE_DRAIN)
        {
            //Cura uma porcentagem do dano causado.
            CombatLogic.Heal(ownState, (int)(CharacterBuffValues.LIFE_DRAIN_FACTOR * damage));
        }
    }

    /// <summary>
    /// Cura o personagem.
    /// </summary>
    /// <param name="state">O estado do personagem alvo.</param>
    /// <param name="amount">A quantidade de vida que ele vai receber.</param>
    public static void Heal(CharacterState state, int amount)
    {
        state.Health += amount;

        if(state.Health > state.info.health)
        {
            state.Health = state.info.health;
        }
    }
}
