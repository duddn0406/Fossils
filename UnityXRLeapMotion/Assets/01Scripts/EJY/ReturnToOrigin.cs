using UnityEngine;

public class ReturnToOrigin : MonoBehaviour //도구 복귀 스크립트
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    public void SetHeldState(bool held)
    {
        if (!held)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.isKinematic = true;
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            rb.isKinematic = false;
        }
    }
}
