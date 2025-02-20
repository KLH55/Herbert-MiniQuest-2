using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragonController : MonoBehaviour
{

    public int direction = 1; // facing right
    public GameObject fireBall; // for bullets
    public Transform dragonFirePoint;
    private SpriteRenderer rend;
    private float fireRate = .3f;
    private float nextFire = 0f;
    private bool facingRight = true;
    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        Debug.DrawRay(transform.position, new Vector2(0,-2), Color.red, 0.5f);
        Debug.Log(hit);
       
        if (hit.collider == null) // no hit wth raycasting
        {
            direction = direction * -1;
            rend.flipX = !rend.flipX;
        }

        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 1 * direction, transform.position.y), Time.deltaTime);

        RaycastHit2D headhit = Physics2D.Raycast(transform.position, Vector2.up, 1f);
        if (headhit.collider != null)
        {
            if (headhit.collider.gameObject.CompareTag("player"))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");
        if (collision.gameObject.CompareTag("fire"))
        {
            Destroy(gameObject);
        }
    }

    public void OnFire(InputValue movementValue)
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;
            animator.SetTrigger("isShooting");
            Instantiate(fireBall, dragonFirePoint.position, facingRight ? dragonFirePoint.rotation : Quaternion.Euler(0, 180, 0));
            audioSource.Play();
        }
    }
}
