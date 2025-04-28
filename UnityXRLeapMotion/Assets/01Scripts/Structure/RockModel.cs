using UnityEngine;

public class RockModel : MonoBehaviour
{
    [SerializeField] private DemolisherController _controller;

    private int _hitCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Test")
        {
            _hitCount++;
            Debug.Log(_hitCount);
            if (_hitCount == 5)
            {
                _controller.CreatePoints(gameObject, 5);

                Material mat = GetComponent<MeshRenderer>().materials[0];
                _controller.CreateDemolish(gameObject, 0.7f, null);
                Destroy(gameObject);
            }
        }
    }
}
