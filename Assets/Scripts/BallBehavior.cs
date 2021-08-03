using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [Header("Components"), SerializeField]
    private Rigidbody rb;

    [Header("Variables"), SerializeField]
    private float ballSpeed = 1f;

    private Vector3 startPos;

#if UNITY_ANDROID || UNITY_IOS

    private void FixedUpdate()
    {
        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    rb.constraints = RigidbodyConstraints.None;
                    var offset =  new Vector3(touch.deltaPosition.x, transform.position.y, touch.deltaPosition.y);
                    rb.AddForce(offset * (ballSpeed / 900));
                    break;
                case TouchPhase.Ended:
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    break;
            }
        }
}

#endif

#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX

    private void OnMouseDown()
    {
        startPos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        rb.constraints = RigidbodyConstraints.None;
        var offset = new Vector3(Input.mousePosition.x - startPos.x, transform.position.y, Input.mousePosition.y - startPos.y);
        rb.AddForce(offset * (ballSpeed / 900));
    }

    private void OnMouseUp()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

#endif
}