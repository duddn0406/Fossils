using UnityEngine;

public class PromptController : MonoBehaviour
{
    public GameObject stampHigh;
    public GameObject stampMid;
    public GameObject stampLow;

    public void ShowStampByLevel(int level)
    {
        // ��� ���� ����
        stampHigh.SetActive(false);
        stampMid.SetActive(false);
        stampLow.SetActive(false);

        // ���޹��� ���ڿ� ���� ���� �ϳ��� �ѱ�
        switch (level)
        {
            case 0:
                stampHigh.SetActive(true);
                break;
            case 1:
                stampMid.SetActive(true);
                break;
            case 2:
                stampLow.SetActive(true);
                break;
            default:
                Debug.LogWarning("�߸��� ���� ��� ����!");
                break;
        }
    }
}