using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PointData _pointData;

    public PointData PointData { get => _pointData; set=>_pointData = value; }

    public Sprite state; //0상 //1중 //2하
}
