using UnityEngine;

public class HandController : MonoBehaviour
{
    [Header("오른손 회전 대상")]
    public Transform _targetObject;
    public float rotationSensitivity = 100f;

    [Header("왼손 UI 설정")]
    public Transform uiCanvas;
    public Vector2 onScreenPos = new Vector2(0f, 0f);
    public Vector2 offScreenPos = new Vector2(-2f, 0f);
    public float slideSpeed = 5f;
    public float showThreshold = 0.2f;
    public float hideThreshold = -0.1f;

    private LeapHandTracker _leapHandTracker;
    private RightHandRotator _rightHandRotator;
    private LeftHandDetector _leftHandDetector;

    private void Update()
    {
        if(_targetObject != null)
            _leapHandTracker.Update();
    }

    void FixedUpdate()
    {
        if(_targetObject != null)
        {
            _leftHandDetector.UpdateWithHand(_leapHandTracker.LeftHand, Time.fixedDeltaTime);
            if (_leftHandDetector.IsVisible && _leftHandDetector.IsLeftFist(_leapHandTracker.LeftHand))
                _rightHandRotator.RotateWithHand(_leapHandTracker.RightHand, Time.fixedDeltaTime);
        }
    }

    public void Initialize(GameObject targetObject)
    {
        _targetObject = targetObject.transform;
        _targetObject.gameObject.SetActive(true);

        _leapHandTracker = new LeapHandTracker();
        _rightHandRotator = new RightHandRotator(_targetObject, rotationSensitivity);
        _leftHandDetector = new LeftHandDetector(uiCanvas, onScreenPos, offScreenPos, slideSpeed, showThreshold, hideThreshold);
    }
}
