using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUI : MonoBehaviour
{
    public Image dinoImage;
    public TextMeshProUGUI dinoDescriptionText;
    public int state; // 상태 텍스트 (또는 다른 UI)
    public Image stampImage;
    public Sprite stampHigh;
    public Sprite stampMid;
    public Sprite stampLow;

    void Start()
    {
        // GameManager에서 값 받아오기
        var gm = GameManager.instance;

        // UI에 반영
        dinoImage.sprite = gm.DinoSprite;
        dinoDescriptionText.text = gm.DinoDescription;

        // 상태에 따라 텍스트 또는 이미지 변경
        switch (gm.state)
        {
            case 0: // 상
                stampImage.sprite = stampHigh;
                break;
            case 1: // 중
                stampImage.sprite = stampMid;
                break;
            case 2: // 하 
                stampImage.sprite = stampLow;
                break;
        }
    }
}
