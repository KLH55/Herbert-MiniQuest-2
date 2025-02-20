using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlyerController : MonoBehaviour
{
    // private
    private Vector2 movementVector;
    private Animator animator; // animation
    private SpriteRenderer sprite_r; // sprite flip
    private Rigidbody2D body;
    private bool isGrounded = false;
    private bool jump = false;
    private float fireRate = .3f;
    private float nextFire = 0f;
    private bool facingRight = true;
    private AudioSource audioSource;

    // public
    public float speed = 3;
    public float height = 500;
    public float maxSpeed = 7f;
    public float gravMult = 3f;
    public GameObject fire; // for bullets
    public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // animation
        sprite_r = GetComponent<SpriteRenderer>(); // sprite flip
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(movementVector.x)); // animation change

        if (movementVector.x > 0 && body.velocity.x < maxSpeed)
            // transform.Translate(Vector2.right * speed * Time.deltaTime); // vector is (1,0)
            body.AddForce(Vector2.right * speed);
        else if (movementVector.x < 0 && Mathf.Abs(body.velocity.x) < maxSpeed)
            // transform.Translate(Vector2.left * speed * Time.deltaTime); // vector is (-1,0)
            body.AddForce(Vector2.left * speed);

        if (movementVector.x < 0 && facingRight) // walking left
        {
            // sprite_r.flipX = true;
            Flip();
            facingRight = false;
        }
        else if (movementVector.x > 0 && !facingRight) // walking right
        {
            // sprite_r.flipX = false;
            Flip();
            facingRight = true;
        }

        if (jump)
        {
            // transform.Translate(Vector2.up * height * Time.deltaTime);
            // StartCoroutine("LerpJump");
            body.AddForce(Vector2.up * height);
            jump = false;
            isGrounded = false;
        }

        if (body.velocity.y < 0) // player is falling
        {
            body.gravityScale = gravMult;
        }
        else
        {
            body.gravityScale = 1;
        }
    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector.x);
    }

    public void OnJump(InputValue movementValue)
    {
        if (isGrounded)
            // StartCoroutine("LerpJump");
            jump = true;
        // transform.Translate(Vector2.up * height * Time.deltaTime);
        // Debug.Log("Jumping !!!:) ;)");
    }

    public void OnFire(InputValue movementValue)
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;
            animator.SetTrigger("isShooting");
            Instantiate(fire, firePoint.position, facingRight ? firePoint.rotation : Quaternion.Euler(0, 180, 0));
            audioSource.Play();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Touching Ground");
        }

        if (collision.gameObject.CompareTag("enemy"))
        {
            GameManager.instance.DecreaseLives();
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator LerpJump()
    {
        float desired = transform.position.y + 3;

        while (transform.position.y < desired)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + .5f);
            yield return new WaitForSeconds(.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))
        {
            GameManager.instance.DecreaseLives();
            SceneManager.LoadScene(0);
        }
    }
    public Vector2 GetDirection()
    {
        if (facingRight)
            return Vector2.right;
        else
            return Vector2.left;
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * -1;
        transform.localScale = theScale;

    }
}
