using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionObject : MonoBehaviour
{
    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private float effectDuration = 5f;
    [SerializeField]
    private Rigidbody[] chunks;
    [SerializeField]
    private GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var c in chunks)
        {
            c.transform.parent = null;
            c.AddForce(Util.RandomVector3() * force, ForceMode.Impulse);
            Destroy(c.gameObject, effectDuration);
        }

        if(effect != null)
            Instantiate(effect, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}
