using UnityEngine;

[CreateAssetMenu(fileName = "MainMenuSetting", menuName = "MainMenu/Setting")]
public class MainMenuSetting : ScriptableObject
{
    // �Һ� ������
    // ----- Setting ----- //
    public string OptionTitle = "����";
    public string BgmTitle = "�����";
    public string SfxTitle = "ȿ����";
    public string GoogleTitle = "����";
    public string FacebookTitle = "���̽���";
    public string CompanyText = "SBS ��ī����";
    // ----- Setting ----- //

    // ���� ������
    // ----- Model ----- //
    public int Level = 20;
    public string Nickname = "��ö��";
    public int Id = 23551325;
    public float ExpRatio = 0.7f;
    public float BgmVolume = 0.5f;
    public float SfxVolume = 1.0f;
    public Sprite AvartarSprite;
    // ----- Model ----- //
}