using UnityEngine;
using UnityEngine.UI;

public class ResultPresenter : MonoBehaviour
{
    [SerializeField] private ResultView _view;

    public void UpdateBoneCount(int value)
    {
        Image image = _view.GetImage((int)FossilView.Images.BoneStateImage);
        Debug.Log(value);
        //image.fillAmount = value / (float)_model.BoneSize;
    }

    public void UpdateRockCount(int value)
    {
        Image image = _view.GetImage((int)FossilView.Images.RockStateImage);
        Debug.Log(value);
        //image.fillAmount = value / (float)_model.RockSize;
    }

    public void UpdateDirtCount(int value)
    {
        Image image = _view.GetImage((int)FossilView.Images.DirtStateImage);
        Debug.Log(value);
        //image.fillAmount = value / 1000.0f;
    }
}
