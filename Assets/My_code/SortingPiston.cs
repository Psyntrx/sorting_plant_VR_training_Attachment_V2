using UnityEngine;
using System.Collections;

public class SortingPistonCollision : MonoBehaviour
{
    public string targetTag = "Cube";
    public Transform piston;

    public float pushDistance = 0.5f;
    public float pushSpeed = 3f;
    public float delay = 0.5f;

    public Vector3 pushDirection = Vector3.right; // Direction of piston movement

    private Vector3 startPos;
    private bool activated = false;

    void Start()
    {
        startPos = piston.localPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag) && !activated)
        {
            Debug.Log("Collision detected with " + collision.gameObject.name);
            activated = true;
            StartCoroutine(PushPiston());
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log("Collision exited with " + collision.gameObject.name);
            activated = false;
        }
    }

    IEnumerator PushPiston()
    {
        yield return new WaitForSeconds(delay);

        Vector3 pushPos = startPos + pushDirection.normalized * pushDistance;

        while (Vector3.Distance(piston.localPosition, pushPos) > 0.01f)
        {
            piston.localPosition = Vector3.MoveTowards(
                piston.localPosition,
                pushPos,
                pushSpeed * Time.deltaTime
            );

            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        while (Vector3.Distance(piston.localPosition, startPos) > 0.01f)
        {
            piston.localPosition = Vector3.MoveTowards(
                piston.localPosition,
                startPos,
                pushSpeed * Time.deltaTime
            );

            yield return null;
        }
    }
}