using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform_A : MonoBehaviour
{
    public Transform position1;
    public Transform position2;
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MovingThePlatform());
    }

    private IEnumerator MovingThePlatform()
    {
        Vector2 goTo = position2.position;
        Vector2 startAt = position1.position;
        
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, goTo, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, goTo) < 0.01f)
                goTo = (goTo == startAt) ? position2.position : position1.position;

            yield return null;
        }
    }
}
