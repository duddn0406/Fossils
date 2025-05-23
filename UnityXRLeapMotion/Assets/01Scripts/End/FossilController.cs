using UnityEngine;

public class FossilController : MonoBehaviour
{
    [SerializeField] private BoneModel[] _bones;
    [SerializeField] private HardRockModel[] _rocks;

    private int _brokenBoneCount = 0;
    private int _brokenRockCount = 0;

    private void Start()
    {
        foreach(BoneModel bone in _bones)
        {
            bone.OnBoneDestroyed += CheckBoneCount;
        }
        foreach(HardRockModel rock in _rocks)
        {
            rock.OnRockDestroyed += CheckRockCount;
        }
    }

    private void CheckBoneCount()
    {
        _brokenBoneCount++;
        Debug.Log(_brokenBoneCount);
    }
    private void CheckRockCount()
    {
        _brokenRockCount++;
        Debug.Log(_brokenRockCount);
    }
}
