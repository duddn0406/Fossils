using UnityEngine;
using Leap;

public class CameraMoveController : MonoBehaviour
{
    public Vector3 startPos = new Vector3(-21.36f, 9.14f, -26.64f); // 초기 멀리서 보는 시점
    public Vector3 endPos = new Vector3(0.51f, 2.08f, -0.42f);      // 지도 위 시점
    public Vector3 startRot = new Vector3(0f, 30.927f, 0f);
    public Vector3 endRot = new Vector3(60f, 0f, 0f);

    public float moveDuration = 2f;

    private float moveTimer = 0f;
    public bool isMoving = false;

    public bool hasMovedOnce = false; // ✅ 손 감지 후 카메라 이동을 한 번만 허용
    private LeapServiceProvider leapProvider;

    void Start()
    {
        transform.position = startPos;
        transform.rotation = Quaternion.Euler(startRot);

        leapProvider = FindAnyObjectByType<LeapServiceProvider>();
    }

    void Update()
    {
        if (!hasMovedOnce && leapProvider.CurrentFrame.Hands.Count > 0)
        {
            // 처음 손 감지 시, 이동 시작
            isMoving = true;
            hasMovedOnce = true;
            moveTimer = 0f;
        }

        if (isMoving)
        {
            moveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(moveTimer / moveDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Lerp(startPos, endPos, smoothT);
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(startRot), Quaternion.Euler(endRot), smoothT);

            if (t >= 1f)
            {
                isMoving = false;
            }
        }
    }
}