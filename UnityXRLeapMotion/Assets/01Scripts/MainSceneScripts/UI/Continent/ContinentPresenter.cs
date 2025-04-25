using UnityEngine;

public class ContinentPresenter : MonoBehaviour
{
    [SerializeField] private ContinentView _view;

    public void Initialize(ContinentData continentData)
    {
        if (continentData == null)
            return;

        _view.SetImageSprite((int)ContinentView.Images.ContinentImage, continentData.Sprite);
        _view.SetTmpText((int)ContinentView.Tmps.DescriptionText, continentData.Description);
    }
}
