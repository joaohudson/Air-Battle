using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CharacterStatus
{
    NONE, ICED, SLOWED, VUNERABLE, BURNING
}

public enum CharacterBuff
{
    NONE, SPEED, DEFENSE, LIFE_DRAIN, ATTACK
}

public class CharacterBuffValues
{
    /// <summary>
    /// Duração de cada buff.
    /// </summary>
    public const float BUFF_DURATION = 20f;

    /// <summary>
    /// Fator de aumento da velocidade
    /// do buff de velocidade(SPEED).
    /// </summary>
    public const float SPEED_FACTOR = 2f;

    /// <summary>
    /// Fator de redução de dano do
    /// buff de defesa(DEFENSE).
    /// </summary>
    public const float DEFENSE_FACTOR = 0.5f;

    /// <summary>
    /// Fator de aumento de ataque
    /// do buff de ataque(ATTACK).
    /// </summary>
    public const float ATTACK_FACTOR = 1.5f;

    /// <summary>
    /// Fator de absorção de vida do
    /// buff de absorção de vida(LIFE_DRAIN).
    /// </summary>
    public const float LIFE_DRAIN_FACTOR = 0.6f;
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
    /// O multiplicador de dano para quando o
    /// personagem está vulnerável.
    /// </summary>
    public const float VULNERABLE_DAMAGE_FACTOR = 2.5f;

    /// <summary>
    /// Dano causado pelo status de fogo a cada DAMAGE_PERIOD.
    /// </summary>
    public const int FIRE_DAMAGE = 20;
}

public class CharacterState : MonoBehaviour
{

    #region Fields
    private CharacterStatus status;
    private CharacterBuff buff;
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
    /// <summary>
    /// Chamado quando o buff desse personagem é
    /// alterado.
    /// </summary>
    public event Action OnChangeBuff;
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
    /// Buff desse personagem. efeito que aumenta as
    /// características do personagem.
    /// </summary>
    public CharacterBuff Buff {
        get => buff;
        set {
            buff = value;
            OnChangeBuff?.Invoke();
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
            float statusFactor;
            float buffFactor;

            buffFactor = Buff == CharacterBuff.SPEED ? CharacterBuffValues.SPEED_FACTOR : 1f;

            switch (Status)
            {
                case CharacterStatus.SLOWED:
                    statusFactor = info.speed * 0.5f;
                    break;
                case CharacterStatus.ICED:
                    statusFactor = 0f;
                    break;
                default:
                    statusFactor = info.speed;
                    break;
            }

            return buffFactor * statusFactor;
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
