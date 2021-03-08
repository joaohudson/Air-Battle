using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    /// <summary>
    /// Gera um vetor aleatório no espaço 3D
    /// normalizado.
    /// </summary>
    public static Vector3 RandomVector3()
    {
        return new Vector3()
        {
            x = Random.Range(-1f, 1f),
            y = Random.Range(-1f, 1f),
            z = Random.Range(-1f, 1f)
        }.normalized;
    }
}
