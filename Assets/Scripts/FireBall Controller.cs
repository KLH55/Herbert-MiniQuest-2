using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    public float force = 8f;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        print("fuck");
        DragonController dragon = GameObject.FindAnyObjectByType<DragonController>();
        direction = dragon.GetDirection();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // rb.AddForce(Vector2.right * force);
        rb.velocity = force * direction * -1;
        Invoke("Die", 2f); // wait 4 seconds befre destroying bullet
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
