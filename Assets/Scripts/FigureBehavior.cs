using System.Collections.Generic;
using UnityEngine;

public class FigureBehavior : MonoBehaviour
{
    [Header("Components"), SerializeField]
    private List<Rigidbody> parts = new List<Rigidbody>();
    [SerializeField]
    private BoxCollider coll;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Destroy(coll);
            foreach(var part in parts)
            {
                part.isKinematic = false;
            }
        }
    }
}