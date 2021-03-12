using UnityEngine;

[System.Serializable]
public struct Drop
{
    [Range(0f, 1f)]
    public float dropChance;
    public GameObject item;
}

[CreateAssetMenu(menuName = "SO/Enemie")]
public class CharacterInfo : ScriptableObject
{
    [Header("General Data")]
    public int health = 100;
    public float speed = 10f;
    [Header("AI")]
    public float visionRange = 40f;
    public float attackRange = 10f;
    [Header("Enemie")]
    public int scores;
    public Drop[] drops;
}
