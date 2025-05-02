using JetBrains.Annotations;
using UnityEngine;

public class SoilModel : MonoBehaviour
{
    [SerializeField] private int _hitCount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Test")
        {
            _hitCount++;
            if (_hitCount == 5)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
