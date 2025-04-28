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

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Image>(typeof(Images));
    }
}