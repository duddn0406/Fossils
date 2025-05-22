using UnityEngine;

public class HandController : MonoBehaviour
{
    [Header("오른손 회전 대상")]
    public Transform targetObject;
    public float rotationSensitivity = 100f;

    [Header("왼손 UI 설정")]
    public Transform uiCanvas;
    public Vector2 onScreenPos = new Vector2(0f, 0f);
    public Vector2 offScreenPos = new Vector2(-2f, 0f);
    public float slideSpeed = 5f;
    public float showThreshold = 0.2f;
    public float hideThreshold = -0.1f;

    private LeapHandTracker handTracker;
    private RightHandRotator rotator;
    private LeftHandDetector slider;

    void Start()
    {
        handTracker = new LeapHandTracker();
        rotator = new RightHandRotator(targetObject, rotationSensitivity);
        slider = new LeftHandDetector(uiCanvas, onScreenPos, offScreenPos, slideSpeed, showThreshold, hideThreshold);
    }

    private void Update()
    {
        handTracker.Update();
    }

    void FixedUpdate()
    {
        slider.UpdateWithHand(handTracker.LeftHand, Time.fixedDeltaTime);
        if (slider.IsVisible)
            rotator.RotateWithHand(handTracker.RightHand, Time.fixedDeltaTime);
    }
}
