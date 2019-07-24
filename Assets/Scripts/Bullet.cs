using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 8f;

    void Start()
    {
        Invoke("DestroyAfterX", lifetime);
    }

    void DestroyAfterX()
    {
        Destroy(gameObject);
    }
}
