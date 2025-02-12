using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float force = 8f;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        direction = player.GetComponent<PlyerController>().GetDirection();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // rb.AddForce(Vector2.right * force);
        rb.velocity = force * direction;
        Invoke("Die", 4f); // wait 4 seconds befre destroying bullet
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
            Die();
    }
}
