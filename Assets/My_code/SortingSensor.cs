using UnityEngine;
using System.Collections;

public class SortingSensor : MonoBehaviour
{
    public string targetTag = "Cube";   // Tag to detect

    public Transform piston;            // Drag piston here in inspector

    public float pushDistance = 0.5f;
    public float pushSpeed = 2f;
    public float delayBeforePush = 0.3f;
    public float retractDelay = 0.2f;

    public Vector3 pushDirection = Vector3.right;

    private Vector3 pistonStartPos;
    private bool pistonBusy = false;

    void Start()
    {
        pistonStartPos = piston.localPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if (pistonBusy) return;

        Debug.Log("Object detected: " + other.gameObject.name);

        if (other.CompareTag(targetTag))
        {
            Debug.Log("Correct tag detected: " + other.tag);
            StartCoroutine(PushPiston());
        }
    }

    IEnumerator PushPiston()
    {
        pistonBusy = true;

        yield return new WaitForSeconds(delayBeforePush);

        Vector3 pushTarget = pistonStartPos + pushDirection.normalized * pushDistance;

        while (Vector3.Distance(piston.localPosition, pushTarget) > 0.001f)
        {
            piston.localPosition = Vector3.MoveTowards(
                piston.localPosition,
                pushTarget,
                pushSpeed * Time.deltaTime
            );

            yield return null;
        }

        yield return new WaitForSeconds(retractDelay);

        while (Vector3.Distance(piston.localPosition, pistonStartPos) > 0.001f)
        {
            piston.localPosition = Vector3.MoveTowards(
                piston.localPosition,
                pistonStartPos,
                pushSpeed * Time.deltaTime
            );

            yield return null;
        }

        pistonBusy = false;
    }
}