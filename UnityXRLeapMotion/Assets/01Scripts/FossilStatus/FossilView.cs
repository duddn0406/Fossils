using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class FossilView : ViewBase
{
    [SerializeField] private Sprite _lowerFossilSprite;
    [SerializeField] private Sprite _upperFossilSprite;
    [SerializeField] private Sprite _middleFossilSprite;

    public Sprite LowerFossilSprite => _lowerFossilSprite;
    public Sprite UpperFossilSprite => _upperFossilSprite;
    public Sprite MiddleFossilSprite => _middleFossilSprite;

    public enum Images
    {
        BoneStateImage,
        RockStateImage,
        DirtStateImage,
        FossilStateImage,
    }

    private void Awake()
    {
        Bind<Image>(typeof(Images));
    }
}
