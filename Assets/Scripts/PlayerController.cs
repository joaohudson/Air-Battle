using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerData
{
    public int scores;
    public int record;
}

[RequireComponent(typeof(CharacterState))]
[RequireComponent(typeof(WeaponController))]
public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField]
    private GameObject destroyedVersion;
    [SerializeField]
    private GameObject graphics;
    [SerializeField]
    private new Collider collider;

    private WeaponController weapon;
    private Camera cameraMain;
    private PlayerData playerData;

    /// <summary>
    /// Chamado quando os dados do joagdor mudar.
    /// </summary>
    public event Action OnChangePlayerData;

    /// <summary>
    /// O estado do player.
    /// </summary>
    public CharacterState State { get; set; }

    /// <summary>
    /// Obtém os dados do jogador.
    /// </summary>
    public PlayerData PlayerData { get => playerData; }

    private void Start()
    {
        State = GetComponent<CharacterState>();
        weapon = GetComponent<WeaponController>();

        SaveDataController.Load(ref playerData);
        OnChangePlayerData?.Invoke();

        cameraMain = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(State.Health == 0)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            graphics.SetActive(false);
            collider.enabled = false;
            this.enabled = false;
            SaveDataController.Save(PlayerData);
            Menu.Instance.ShowPausePanel();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            weapon.Fire();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            weapon.ChangeProjectile();
        }
    }

    private void LateUpdate()
    {
        cameraMain.transform.position = transform.position;
        cameraMain.transform.forward = transform.forward;
        cameraMain.transform.position -= transform.forward * 10f;

        cameraMain.transform.Translate(0f, 10f, 0f);
        cameraMain.transform.Rotate(30f, 0f, 0f);
    }

    /// <summary>
    /// Adiciona pontos ao jogador.
    /// </summary>
    /// <param name="score">Pontos a serem adicionados.</param>
    public void AddScore(int scores)
    {
        playerData.scores += scores;
        OnChangePlayerData?.Invoke();

    }
}
