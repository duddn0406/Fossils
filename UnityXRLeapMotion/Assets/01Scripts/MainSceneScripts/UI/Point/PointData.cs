using UnityEngine;

[System.Serializable]
public class PointData
{
    [SerializeField] private string _name;                  // 화석 이름
    [SerializeField] private Sprite _sprite;                // 지역 배경 이미지
    [SerializeField] private string _description;           // 지역 설명

    [SerializeField] private Sprite _resultSprite;          // 결과(화석) 이미지
    [SerializeField] private string _resultDescription;     // 결과 설명

    public string Name => _name;
    public Sprite Sprite => _sprite;
    public string Description => _description;

    public Sprite ResultSprite => _resultSprite;
    public string ResultDescription => _resultDescription;
}