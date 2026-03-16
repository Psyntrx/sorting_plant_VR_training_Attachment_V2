using UnityEngine;
using System.Collections.Generic;

public class ConveyerBeltScript : MonoBehaviour
{
    public float speed = 2f;
    public Vector3 direction = Vector3.forward;

    public List<GameObject> OnBelt = new List<GameObject>();

    void FixedUpdate()
    {
        for (int i = 0; i < OnBelt.Count; i++)
        {
            Rigidbody rb = OnBelt[i].GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = direction.normalized * speed;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!OnBelt.Contains(collision.gameObject))
        {
            OnBelt.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        OnBelt.Remove(collision.gameObject);
    }
}
