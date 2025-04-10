using UnityEngine;
using TMPro;

public class PointPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _pointDescriptionParent;
    [SerializeField] private TextMeshProUGUI _pointDescriptionText;

    public void ActivatePointParent()
    {
        _pointDescriptionParent.SetActive(true);
    }
    public void DeactivatePointParent()
    {
        _pointDescriptionParent.SetActive(false);
    }
    public void SetPointDescription(string text)
    {
        _pointDescriptionText.text = text;
    }
}
