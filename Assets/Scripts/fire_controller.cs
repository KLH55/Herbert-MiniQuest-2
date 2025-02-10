using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_controller : MonoBehaviour
{
    public float force = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * force);
        Invoke("Die", 4f); // wait 4 seconds befre destroying bullet
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
