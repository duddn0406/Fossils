using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
            
        dinoImage.sprite = gm.PointData.ResultSprite;
        dinoDescriptionText.text = gm.PointData.ResultDescription;
        stampImage.gameObject.SetActive(false);

        StartCoroutine(ShowStampAfterDelay(gm.state));
    }
    IEnumerator ShowStampAfterDelay(Sprite stamp)
    {
        yield return new WaitForSeconds(1f);
        stampImage.sprite = stamp;
        stampImage.gameObject.SetActive(true);
        SoundManager.Instance.PlaySFX("Stamp");
    }
}
