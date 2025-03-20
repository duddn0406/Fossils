using UnityEngine;

[System.Serializable]
public class ContinentData
{
    [SerializeField] FossilData[] _fossilDatas;

    public FossilData[] FossilDatas => _fossilDatas;

    public FossilData GetFossilDataById(int id)
    {
        if (id < _fossilDatas.Length) 
            return _fossilDatas[id];

        return null;
    }
}
[System.Serializable]
public class FossilData
{
    [SerializeField] int _fossilId;
    [SerializeField] string _fossilName;
    [SerializeField] string _fossilDescription;

    public int FossilId => _fossilId;
    public string FossilName => _fossilName;
    public string FossilDescription => _fossilDescription;

    public FossilData(int fossilId, string fossilName, string fossilDescription)
    {
        _fossilId = fossilId;
        _fossilName = fossilName;
        _fossilDescription = fossilDescription;
    }
}

public class MainMenuController : MonoBehaviour
{
    [SerializeField] ContinentData[] _continentDatas;

    //줌 기능

    //선택 기능
    public void OnClick(int id)
    {
        //FossilData fossilData = Foss 
    }

    //설명 표시 기능

    void Start() 
    {
        Invoke("F", 2);
        InvokeRepeating("F", 5f, 0.1f);
    }

    private void F()
    {
        Debug.Log("Hi");
    }
}
