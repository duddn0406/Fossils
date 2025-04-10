using UnityEngine;

public enum PointType
{
    NA,SA,Eu,AF,AS,OC
}

public class PointModel : MonoBehaviour
{
    [SerializeField] private PointType _pointType;
    [SerializeField] private int _pointIndex;

    public PointType PointType => _pointType;
    public int PointIndex => _pointIndex;   
}
