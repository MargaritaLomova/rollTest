using UnityEngine;

public class PartBehaviour : MonoBehaviour
{
    [Header("Variables"), SerializeField]
    private float speed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector3 oppositeOfCollisionDirection = collision.contacts[0].point - transform.position;
            oppositeOfCollisionDirection = -oppositeOfCollisionDirection.normalized;

            rb.AddForce(oppositeOfCollisionDirection * speed);
        }
    }
}