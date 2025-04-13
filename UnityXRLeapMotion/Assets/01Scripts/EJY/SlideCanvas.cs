using UnityEngine;

public class SlideCanvas : MonoBehaviour
{
    public RectTransform uiCanvas;
    public Vector2 onScreenPos = new Vector2(0f, 0f);
    public Vector2 offScreenPos = new Vector2(-1000f, 0f);
    public float slideSpeed = 5f;
    public float moveThreshold = 0.1f;

    public Transform handTrackPoint;

    private Vector3 initialHandPosition;
    private bool initialized = false;
    private bool isCanvasVisible = false;

    private void Start()
    {
        if (uiCanvas != null)
        {
            uiCanvas.anchoredPosition = offScreenPos;
        }
    }

    void Update()
    {
        if (handTrackPoint == null)
            return;

        //  손이 벗어났다가 다시 들어온 경우를 자동 처리
        if (!IsHandTracked()) // 트래킹 안 되면 초기화 상태로 되돌림
        {
            initialized = false;
            return;
        }

        //  손이 새로 인식된 경우 → 기준 위치 새로 설정
        if (!initialized)
        {
            initialHandPosition = handTrackPoint.position;
            initialized = true;
        }

        Vector3 currentHandPosition = handTrackPoint.position;
        float handMovement = currentHandPosition.x - initialHandPosition.x;

        if (handMovement > moveThreshold && !isCanvasVisible)
        {
            isCanvasVisible = true;
        }
        else if (handMovement < -moveThreshold && isCanvasVisible)
        {
            isCanvasVisible = false;
        }

        Vector2 targetPos = isCanvasVisible ? onScreenPos : offScreenPos;
        uiCanvas.anchoredPosition = Vector2.Lerp(uiCanvas.anchoredPosition, targetPos, Time.deltaTime * slideSpeed);
    }

    //  트래킹 상태 판별 함수
    private bool IsHandTracked()
    {
        // 가장 간단한 방식: 손의 위치가 (0,0,0)이 아닌 경우 트래킹 중이라 판단
        return handTrackPoint.position != Vector3.zero;
    }
}
