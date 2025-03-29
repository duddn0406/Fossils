using UnityEngine;
using System.Threading.Tasks;
using Leap;
using UnityEngine.SceneManagement;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private Transform fingerTip;
    [SerializeField] private LayerMask selectableLayer;
    [SerializeField] private LeapServiceProvider leapProvider;
    [SerializeField] private CameraModel cameraModel;

    [SerializeField] private GameObject[] ContinentObjects;

    private GameObject selectedObject;
    private GameObject selectedPoint;
    private bool isAnimating = false;

    private float clapCooldown = 1f;
    private float lastClapTime = -10f;


    void Update()
    {
        if (!isAnimating)
        {
            Frame frame = leapProvider.CurrentFrame;
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

                if (distance < 0.1f &&
                 Vector3.Dot(relativeVelocity, (leftPos - rightPos).normalized) < -0.5f &&
                 Time.time - lastClapTime > clapCooldown)
                {
                    //Debug.Log("박수");
                    lastClapTime = Time.time;

                    if(selectedPoint != null)
                    {
                        PointModel pointModel = selectedPoint.GetComponent<PointModel>();
                        GameManager.instance.PointType = pointModel.PointType;
                        GameManager.instance.PointIndex = pointModel.PointIndex;
                        SceneManager.LoadScene("00Scenes/GameScene");
                    }
                }
            }

            foreach (var hand in frame.Hands)
            {
                if (hand.IsLeft && hand.GrabStrength > 0.8f && selectedObject != null)
                {
                    DeSelectPoint();
                    _ = ResetObjectAsync();
                    return;
                }
            }

            //Debug.DrawRay(fingerTip.position, fingerTip.right * 0.01f, Color.green);

            Ray ray = new Ray(fingerTip.position, fingerTip.right);
            if (Physics.Raycast(ray, out RaycastHit hit, 0.1f, selectableLayer))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (selectedObject)
                {
                    DeSelectPoint();
                    selectedPoint = hitObject;
                    SelectPoint();
                }
                else
                {
                    _ = ResetObjectAsync();
                    selectedObject = hitObject;
                    _ = SelectObjectAsync();
                }
            }
        }
    }
    async Task SelectObjectAsync()
    {
        if (selectedObject == null) return;

        isAnimating = true;

        var selectedModel = selectedObject.transform.parent.GetComponent<ContinentModel>();
        if (selectedModel == null)
        {
            isAnimating = false;
            return;
        }
        selectedObject.layer = 0;
        var fadeOutTasks = new Task[ContinentObjects.Length - 1];
        int i = 0;
        foreach (GameObject continent in ContinentObjects)
        {
            if (continent == selectedObject.transform.parent.gameObject) continue; 

            var model = continent.transform.GetComponent<ContinentModel>();
            if (model != null)
                fadeOutTasks[i++] = model.FadeInAndOutAsync(false, 0.5f);
        }

        var camTask = cameraModel.MoveToTargetAsync(
            new Vector3(0, 0.7f, 0),
            Quaternion.Euler(90f, 0f, 0f),
            1f
        );

        var objTask = selectedModel.MoveToTargetAsync(
            selectedModel.TargetPos,
            selectedModel.TargetSize,
            0.5f
        );

        await Task.WhenAll(camTask, objTask, Task.WhenAll(fadeOutTasks));

        isAnimating = false;
    }

    async Task ResetObjectAsync()
    {
        if (selectedObject == null) return;

        isAnimating = true;

        var selectedModel = selectedObject.transform.parent.GetComponent<ContinentModel>();
        if (selectedModel == null)
        {
            isAnimating = false;
            return;
        }

        var fadeInTasks = new Task[ContinentObjects.Length - 1];
        int i = 0;
        foreach (GameObject continent in ContinentObjects)
        {
            if (continent == selectedObject.transform.parent.gameObject) continue;

            var model = continent.transform.GetComponent<ContinentModel>();
            if (model != null)
                fadeInTasks[i++] = model.FadeInAndOutAsync(true, 0.5f);
        }

        var camReset = cameraModel.MoveToTargetAsync(
            cameraModel.StartPos,
            cameraModel.StartRot,
            1f
        );

        var objReset = selectedModel.MoveToTargetAsync(
            selectedModel.StartPos,
            selectedModel.StartSize,
            1f
        );

        await Task.WhenAll(camReset, objReset, Task.WhenAll(fadeInTasks));

        selectedObject.layer = 15;

        selectedObject = null;
        isAnimating = false;
    }

    void SelectPoint()
    {
        if (selectedPoint == null) return;
        SpriteRenderer sprite = selectedPoint.GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
    }
    void DeSelectPoint()
    {
        if (selectedPoint == null) return;
        SpriteRenderer sprite = selectedPoint.GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
        selectedPoint = null;
    }
}
