using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUI : MonoBehaviour
{
    public Image dinoImage;
    public TextMeshProUGUI dinoDescriptionText;
    public int state; // ���� �ؽ�Ʈ (�Ǵ� �ٸ� UI)
    public Image stampImage;
    public Sprite stampHigh;
    public Sprite stampMid;
    public Sprite stampLow;

    void Start()
    {
        // GameManager���� �� �޾ƿ���
        var gm = GameManager.instance;

        // UI�� �ݿ�
        dinoImage.sprite = gm.DinoSprite;
        dinoDescriptionText.text = gm.DinoDescription;

        // ���¿� ���� �ؽ�Ʈ �Ǵ� �̹��� ����
        switch (gm.state)
        {
            case 0: // ��
                stampImage.sprite = stampHigh;
                break;
            case 1: // ��
                stampImage.sprite = stampMid;
                break;
            case 2: // �� 
                stampImage.sprite = stampLow;
                break;
        }
    }
}
