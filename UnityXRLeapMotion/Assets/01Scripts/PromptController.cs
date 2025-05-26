using UnityEngine;

public class PromptController : MonoBehaviour
{
    public GameObject stampHigh;
    public GameObject stampMid;
    public GameObject stampLow;

    public void ShowStampByLevel(int level)
    {
        // 모든 도장 끄기
        stampHigh.SetActive(false);
        stampMid.SetActive(false);
        stampLow.SetActive(false);

        // 전달받은 인자에 따라 도장 하나만 켜기
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
                Debug.LogWarning("잘못된 도장 등급 인자!");
                break;
        }
    }
}