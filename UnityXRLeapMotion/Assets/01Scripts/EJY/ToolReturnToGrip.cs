using UnityEngine;

public class ToolReturnToGrip : MonoBehaviour
{
    public Transform gripTarget; // 손의 palmCenter
    public Vector3 gripOffset = Vector3.zero;
    public Vector3 gripRotationOffset = new Vector3(0, 90, 90); // 예시

    public float positionThreshold = 0.03f; // 3cm
    public float rotationThreshold = 5f;    // 5도

    public float returnSpeed = 5f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Rigidbody rb;
    private bool isTracking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isTracking || gripTarget == null) return;

        targetPosition = gripTarget.TransformPoint(gripOffset);
        targetRotation = gripTarget.rotation * Quaternion.Euler(gripRotationOffset);

        // 위치: 손 방향으로 부드럽게 빨려오게 힘 적용
        Vector3 direction = (targetPosition - rb.position);
        float distance = direction.magnitude;

        // 도착 판단 후 고정 (너무 가까우면 흔들림 제거)
        if (distance < 0.01f)
        {
            rb.linearVelocity = Vector3.zero;
            rb.MovePosition(targetPosition);
        }
        else
        {
            float forceAmount = returnSpeed * rb.mass; // 힘 크기 조절 가능
            rb.AddForce(direction.normalized * forceAmount, ForceMode.Force);
        }

        // 회전: 스무스하게 돌아오도록 회전력 적용
        Quaternion deltaRotation = targetRotation * Quaternion.Inverse(rb.rotation);
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle > 0.01f)
        {
            float torqueAmount = returnSpeed * angle;
            rb.AddTorque(axis * torqueAmount, ForceMode.Force);
        }
    }

    public void StartTracking()
    {
        if (gripTarget == null) return;

        isTracking = true;
        //rb.useGravity = false;

        // 기준 위치 = palmCenter 위치 + 오프셋
        targetPosition = gripTarget.position + gripTarget.rotation * gripOffset;
        targetRotation = gripTarget.rotation * Quaternion.Euler(gripRotationOffset);
    }

    public void StopTracking()
    {
        //rb.useGravity = true;
        isTracking = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
