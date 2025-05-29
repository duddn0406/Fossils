using UnityEngine;
using System.Threading.Tasks;
using Leap;
using UnityEngine.SceneManagement;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private MainMenuSceneUI _mainMenuSceneUI;

    [SerializeField] private Transform fingerTip;
    [SerializeField] private LayerMask selectableLayer;
    [SerializeField] private LeapServiceProvider leapProvider;
    [SerializeField] private CameraModel cameraModel;
    [SerializeField] private ReturnToScene _returnToScene;


    [SerializeField] private GameObject[] ContinentObjects;

    private GameObject selectedContinent;
    private GameObject selectedPoint;
    private bool isAnimating = false;

    private float clapCooldown = 1f;
    private float lastClapTime = -10f;

    public bool canAct;

    void Update()
    {
        if (!canAct)
            return;
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
                 Time.time - lastClapTime > clapCooldown) //박수칠 때
                {
                    lastClapTime = Time.time;
                    if (selectedPoint != null)//씬으로 넘어가며 정보 전달.
                    {
                        PointModel pointModel = selectedPoint.GetComponent<PointModel>();
                        GameManager.instance.SetPointData(pointModel.PointData.Name, pointModel.PointData.ResultSprite, pointModel.PointData.ResultDescription);
                        SceneManager.LoadScene("00Scenes/GameScene");
                    }
                }

                foreach (var hand in frame.Hands)
                {
                    if (hand.IsLeft && hand.GrabStrength > 0.8f && selectedContinent != null) //왼손 주먹 쥐었을 때
                    {
                        _returnToScene.SetCurTime(0f); //타이머 초기화    
                        _ = ResetObjectAsync(); //대륙 초기화
                        return;
                    }
                }

                Ray ray = new Ray(fingerTip.position, fingerTip.right);
                //Debug.DrawRay(fingerTip.position, ray.direction * 5f, Color.red);
                if (Physics.Raycast(ray, out RaycastHit hit, 0.1f, selectableLayer))
                {
                    GameObject hitObject = hit.collider.gameObject;

                    if (selectedContinent) //핑 선택
                    {
                        _returnToScene.SetCurTime(0f);

                        if (selectedPoint == hitObject)
                        {
                            return;
                        }
                        else
                        {                   
                            SoundManager.Instance.PlaySFX("Pick");
                            if (selectedPoint == null)
                            {
                                selectedPoint = hitObject;
                                SelectPoint();
                            }
                            else
                            {
                                SpriteRenderer sprite = selectedPoint.GetComponent<SpriteRenderer>();
                                sprite.color = Color.white;
                                selectedPoint = hitObject;
                                SelectPoint();
                            }
                        }
                        _mainMenuSceneUI.ResetContinentView();
                    }
                    else //대륙 선택
                    {
                        SoundManager.Instance.PlaySFX("Pick"); // ✅ 대륙 클릭 사운드 추가!

                        _returnToScene.SetCurTime(0f);
                        _ = ResetObjectAsync();
                        selectedContinent = hitObject;
                        _ = SelectObjectAsync();
                    }
                }
            }
        }
        async Task SelectObjectAsync()
        {
            if (selectedContinent == null) return;

            isAnimating = true;

            var continentModel = selectedContinent.transform.parent.GetComponent<ContinentModel>();
            if (continentModel == null)
            {
                isAnimating = false;
                return;
            }
            _mainMenuSceneUI.InitializeContinentView(continentModel.ContinentData);

            selectedContinent.layer = 0;
            var fadeOutTasks = new Task[ContinentObjects.Length - 1];
            int i = 0;
            foreach (GameObject continent in ContinentObjects)
            {
                if (continent == selectedContinent.transform.parent.gameObject) continue;

                var model = continent.transform.GetComponent<ContinentModel>();
                if (model != null)
                    fadeOutTasks[i++] = model.FadeInAndOutAsync(false, 0.5f);
            }

            var targetContinentPosition = continentModel.transform.position;
            var camTask = cameraModel.MoveToTargetAsync(
                 new Vector3(0.5f,2,0),
                Quaternion.Euler(90f, 0f, 0f),
                1f
            );

            var objTask = continentModel.MoveToTargetAsync(
                continentModel.TargetPos,
                continentModel.TargetSize,
                0.5f
            );

            await Task.WhenAll(camTask, objTask, Task.WhenAll(fadeOutTasks));

            isAnimating = false;
        }

        async Task ResetObjectAsync()
        {
            if (selectedContinent == null) return;

            DeSelectPoint();

            isAnimating = true;

            var selectedModel = selectedContinent.transform.parent.GetComponent<ContinentModel>();
            if (selectedModel == null)
            {
                isAnimating = false;
                return;
            }

            var fadeInTasks = new Task[ContinentObjects.Length - 1];
            int i = 0;
            foreach (GameObject continent in ContinentObjects)
            {
                if (continent == selectedContinent.transform.parent.gameObject) continue;

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

            _mainMenuSceneUI.ResetContinentView();

            selectedContinent.layer = 15;

            selectedContinent = null;
            isAnimating = false;
        }

        void SelectPoint()
        {
            if (selectedPoint == null) return;
            SpriteRenderer sprite = selectedPoint.GetComponent<SpriteRenderer>();
            sprite.color = Color.red;

            PointModel pointModel = selectedPoint.GetComponent<PointModel>();
            //선택한 핑 ui활성화
            _mainMenuSceneUI.InitializePointView(pointModel.PointData);
        }
        void DeSelectPoint()
        {
            if (selectedPoint == null) return;
            SpriteRenderer sprite = selectedPoint.GetComponent<SpriteRenderer>();
            sprite.color = Color.white;
            selectedPoint = null;

            //핑 UI 비활성화
            _mainMenuSceneUI.ResetPointView();
        }
    }

}