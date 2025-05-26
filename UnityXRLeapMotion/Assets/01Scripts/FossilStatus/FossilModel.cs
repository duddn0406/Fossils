using UnityEngine;
using System;

public class FossilModel : MonoBehaviour
{
    [SerializeField] private BoneModel[] _bones;
    [SerializeField] private HardRockModel[] _rocks;

    private int _boneCount = 0;
    private int _rockCount = 0;

    public event Action<int> OnBoneCountChanged;
    public event Action<int> OnRockCountChanged;
    public int BrokenBoneCount => _boneCount;
    public int BrokenRockCount => _rockCount;
    public int BoneSize => _bones.Length;
    public int RockSize => _rocks.Length;

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
}