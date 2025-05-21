using UnityEngine;
using Leap;

public class SlideCanvas : MonoBehaviour
{
    public Transform uiCanvas;
    public Vector2 onScreenPos = new Vector2(0f, 0f);
    public Vector2 offScreenPos = new Vector2(-1000f, 0f);
    public float slideSpeed = 5f;

    public float clapCooldown = 0.8f;
    public float preClapMinDistance = 0.25f; // 박수 전 손 간 거리 조건

    private float lastClapTime = -10f;
    private bool isCanvasVisible = false;
    private bool isReadyToClap = false;

    private Controller controller;

    void Start()
    {
        controller = new Controller();

        if (uiCanvas != null)
        {
            uiCanvas.transform.position = offScreenPos;
        }
    }

    void Update()
    {
        Frame frame = controller.Frame();
        Hand leftHand = null;
        Hand rightHand = null;

        foreach (var hand in frame.Hands)
        {
            if (hand.IsLeft) leftHand = hand;
            if (hand.IsRight) rightHand = hand;
        }

        if (leftHand != null && rightHand != null)
        {
            Vector3 leftPos = new Vector3(leftHand.PalmPosition.x, leftHand.PalmPosition.y, leftHand.PalmPosition.z);
            Vector3 rightPos = new Vector3(rightHand.PalmPosition.x, rightHand.PalmPosition.y, rightHand.PalmPosition.z);
            float distance = Vector3.Distance(leftPos, rightPos);

            Vector3 leftVel = new Vector3(leftHand.PalmVelocity.x, leftHand.PalmVelocity.y, leftHand.PalmVelocity.z);
            Vector3 rightVel = new Vector3(rightHand.PalmVelocity.x, rightHand.PalmVelocity.y, rightHand.PalmVelocity.z);
            Vector3 relativeVelocity = leftVel - rightVel;

            // 박수 준비 조건: 손이 충분히 떨어져 있을 때
            if (distance > preClapMinDistance)
            {
                isReadyToClap = true;
            }

            // 박수 감지 조건
            if (isReadyToClap &&
                distance < 0.1f &&
                Vector3.Dot(relativeVelocity, (leftPos - rightPos).normalized) < -0.5f &&
                Time.time - lastClapTime > clapCooldown)
            {
                lastClapTime = Time.time;
                isCanvasVisible = !isCanvasVisible;
                isReadyToClap = false; // 박수 처리 후 초기화
            }
        }

        Vector2 targetPos = isCanvasVisible ? onScreenPos : offScreenPos;
        uiCanvas.transform.position = Vector2.Lerp(uiCanvas.transform.position, targetPos, Time.deltaTime * slideSpeed);
    }
}
