using UnityEngine;

public class LeftHandAttacher : MonoBehaviour
{
    [Header("�޼� ���� Ʈ������")]
    public Transform palmCenter;

    public Transform thumbTip;
    public Transform indexTip;
    public Transform middleTip;
    public Transform ringTip;
    public Transform pinkyTip;

    [Header("��ȯ�� ���� ������ UI")]
    public ObjectSpawner spawner; // Inspector���� ����

    [Header("�� ���� ������ ��ġ")]
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

        // chisel�� �޼տ� ���̵��� ����
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

        // �±׺� ȸ���� �� ������
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

        // ������ �ʱ�ȭ
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
