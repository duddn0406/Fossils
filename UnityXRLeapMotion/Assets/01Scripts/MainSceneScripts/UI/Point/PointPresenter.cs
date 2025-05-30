﻿using UnityEngine;
using TMPro;

public class PointPresenter : MonoBehaviour
{
    [SerializeField] private PointView _view;

    public void Initialize(PointData pointData)
    {
        _view.SetImageSprite((int)PointView.Images.PointImage, pointData.Sprite);
        _view.SetTmpText((int)PointView.Tmps.DescriptionText, pointData.Description);
    }
}
