using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Image>(typeof(Images));
    }
}
