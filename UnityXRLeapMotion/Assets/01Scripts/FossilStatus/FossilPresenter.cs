using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class FossilPresenter : MonoBehaviour
{
    [SerializeField] private FossilView _view;
    [SerializeField] private FossilModel _model;

    private void Awake()
    {
        _model.OnBoneCountChanged += UpdateBoneCount;
        _model.OnBoneCountChanged += UpdateFossilState;
        _model.OnRockCountChanged += UpdateRockCount;
    }

    public void UpdateBoneCount(int value)
    {
        Image image = _view.GetImage((int)FossilView.Images.BoneStateImage);
        Debug.Log(value);
        image.fillAmount = value / (float)_model.BoneSize;
    }

    public void UpdateRockCount(int value)
    {
        Image image = _view.GetImage((int)FossilView.Images.RockStateImage);
        Debug.Log(value);
        image.fillAmount = value / (float)_model.RockSize;
    }

    private void UpdateFossilState(int value)
    {
        Image image = _view.GetImage((int)FossilView.Images.FossilStateImage);
        int max = _model.BoneSize;

        int lowerBound = max * 20 / 100;
        int upperBound = max * 80 / 100;

        if (value < lowerBound)
        {
            image.sprite = _view.LowerFossilSprite;
        }
        else if(value < upperBound)
        {
            image.sprite = _view.MiddleFossilSprite;
        }
        else
        {
            image.sprite = _view.UpperFossilSprite;
        }
    }
}