using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterState))]
public class HealthPlayerDisplay : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Text healthText;

    private CharacterState state;

    private void Start()
    {
        state = GetComponent<CharacterState>();
        state.OnChangeHealth += OnChangeHealth;
    }

    private void OnChangeHealth()
    {
        float health = state.HealthNormalized;
        healthBar.fillAmount = health;
        healthText.text = $"{health * 100f}%";
    }
}
