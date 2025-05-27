using UnityEngine;

public class PointView : ViewBase
{
    public enum Tmps
    {
        DescriptionText
    }

    public enum Images
    {
        PointImage
    }

    void Awake()
    {
        // 이름 기준으로 UI 요소 자동 바인딩
        BindTextMeshProUGUI(typeof(Tmps));
        BindImage(typeof(Images));
    }
}
