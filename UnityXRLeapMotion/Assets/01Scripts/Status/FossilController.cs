using UnityEngine;

public class FossilController : MonoBehaviour
{
    [SerializeField] private BoneModel[] _bones;
    [SerializeField] private HardRockModel[] _rocks;

    private int _brokenBoneCount = 0;
    private int _brokenRockCount = 0;
}
