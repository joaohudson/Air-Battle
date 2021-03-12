using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastShooter : MonoBehaviour
{
    #region Singleton
    public static FastShooter Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private struct Projectile
    {
        public Vector3 position;
        public Vector3 direction;
        public float distance;
        public ProjectileInfo info;
        public string target;
        public CharacterState own;
    }

    [SerializeField]
    private int capacity = 200;

    private Projectile[] projectiles;
    private int count = 0;
    private Camera cameraMain;

    private void Start()
    {
        projectiles = new Projectile[capacity];
        cameraMain = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < count; i++)
        {
            var info = projectiles[i].info;
            float d = info.speed * Time.deltaTime;
            projectiles[i].position += projectiles[i].direction * d;
            projectiles[i].distance += d;

            Vector3 toCamera = cameraMain.transform.position - projectiles[i].position;
            toCamera = -toCamera;
            toCamera.Normalize();
            Matrix4x4 m = Matrix4x4.TRS(projectiles[i].position, Quaternion.LookRotation(toCamera), Vector3.one);
            Graphics.DrawMesh(info.mesh, m, info.material, 0, cameraMain, 0, null, false);

            if(projectiles[i].distance >= info.range)
            {
                Delete(i);
                i--;
            }
        }
    }

    public void Fire(ProjectileInfo projectile, Vector3 position, Vector3 direction, string target, CharacterState own)
    {
        Spawn(projectile, position, direction, target, own);
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < count; i++)
        {
            var cds = Physics.OverlapSphere(projectiles[i].position, projectiles[i].info.size);
            foreach (var cd in cds)
            {
                if (cd.CompareTag(projectiles[i].target))
                {
                    var state = cd.GetComponent<CharacterState>();
                    CombatLogic.TakeDamage(state, projectiles[i].own, projectiles[i].info.damage, projectiles[i].info.effect);
                    Delete(i);
                    i--;
                    break;
                }
            }
        }
    }

    private void Spawn(ProjectileInfo info, Vector3 position, Vector3 direction, string target, CharacterState own)
    {
        if (count == capacity)
            return;

        const float angle = 15f;

        Quaternion rot = Quaternion.identity;

        for (int i = 0; i < info.spreadAmount; i++)
        {
            if (count == capacity)
                break;

            projectiles[count].position = position;
            projectiles[count].direction = rot * direction;
            projectiles[count].distance = 0f;
            projectiles[count].info = info;
            projectiles[count].target = target;
            projectiles[count].own = own;
            count++;

            rot = Quaternion.AngleAxis(Random.Range(-angle, angle), Vector3.up) * 
                  Quaternion.AngleAxis(Random.Range(-angle, angle), Vector3.right) *
                  Quaternion.AngleAxis(Random.Range(-angle, angle), Vector3.forward);
        }
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < count; i++)
        {
            float size = projectiles[i].info.size;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(projectiles[i].position, size);
        }
    }

    private void Delete(int index)
    {
        if (count <= 0)
            return;

        var aux = projectiles[index];
        projectiles[index] = projectiles[count - 1];
        projectiles[count - 1] = aux;
        count--;
    }
}
