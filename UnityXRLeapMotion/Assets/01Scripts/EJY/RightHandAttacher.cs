using UnityEngine;

public class RightHandAttacher : MonoBehaviour
{
    [Header("오른손 기준 트랜스폼")]
    public Transform palmCenter;

    public Transform thumbTip;
    public Transform indexTip;
    public Transform middleTip;
    public Transform ringTip;
    public Transform pinkyTip;

    [Header("소환할 도구 프리팹 UI")]
    public ObjectSpawner spawner; // Inspector에서 연결

    [Header("손 기준 오프셋 위치")]
    public Vector3 toolOffset = new Vector3(0f, 0.03f, 0.0f); // 손 아래, 약간 앞으로

    private GameObject currentTool;
    private bool wasFist = false;

    void Update()
    {
        bool isFist = IsFistClosed();

        if (isFist && !wasFist)
        {
            AttachToolToPalm();
        }
        else if (!isFist && wasFist)
        {
            DetachTool();
        }

        wasFist = isFist;
    }

    private bool IsFistClosed()
    {
        float threshold = 0.12f;

        float d1 = Vector3.Distance(thumbTip.position, palmCenter.position);
        float d2 = Vector3.Distance(indexTip.position, palmCenter.position);
        float d3 = Vector3.Distance(middleTip.position, palmCenter.position);
        float d4 = Vector3.Distance(ringTip.position, palmCenter.position);
        float d5 = Vector3.Distance(pinkyTip.position, palmCenter.position);

        //Debug.Log($"거리 thumb={d1}, index={d2}, middle={d3}, ring={d4}, pinky={d5}");

        return d1 < threshold && d2 < threshold && d3 < threshold && d4 < threshold && d5 < threshold;
    }

    private void AttachToolToPalm()
    {
        if (currentTool != null) return;
        
        foreach (GameObject obj in spawner.GetCurrentSpawnedObjects())
        {
            if (obj != null && obj.tag != "hammer")
            {
                currentTool = obj;
                break;
            }
        }
        if (currentTool == null) return;


        currentTool.transform.SetParent(palmCenter);


        // 태그별 회전값 설정
        Quaternion rotation = Quaternion.identity;

        switch (currentTool.tag)
        {
            case "hand_pick":
                rotation = Quaternion.Euler(0, 0, 90);
                break;
            case "hand_shovel":
                rotation = Quaternion.Euler(0, 0, 270);
                toolOffset = new Vector3(-0.3f, 0.03f, 0f);
                break;
            case "margin_trowel":
                rotation = Quaternion.Euler(0, 0, 90);
                break;
            case "sm_brush":
                rotation = Quaternion.Euler(0, 0, 0);
                toolOffset = new Vector3(-0.08f, -0.03f, -0.04f);
                break;
            case "trowel":
                rotation = Quaternion.Euler(0, 0, 20);
                toolOffset = new Vector3(-0.04f, -0.08f, -0.04f);
                break;
            case "chisel":
                rotation = Quaternion.Euler(0, 90, 90);
                toolOffset = new Vector3(-0.07f, 0.04f, 0f);
                break;
            default:
                rotation = Quaternion.identity;
                break;
        }
        currentTool.transform.localPosition = toolOffset;
        currentTool.transform.localRotation = rotation;

        Rigidbody rigid = currentTool.GetComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.angularVelocity = Vector3.zero;
        rigid.linearVelocity = Vector3.zero;

        toolOffset = new Vector3(0f, 0.03f, 0.0f);
    }

    public void AttackTool()
    {
        foreach (GameObject obj in spawner.GetCurrentSpawnedObjects())
        {
            if (obj != null && obj.tag != "hammer")
            {
                currentTool = obj;
                break;
            }
        }
        if (currentTool == null) return;
    }

    public void DetachTool()
    {
        if (currentTool == null) return;

        currentTool.transform.SetParent(null); // 부모 관계 해제
        currentTool.GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("손을 놓음");
        currentTool = null;
    }
}
