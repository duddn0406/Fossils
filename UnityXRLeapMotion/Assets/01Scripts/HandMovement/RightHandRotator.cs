using UnityEngine;
using Leap;

public class RightHandRotator
{
    private readonly Transform target;
    private readonly float sensitivity;

    public RightHandRotator(Transform targetObject, float rotationSensitivity)
    {
        target = targetObject;
        sensitivity = rotationSensitivity;
    }

    public void RotateWithHand(Hand rightHand, float deltaTime)
    {
        if (rightHand == null || target == null) return;

        Vector3 direction = rightHand.Direction;
        float yaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        target.rotation = Quaternion.Euler(-40f, yaw * sensitivity * deltaTime, 0f);
    }
}
