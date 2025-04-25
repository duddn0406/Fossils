using UnityEngine;

public class PointModel : MonoBehaviour
{
    [SerializeField] private PointData _pointData;

    public PointData PointData=> _pointData;
}
