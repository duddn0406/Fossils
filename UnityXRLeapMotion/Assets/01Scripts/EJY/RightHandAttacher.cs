using UnityEngine;

public class RightHandAttacher : MonoBehaviour
{
    [Header("������ ���� Ʈ������")]
    public Transform palmCenter;

    public Transform thumbTip;
    public Transform indexTip;
    public Transform middleTip;
    public Transform ringTip;
    public Transform pinkyTip;

    [Header("��ȯ�� ���� ������ UI")]
    public ObjectSpawner spawner; // Inspector���� ����

    [Header("�� ���� ������ ��ġ")]
    public Vector3 toolOffset = new Vector3(0f, -0.03f, 0.07f); // �� �Ʒ�, �ణ ������

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

        //Debug.Log($"�Ÿ� thumb={d1}, index={d2}, middle={d3}, ring={d4}, pinky={d5}");

        return d1 < threshold && d2 < threshold && d3 < threshold && d4 < threshold && d5 < threshold;
    }

    private void AttachToolToPalm()
    {
        if (currentTool != null) return;

        GameObject spawned = spawner.GetCurrentSpawnedObject();
        if (spawned == null) return;

        currentTool = spawned;

        currentTool.transform.SetParent(palmCenter);
        currentTool.transform.localPosition = toolOffset;
        currentTool.transform.localRotation = Quaternion.Euler(0, 90, 90);

        // �߷� ���� ����
        Rigidbody rb = currentTool.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    private void DetachTool()
    {
        if (currentTool == null) return;

        // ���� �ٽ� ����
        Rigidbody rb = currentTool.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        currentTool.transform.SetParent(null); // �θ� ���� ����
        Debug.Log("���� ����");
        currentTool = null;
    }
}
