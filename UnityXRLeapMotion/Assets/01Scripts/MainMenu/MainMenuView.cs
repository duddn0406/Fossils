using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : ViewBase
{
    public MainMenuSetting MainMenuSetting;

    enum Images
    {
        UserImage,
        ExpGauge
    }

    enum Texts
    {
        OptionTitle,
        LevelText,
        NameIdText,
        BGMTitle,
        SFXTitle,
        GoogleTitle,
        FacebookTitle,
        CompanyText
    }

    enum Sliders
    {
        BGMSlider,
        SFXSlider
    }

    private void Awake()
    {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));
    }

    private void Start()
    {
        // 특정 이름을 가진 자식오브젝트의
        // 이미지 컴포넌트를 읽어온 것
        GetImage((int)Images.UserImage).sprite = MainMenuSetting.AvartarSprite;
        GetImage((int)Images.ExpGauge).fillAmount = MainMenuSetting.ExpRatio;

        SetTextText((int)Texts.OptionTitle, MainMenuSetting.OptionTitle);
        SetTextText((int)Texts.LevelText, $"LV.{MainMenuSetting.Level}");
        SetTextText((int)Texts.NameIdText, $"{MainMenuSetting.Nickname}\n{MainMenuSetting.Id}");
        SetTextText((int)Texts.BGMTitle, MainMenuSetting.BgmTitle);
        SetTextText((int)Texts.SFXTitle, MainMenuSetting.SfxTitle);
        SetTextText((int)Texts.GoogleTitle, MainMenuSetting.GoogleTitle);
        SetTextText((int)Texts.FacebookTitle, MainMenuSetting.FacebookTitle);
        SetTextText((int)Texts.CompanyText, MainMenuSetting.CompanyText);

        Get<Slider>((int)Sliders.BGMSlider).value = MainMenuSetting.BgmVolume;
        Get<Slider>((int)Sliders.SFXSlider).value = MainMenuSetting.SfxVolume;
    }
}