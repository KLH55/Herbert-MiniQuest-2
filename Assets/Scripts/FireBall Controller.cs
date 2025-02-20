using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    public float force = 8f;

    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        GameObject dragon = GameObject.FindGameObjectWithTag("enemy");
        direction = dragon.GetComponent<DragonController>().direction * Vector2.left;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // rb.AddForce(Vector2.right * force);
        rb.velocity = force * direction;
        Invoke("Die", 4f); // wait 4 seconds befre destroying bullet
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
