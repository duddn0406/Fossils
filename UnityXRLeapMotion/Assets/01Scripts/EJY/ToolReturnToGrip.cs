using UnityEngine;

public class ToolReturnToGrip : MonoBehaviour
{
    public Transform gripTarget; // ���� palmCenter
    public Vector3 gripOffset = Vector3.zero;
    public Vector3 gripRotationOffset = new Vector3(0, 90, 90); // ����

    public float positionThreshold = 0.05f; // 5cm
    public float rotationThreshold = 10f;    // 10��

    public float returnSpeed = 5f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private bool isTracking = false;

    void Update()
    {
        if (!isTracking || gripTarget == null) return;

        // local �������� ���� ��ġ ���
        Vector3 currentLocalPos = transform.localPosition;
        Quaternion currentLocalRot = transform.localRotation;

        // ���� ���
        float posDiff = Vector3.Distance(currentLocalPos, gripOffset);
        float rotDiff = Quaternion.Angle(currentLocalRot, Quaternion.Euler(gripRotationOffset));

        if (posDiff > positionThreshold)
        {
            transform.localPosition = Vector3.Lerp(currentLocalPos, gripOffset, Time.deltaTime * returnSpeed);
        }

        if (rotDiff > rotationThreshold)
        {
            transform.localRotation = Quaternion.Slerp(currentLocalRot, Quaternion.Euler(gripRotationOffset), Time.deltaTime * returnSpeed);
        }
    }

    public void StartTracking()
    {
        if (gripTarget == null) return;

        isTracking = true;

        // ���� ��ġ = palmCenter ��ġ + ������
        targetPosition = gripTarget.position + gripTarget.rotation * gripOffset;
        targetRotation = gripTarget.rotation * Quaternion.Euler(gripRotationOffset);
    }

    public void StopTracking()
    {
        isTracking = false;
    }
}
