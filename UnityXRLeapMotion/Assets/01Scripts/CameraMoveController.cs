using UnityEngine;
using Leap;

public class CameraMoveController : MonoBehaviour
{
    public Vector3 startPos = new Vector3(-21.36f, 9.14f, -26.64f);
    public Vector3 endPos = new Vector3(0.51f, 2.08f, -0.42f);
    public Vector3 startRot = new Vector3(0f, 30.927f, 0f);
    public Vector3 endRot = new Vector3(60f, 0f, 0f);

    public float moveDuration = 2f;
    private float moveTimer = 0f;

    public bool isMoving = false;
    public bool hasMovedOnce = false;

    private LeapServiceProvider leapProvider;

    [SerializeField] private AudioSource audioSource; 

    void Start()
    {
        transform.position = startPos;
        transform.rotation = Quaternion.Euler(startRot);

        leapProvider = FindAnyObjectByType<LeapServiceProvider>();
    }

    void Update()
    {
        // 👉 트리거는 단 한 번
        if (!hasMovedOnce && Input.GetKeyDown(KeyCode.Space))
        //if (!hasMovedOnce && leapProvider.CurrentFrame.Hands.Count > 0) - 립모션 사용할때
        {
            isMoving = true;
            hasMovedOnce = true;
            moveTimer = 0f; 

           
            if (audioSource != null)
            {
                audioSource.pitch = 1f; // 혹시 pitch 문제 대비
                audioSource.PlayOneShot(audioSource.clip);
                Debug.Log("📢 효과음 재생!");
            }
            else
            {
                Debug.LogWarning("⚠️ AudioSource가 연결되지 않았습니다!");
            }
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
