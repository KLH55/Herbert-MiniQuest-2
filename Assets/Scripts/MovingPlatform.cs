using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform position1; // Where the platfrom starts.
    public Transform position2; // Where the platfrom stops.
    public float speed = 2f; // How fast the platform moves.

    private void Start()
    {
        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        // Coroputine to move the platfrom to the first position, wait
        // , then move the platform to the the second position, then wait againg
        // before repeating.
        while (true)
        {
            yield return StartCoroutine(MoveToPoint(position1.position));
            yield return new WaitForSeconds(.5f);
            yield return StartCoroutine(MoveToPoint(position2.position));
            yield return new WaitForSeconds(.5f);
        }
    }

    private IEnumerator MoveToPoint(Vector2 goTo)
    {
        // Coroutine that makes the platform move to each position.
        while (Vector2.Distance(transform.position, goTo) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(transform.position, goTo, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = goTo;
    }
}