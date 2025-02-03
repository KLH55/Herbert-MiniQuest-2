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

    // public
    public float speed = 3;
    public float height = 500;
    public float maxSpeed = 7f;
    public float gravMult = 3f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // animation
        sprite_r = GetComponent<SpriteRenderer>(); // sprite flip
        body = GetComponent<Rigidbody2D>();
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

            if (movementVector.x < 0) // walking left
                sprite_r.flipX = true;
            else if (movementVector.x > 0) // walking right
                sprite_r.flipX = false;

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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Touching Ground");
        }
    }

    IEnumerator LerpJump()
    {
        float desied = transform.position.y + 3;

        while (transform.position.y < desied)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + .5f);
            yield return new WaitForSeconds(.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
