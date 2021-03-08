using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    #region Singleton
    public static CanvasManager Instance{ get; private set;}
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Fields
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Canvas worldCanvas;
    #endregion

    /// <summary>
    /// Canvas da HUD do jogo.
    /// </summary>
    public Canvas Canvas { get => canvas; }

    /// <summary>
    /// Canvas para UIs dinâmicas no espaço
    /// do mundo.
    /// </summary>
    public Canvas WorldCanvas { get => worldCanvas; }
}
