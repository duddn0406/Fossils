using UnityEngine;

public class RockModel : MonoBehaviour
{
    [SerializeField] private DemolisherController _controller;
    [SerializeField] private int _hitCount;

    [Header("Demolisher 필드")]
    [SerializeField] private float _demolishScale;

    [Header("PointGenerator 필드")]
    [SerializeField] private int _pointCount;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Test")
        {
            _hitCount++;
            if(_hitCount == 5)
            {
                _controller.GeneratePointAndDemolish(this.gameObject, _pointCount, _demolishScale);
                Destroy(this.gameObject);
            }
        }
    }
}
