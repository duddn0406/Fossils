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
        bool wasVisible = isVisible;

        if (leftHand == null)
        {
            initialized = false;

            if (isVisible)
            {
                isVisible = false;

                if (wasVisible && !isVisible)
                {
                    SoundManager.Instance.PlaySFX("Menu"); // UI 사라질 때도 소리
                }
            }
            MoveCanvas(deltaTime); // 움직임 처리
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

        // ✅ 전환 시점에 사운드 재생
        if (!wasVisible && isVisible)
        {
            SoundManager.Instance.PlaySFX("Menu"); // UI 나타날 때 소리
        }
        else if (wasVisible && !isVisible)
        {
            SoundManager.Instance.PlaySFX("Menu"); // UI 사라질 때 소리
        }

        MoveCanvas(deltaTime);
    }



    public bool IsLeftFist(Hand leftHand)
    {
        if (leftHand == null)
            return false;

        foreach (var finger in leftHand.fingers)
        {
            if (finger.IsExtended) return false; 
        }

        return leftHand.GrabStrength > 0.9f;
        // 또는 return currentLeftHand.GrabStrength > 0.9f;
    }

    private void MoveCanvas(float deltaTime)
    {
        Vector2 targetPos = isVisible ? onScreenPos : offScreenPos;
        canvas.transform.position = Vector2.Lerp(canvas.transform.position, targetPos, deltaTime * slideSpeed);
    }
}
