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
    private float fireRate = .8f;
    private float nextFire = .5f;
    private bool facingRight = true;
    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        Debug.DrawRay(transform.position, new Vector2(0,-2), Color.red, 0.5f);
        Debug.Log(hit);

        RaycastHit2D playerHit = Physics2D.Raycast(transform.position, Vector2.left, 6f);
        Debug.DrawRay(transform.position, new Vector2(-6, 0), Color.red, 0.5f);
        Debug.Log(playerHit);

        if (hit.collider == null) // no hit wth raycasting
        {
            direction = direction * -1;
            rend.flipX = !rend.flipX;
        }

        if (playerHit.collider != null && playerHit.collider.CompareTag("player"))
        {
            OnFireBall();
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

    public void OnFireBall()
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;
            animator.SetTrigger("isShooting");
            Instantiate(fireBall, dragonFirePoint.position, facingRight ? dragonFirePoint.rotation : Quaternion.Euler(0, 180, 0));
            audioSource.Play();
        }
    }

    public Vector2 GetDirection()
    {
        if (facingRight)
            return Vector2.right;
        else
            return Vector2.left;
    }
}
