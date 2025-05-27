using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContinentView : ViewBase
{
    public enum Tmps
    {
        DescriptionText
    }

    public enum Images
    {
        ContinentImage
    }

    void Awake()
    {
        // 여기서 Enum 이름 기준으로 오브젝트 자동 등록
        Debug.Log("ContinentView Awake 실행됨"); // 이거 추가
        Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Image>(typeof(Images));
    }
}
