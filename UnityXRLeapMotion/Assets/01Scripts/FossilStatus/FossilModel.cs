using UnityEngine;
using System;
using NUnit.Framework;
using System.Collections.Generic;

public class FossilModel : MonoBehaviour
{
    public static FossilModel instance;

    [SerializeField] private BoneModel[] _bones;
    [SerializeField] private HardRockModel[] _rocks;

    private int _boneCount = 0;
    private int _rockCount = 0;
    private int _dirtCount = 0;

    public event Action<int> OnBoneCountChanged;
    public event Action<int> OnRockCountChanged;
    public event Action<int> OnDirtCountChanged;

    public int BrokenBoneCount => _boneCount;
    public int BrokenRockCount => _rockCount;
    public int DirtCount => _dirtCount;

    public int BoneSize => _bones.Length;
    public int RockSize => _rocks.Length;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _boneCount = _bones.Length;
        _rockCount = _rocks.Length;

        foreach (BoneModel bone in _bones)
        {
            bone.OnBoneDestroyed += UpdateBoneCount;
        }
        foreach (HardRockModel rock in _rocks)
        {
            rock.OnRockDestroyed += UpdateRockCount;
        }
    }

    public void UpdateBoneCount(int value)
    {
        _boneCount = Mathf.Clamp(_boneCount + value, 0, _bones.Length);
        OnBoneCountChanged?.Invoke(_boneCount);
    }

    public void UpdateRockCount(int value)
    {
        _rockCount = Mathf.Clamp(_rockCount + value, 0, _rocks.Length);
        OnRockCountChanged?.Invoke(_rockCount);
    }

    public void UpdateDirtCount(int value)
    {
        _dirtCount = Mathf.Clamp(_dirtCount + value, 0, 1000);
        OnDirtCountChanged?.Invoke(_dirtCount);
    }
}