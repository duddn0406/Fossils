using UnityEngine;
using Leap;

public class Customizer : MonoBehaviour
{
    public Transform targetObject; // 회전시킬 오브젝트
    public float rotationSensitivity = 100f;

    private Controller controller;

    void Start()
    {
        controller = new Controller();
    }

    void FixedUpdate()
    {
        Frame frame = controller.Frame();
        Hand leftHand = null;

        // 왼손 찾기
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsLeft)
            {
                leftHand = hand;
                break;
            }
        }

        if (leftHand != null)
        {
            // 왼손의 방향(가리키는 방향)에서 yaw 추출
            Vector3 direction = leftHand.Direction;
            float yaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // 회전 적용 (Y축만)
            targetObject.rotation = Quaternion.Euler(0f, yaw * rotationSensitivity*Time.fixedDeltaTime, 0f);
        }
    }
}
