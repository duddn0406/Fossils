using UnityEngine;
using Leap;

public class LeftHandDetector
{
    private readonly Transform canvas;
    private readonly Vector2 onScreenPos;
    private readonly Vector2 offScreenPos;
    private readonly float slideSpeed;
    private readonly float showThreshold;
    private readonly float hideThreshold;

    private Vector3 initialHandPosition;
    private bool initialized = false;
    private bool isVisible = false;
    public bool IsVisible => isVisible;

    public LeftHandDetector(Transform uiCanvas, Vector2 onPos, Vector2 offPos, float speed, float showThresh, float hideThresh)
    {
        canvas = uiCanvas;
        onScreenPos = onPos;
        offScreenPos = offPos;
        slideSpeed = speed;
        showThreshold = showThresh;
        hideThreshold = hideThresh;

        canvas.transform.position = offScreenPos;
    }

    public void UpdateWithHand(Hand leftHand, float deltaTime)
    {
        if (leftHand == null)
        {
            initialized = false;
            isVisible = false; // ✅ 손이 사라지면 UI 자동 off
            MoveCanvas(deltaTime);
            return;
        }

        Vector3 current = leftHand.PalmPosition;

        if (!initialized)
        {
            initialHandPosition = current;
            initialized = true;
        }

        float deltaX = current.x - initialHandPosition.x;

        if (deltaX > showThreshold && !isVisible)
        {
            isVisible = true;
        }
        else if (deltaX < hideThreshold && isVisible)
        {
            isVisible = false;
        }

        MoveCanvas(deltaTime);
    }

    private void MoveCanvas(float deltaTime)
    {
        Vector2 targetPos = isVisible ? onScreenPos : offScreenPos;
        canvas.transform.position = Vector2.Lerp(canvas.transform.position, targetPos, deltaTime * slideSpeed);
    }
}
