using UnityEngine;

[System.Serializable]
public class PointData
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _description;

    public int Id => _id;
    public string Name => _name;
    public Sprite Sprite => _sprite;
    public string Description => _description;
}
