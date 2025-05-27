using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PointData _pointData;

    public PointData PointData { get => _pointData; set=>_pointData = value; }

    public Sprite DinoSprite;
    public string DinoDescription;
    public int state; //0상 //1중 //2하

    public void setSpriteAndDesc()
    {
        DinoSprite = PointData.Sprite;
        DinoDescription = PointData.Description;
    }

    public void setState(int value)
    {
        state = value;
    }
}
