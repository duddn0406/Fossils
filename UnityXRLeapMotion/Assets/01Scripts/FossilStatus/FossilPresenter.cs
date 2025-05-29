using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class FossilPresenter : MonoBehaviour
{
    [SerializeField] private FossilView _view;
    [SerializeField] private FossilModel _model;

    [SerializeField] private ResultUI _resultUi;

    private void Awake()
    {
        _model.OnBoneCountChanged += UpdateBoneCount;
        _model.OnBoneCountChanged += UpdateFossilState;
        _model.OnRockCountChanged += UpdateRockCount;
        _model.OnDirtCountChanged += UpdateDirtCount;
    }

    public void UpdateBoneCount(int value)
    {
        if (GameManager.instance.GameOver)
            return;
        Image image = _view.GetImage((int)FossilView.Images.BoneStateImage);
        image.fillAmount = value / (float)_model.BoneSize;
        CheckForSceneMove();
    }

    public void UpdateRockCount(int value)
    {
        if (GameManager.instance.GameOver)
            return;
        Image image = _view.GetImage((int)FossilView.Images.RockStateImage);
        image.fillAmount = value / (float)_model.RockSize;
        CheckForSceneMove();
    }

    public void UpdateDirtCount(int value)
    {
        if (GameManager.instance.GameOver)
            return;
        Image image = _view.GetImage((int)FossilView.Images.DirtStateImage);
        image.fillAmount = value / (float)_model.DirtSize;
        CheckForSceneMove();
    }

    private void UpdateFossilState(int value)
    {
        if (GameManager.instance.GameOver)
            return;
        Image image = _view.GetImage((int)FossilView.Images.FossilStateImage);
        int max = _model.BoneSize;

        int lowerBound = max * 40 / 100;
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

    public void CheckForSceneMove()
    {
        Image image = _view.GetImage((int)FossilView.Images.FossilStateImage);

        int endRock = _model.RockSize * 5 / 100;
        //int endDirt = _model.DirtSize * 10 / 100;

        int badEndDirt = _model.DirtSize * 90 / 100;
        int badEndBone = _model.BoneSize * 40 / 100;

        //if (_model.RockCount < endRock && _model.DirtCount < endDirt)
        if (_model.RockCount < endRock)
        {
            ShowResult();
            GameManager.instance.GameOver = true;
        }
   
        if (_model.BoneCount < badEndBone || _model.DirtCount > badEndDirt)
        {
            image.sprite = _view.LowerFossilSprite;
            ShowResult();
            GameManager.instance.GameOver = true;
        }
    }

    public void ShowResult()
    {
        Image image = _view.GetImage((int)FossilView.Images.FossilStateImage);

        GameManager.instance.SetState(image.sprite);
        _resultUi.Initialize();
    }
}