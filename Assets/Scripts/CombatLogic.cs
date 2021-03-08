using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLogic
{
    /// <summary>
    /// Causa dano a um personagem.
    /// </summary>
    /// <param name="state">O estado do personagem alvo.</param>
    /// <param name="damage">O dano a ser causado.</param>
    /// <param name="effect">O efeito de status causado.</param>
    public static void TakeDamage(CharacterState state, int damage, CharacterStatus effect = CharacterStatus.NONE)
    {
        //Dano dobrado quando vulnerável.
        if (state.Status == CharacterStatus.VUNERABLE)
            damage *= 2;

        state.Health -= damage;

        if(effect != CharacterStatus.NONE)
            state.Status = effect;
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
