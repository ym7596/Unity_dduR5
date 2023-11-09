using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missiles : MonoBehaviour
{
    public float speed;
    public int damage;

    public void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
