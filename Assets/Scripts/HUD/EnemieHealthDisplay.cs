using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemieHealthDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject display;
    [SerializeField]
    private float offset;

    private CharacterState state;
    private Image healthBar;
    private GameObject currentDisplay;
    private Canvas canvas;

    private void Start()
    {
        canvas = CanvasManager.Instance.WorldCanvas;
        currentDisplay = Instantiate(display, transform.position, Quaternion.identity, canvas.transform);
        state = GetComponent<CharacterState>();
        state.OnChangeHealth += OnChangeHealth;
        healthBar = currentDisplay.transform.GetChild(1).GetComponent<Image>();
    }

    private void OnChangeHealth()
    {
        healthBar.fillAmount = state.HealthNormalized;
    }

    private void Update()
    {
        currentDisplay.transform.position = transform.position + offset * Vector3.up;
    }

    private void OnDestroy()
    {
        Destroy(currentDisplay);
    }
}
