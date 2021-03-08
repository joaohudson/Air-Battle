using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Spawner
{
    public Transform transform;
    public int enemieCount;
    public float area;
}

public class EnemieManager : MonoBehaviour
{
    #region Singleton
    public static EnemieManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField]
    private GameObject enemiePrefab;
    [SerializeField]
    private Spawner[] spawners;

    private int enemiesCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        Vector3 position;

        foreach (var spawner in spawners)
        {
            position = spawner.transform.position;

            enemiesCount += spawner.enemieCount;

            for (int i = 0; i < spawner.enemieCount; i++)
                Instantiate(enemiePrefab, position + Util.RandomVector3() * spawner.area, Quaternion.identity);
        }
    }

    /// <summary>
    /// Registra a morte de um inimigo
    /// gerado.
    /// </summary>
    public void DeathResgiter()
    {
        enemiesCount--;

        if(enemiesCount == 0)
        {
            Spawn();
        }
    }
}
