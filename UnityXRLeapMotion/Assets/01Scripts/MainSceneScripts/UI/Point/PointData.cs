using UnityEngine;

[System.Serializable]
public class PointData
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _description;

    public Sprite Sprite => _sprite;
    public string Description => _description;
}
