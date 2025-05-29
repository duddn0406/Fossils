using LeapInternal;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private string _name;
    [SerializeField] private Sprite _resultSprite;
    [SerializeField] private string _resultDescription;
    [SerializeField] private Sprite _state; //0상 //1중 //2하

    public string Name => _name;
    public Sprite ResultSprite => _resultSprite;
    public string ResultDescription => _resultDescription;
    public Sprite State => _state;

    public bool GameOver;

    public void SetPointData(string name, Sprite resultSprite, string resultDescription)
    {
        _name = name;
        _resultSprite = resultSprite;
        _resultDescription = resultDescription;
        Debug.Log("Hi");
    }
    public void SetState(Sprite state)
    {
        _state = state;
    }
}
