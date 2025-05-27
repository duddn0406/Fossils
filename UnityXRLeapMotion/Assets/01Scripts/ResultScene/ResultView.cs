using UnityEngine;
using UnityEngine.UI;

public class ResultView : ViewBase
{
    public enum Images
    {
        DinoImage,
        StampImage,
    }
    public enum Tmps
    {
        DescriptionText,
    }

    private void Awake()
    {
        Bind<Image>(typeof(Images));
    }
}