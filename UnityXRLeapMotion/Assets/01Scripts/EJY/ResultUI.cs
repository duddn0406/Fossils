using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUI : MonoBehaviour
{
    public GameObject ResultGroup;

    public Image dinoImage;
    public TextMeshProUGUI dinoDescriptionText;
    public Image stampImage;

    public void Initialize()
    {
        ResultGroup.SetActive(true);

        var gm = GameManager.instance;

        dinoImage.sprite = gm.ResultSprite;
        dinoDescriptionText.text = gm.ResultDescription;
        stampImage.sprite = gm.State;
    }
}
