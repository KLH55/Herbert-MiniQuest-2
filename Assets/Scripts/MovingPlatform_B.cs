using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform_B : MonoBehaviour
{
    public Transform position3;
    public Transform position4;
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MovingThePlatform());
    }

    private IEnumerator MovingThePlatform()
    {
        Vector2 goTo = position4.position;
        Vector2 startAt = position3.position;

        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, goTo, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, goTo) < 0.01f)
                goTo = (goTo == startAt) ? position4.position : position3.position;

            yield return null;
        }
    }
}
