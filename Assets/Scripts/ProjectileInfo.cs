using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectile", menuName = "SO/Projectile")]
public class ProjectileInfo : ScriptableObject
{
    [Header("Graphics")]
    public Mesh mesh;
    public Material material;
    public bool bilboard = false;
    [Header("Logic")]
    public float range = 30f;
    public int spreadAmount = 1;
    public float fireRate = 1f;
    public float speed = 20f;
    public float size = 1f;
    public int damage = 10;
    public CharacterStatus effect = CharacterStatus.NONE;
}
