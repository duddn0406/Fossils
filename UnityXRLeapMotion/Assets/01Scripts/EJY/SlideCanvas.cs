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

        //  ���� ����ٰ� �ٽ� ���� ��츦 �ڵ� ó��
        if (!IsHandTracked()) // Ʈ��ŷ �� �Ǹ� �ʱ�ȭ ���·� �ǵ���
        {
            initialized = false;
            return;
        }

        //  ���� ���� �νĵ� ��� �� ���� ��ġ ���� ����
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

    //  Ʈ��ŷ ���� �Ǻ� �Լ�
    private bool IsHandTracked()
    {
        // ���� ������ ���: ���� ��ġ�� (0,0,0)�� �ƴ� ��� Ʈ��ŷ ���̶� �Ǵ�
        return handTrackPoint.position != Vector3.zero;
    }
}
