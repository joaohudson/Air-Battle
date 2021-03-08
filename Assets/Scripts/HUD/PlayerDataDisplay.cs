using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataDisplay : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text recordText;

    private PlayerController player;

    private void Awake()
    {
        player = PlayerController.Instance;
        player.OnChangePlayerData += OnChangePlayerData;
    }

    private void OnChangePlayerData()
    {
        var playerData = player.PlayerData;
        scoreText.text = $"Scores: {playerData.scores}";
        recordText.text = $"Record: {playerData.record}";
    }
}
