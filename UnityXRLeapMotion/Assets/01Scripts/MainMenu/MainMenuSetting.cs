using UnityEngine;

[CreateAssetMenu(fileName = "MainMenuSetting", menuName = "MainMenu/Setting")]
public class MainMenuSetting : ScriptableObject
{
    // 불변 데이터
    // ----- Setting ----- //
    public string OptionTitle = "설정";
    public string BgmTitle = "배경음";
    public string SfxTitle = "효과음";
    public string GoogleTitle = "구글";
    public string FacebookTitle = "페이스북";
    public string CompanyText = "SBS 아카데미";
    // ----- Setting ----- //

    // 가변 데이터
    // ----- Model ----- //
    public int Level = 20;
    public string Nickname = "김철수";
    public int Id = 23551325;
    public float ExpRatio = 0.7f;
    public float BgmVolume = 0.5f;
    public float SfxVolume = 1.0f;
    public Sprite AvartarSprite;
    // ----- Model ----- //
}