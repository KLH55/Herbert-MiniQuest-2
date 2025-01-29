using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlyerController : MonoBehaviour
{
    // private
    private Vector2 movementVector;
    private Animator animator; // animation
    private SpriteRenderer sprite_r; // sprite flip

    // public
    public float speed = 3;
    public float height = 500;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // animation
        sprite_r = GetComponent<SpriteRenderer>(); // sprite flip
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(movementVector.x)); // animation change

        if (movementVector.x > 0)
            transform.Translate(Vector2.right * speed * Time.deltaTime); // vector is (1,0)
        else if(movementVector.x < 0)
            transform.Translate(Vector2.left * speed * Time.deltaTime); // vector is (-1,0)

        if (movementVector.x < 0) // walking left
            sprite_r.flipX = true;
        else if (movementVector.x > 0) // walking right
            sprite_r.flipX = false;
    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector.x);
    }

    public void OnJump(InputValue movementValue)
    {
        transform.Translate(Vector2.up * height * Time.deltaTime);
        Debug.Log("Jumping !!!:) ;)");
    }
}
