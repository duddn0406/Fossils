using UnityEngine;

public class ToolReturnToGrip : MonoBehaviour
{
    public Transform gripTarget; // ���� palmCenter
    public Vector3 gripOffset = Vector3.zero;
    public Vector3 gripRotationOffset = new Vector3(0, 90, 90); // ����

    public float positionThreshold = 0.03f; // 3cm
    public float rotationThreshold = 5f;    // 5��

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

        // ��ġ: �� �������� �ε巴�� �������� �� ����
        Vector3 direction = (targetPosition - rb.position);
        float distance = direction.magnitude;

        // ���� �Ǵ� �� ���� (�ʹ� ������ ��鸲 ����)
        if (distance < 0.01f)
        {
            rb.linearVelocity = Vector3.zero;
            rb.MovePosition(targetPosition);
        }
        else
        {
            float forceAmount = returnSpeed * rb.mass; // �� ũ�� ���� ����
            rb.AddForce(direction.normalized * forceAmount, ForceMode.Force);
        }

        // ȸ��: �������ϰ� ���ƿ����� ȸ���� ����
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

        // ���� ��ġ = palmCenter ��ġ + ������
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
