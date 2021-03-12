using UnityEngine;

[RequireComponent(typeof(CharacterState))]
public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private ProjectileInfo[] projectiles;
    [SerializeField]
    private string target;

    private CharacterState state;
    private int currentProjectile = 0;
    private float fireInterval = 0f;

    private void Start()
    {
        state = GetComponent<CharacterState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fireInterval > 0f)
        {
            fireInterval -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        //Não pode atirar se estiver congelado
        if (state.Status == CharacterStatus.ICED)
            return;

        if (fireInterval > 0f)
            return;

        FastShooter.Instance.Fire(projectiles[currentProjectile], transform.position + transform.forward * 2f, transform.forward, target, state);
        fireInterval = 1f / projectiles[currentProjectile].fireRate;
    }

    public void ChangeProjectile()
    {
        currentProjectile = (currentProjectile + 1) % projectiles.Length;
    }
}
