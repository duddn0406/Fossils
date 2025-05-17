using UnityEngine;

public class LeftHandAttacher : MonoBehaviour
{
    [Header("왼손 기준 트랜스폼")]
    public Transform palmCenter;

    public Transform thumbTip;
    public Transform indexTip;
    public Transform middleTip;
    public Transform ringTip;
    public Transform pinkyTip;

    [Header("소환할 도구 프리팹 UI")]
    public ObjectSpawner spawner; // Inspector에서 연결

    [Header("손 기준 오프셋 위치")]
    public Vector3 toolOffset = new Vector3(0f, -0.03f, 0f);

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

        return d1 < threshold && d2 < threshold && d3 < threshold && d4 < threshold && d5 < threshold;
        
    }

    private void AttachToolToPalm()
    {
        if (currentTool != null) return;

        // chisel만 왼손에 붙이도록 제한
        foreach (GameObject obj in spawner.GetCurrentSpawnedObjects())
        {
            if (obj != null && obj.tag == "chisel")
            {
                currentTool = obj;
                break;
            }
        }

        if (currentTool == null) return;

        currentTool.transform.SetParent(palmCenter);

        // 태그별 회전값 및 오프셋
        Quaternion rotation = Quaternion.identity;

        switch (currentTool.tag)
        {
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

        Rigidbody rb = currentTool.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // 오프셋 초기화
        toolOffset = new Vector3(0f, 0.03f, 0f);
    }

    private void DetachTool()
    {
        if (currentTool == null) return;

        Rigidbody rb = currentTool.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        currentTool.transform.SetParent(null);
        currentTool = null;
    }
}
