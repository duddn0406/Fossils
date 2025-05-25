using UnityEngine;

public class ToolReturnToGrip : MonoBehaviour
{
    public Transform gripTarget; // 손의 palmCenter
    public Vector3 gripOffset = Vector3.zero;
    public Vector3 gripRotationOffset = new Vector3(0, 90, 90); // 예시

    public float positionThreshold = 0.05f; // 5cm
    public float rotationThreshold = 10f;    // 10도

    public float returnSpeed = 5f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private bool isTracking = false;

    void Update()
    {
        if (!isTracking || gripTarget == null) return;

        // local 기준으로 복원 위치 계산
        Vector3 currentLocalPos = transform.localPosition;
        Quaternion currentLocalRot = transform.localRotation;

        // 차이 계산
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

        // 기준 위치 = palmCenter 위치 + 오프셋
        targetPosition = gripTarget.position + gripTarget.rotation * gripOffset;
        targetRotation = gripTarget.rotation * Quaternion.Euler(gripRotationOffset);
    }

    public void StopTracking()
    {
        isTracking = false;
    }
}
