using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CharacterStatus
{
    NONE, ICED, SLOWED, VUNERABLE, BURNING
}

public class CharacterStatusValues
{
    /// <summary>
    /// Duração do status em segundos.
    /// </summary>
    public const float STATUS_DURATION = 5f;

    /// <summary>
    /// Período em que é causado o dano contínuo
    /// de um status.
    /// </summary>
    public const float DAMAGE_PERIOD = 0.25f;

    /// <summary>
    /// Dano causado pelo status de fogo a cada DAMAGE_PERIOD.
    /// </summary>
    public const int FIRE_DAMAGE = 20;
}

public class CharacterState : MonoBehaviour
{

    #region Fields
    private CharacterStatus status;
    private int health;
    #endregion

    #region Events
    /// <summary>
    /// Chamado quando a vida é alterada.
    /// </summary>
    public event Action OnChangeHealth;
    /// <summary>
    /// Chamado quando o status é alterado.
    /// </summary>
    public event Action OnChangeStatus;
    #endregion

    /// <summary>
    /// Status do personagem.
    /// </summary>
    public CharacterStatus Status {
        get => status; 
        set{
            status = value;
            OnChangeStatus?.Invoke();
        }
    }

    /// <summary>
    /// Vida corrente do personagem.
    /// </summary>
    public int Health { 
        get => health;
        set {
            health = value >= 0 ? value : 0;
            OnChangeHealth?.Invoke();
        } 
    }

    /// <summary>
    /// Vida corrente do personagem normalizada.
    /// </summary>
    public float HealthNormalized
    {
        get
        {
            float currentHealth = Health;
            float maxHealth = info.health;

            return currentHealth / maxHealth;
        }
    }

    /// <summary>
    /// Velocidade corrente do personagem.
    /// </summary>
    public float Speed { 
        get 
        {
            switch (Status)
            {
                case CharacterStatus.SLOWED:
                    return info.speed * 0.5f;
                case CharacterStatus.ICED:
                    return 0f;
                default:
                    return info.speed;
            }
        }
    }

    /// <summary>
    /// Informações descritivas do personagem[ReadOnly].
    /// </summary>    
    public CharacterInfo info;

    private void Awake()
    {
        Health = info.health;
        Status = CharacterStatus.NONE;
    }
}
