using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private HandController _handController;
    [SerializeField] private FossilModel _fossilModel;

    [SerializeField] private GameObject _animalBone;
    [SerializeField] private BoneModel[] _animalBoneModels;
    [SerializeField] private HardRockModel[] _animalHardRockModels;

    [SerializeField] private GameObject _ammoniteBone;
    [SerializeField] private BoneModel[] _ammoniteBoneModels;
    [SerializeField] private HardRockModel[] _ammoniteHardRockModels;

    [SerializeField] private GameObject _trikeBone;
    [SerializeField] private BoneModel[] _trikeBoneModels;
    [SerializeField] private HardRockModel[] _trikeHardRockModels;

    [SerializeField] private GameObject _qechalBone;
    [SerializeField] private BoneModel[] _qechalBoneModels;
    [SerializeField] private HardRockModel[] _qechalHardRockModels;

    [SerializeField] private GameObject _stegoBone;
    [SerializeField] private BoneModel[] _stegoBoneModels;
    [SerializeField] private HardRockModel[] _stegoHardRockModels;

    public string num;

    private void Start()
    {
        SoundManager.Instance.PlayBGM("gamebgm"); // ✅ 게임씬 진입 시 브금 재생
        Initialize();
    }
    public void Initialize()
    {
        switch(num)
        //switch (GameManager.instance.PointData.Name)
        {
            case "1":
                _handController.Initialize(_animalBone);
                _fossilModel.Initialize(_animalBoneModels, _animalHardRockModels);
                break;
            case "2":
                _handController.Initialize(_ammoniteBone);
                _fossilModel.Initialize(_ammoniteBoneModels, _ammoniteHardRockModels);
                break;
            case "3":
                _handController.Initialize(_trikeBone);
                _fossilModel.Initialize(_trikeBoneModels, _trikeHardRockModels);
                break;
            case "4":
                _handController.Initialize(_qechalBone);
                _fossilModel.Initialize(_qechalBoneModels, _qechalHardRockModels);
                break;
            case "5":
                _handController.Initialize(_stegoBone);
                _fossilModel.Initialize(_stegoBoneModels, _stegoHardRockModels);
                break;
            case "6":
                break;
        }
    }
}
