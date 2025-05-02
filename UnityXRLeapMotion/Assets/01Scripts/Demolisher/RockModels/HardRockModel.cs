using UnityEngine;

public class HardRockModel : MonoBehaviour
{
    private int _hitCount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Test")
        {
            _hitCount++;
            if (_hitCount == 5)
            {
                DemolisherController.instance.GeneratePointAndDemolish(this.gameObject, 10, 0.8f);
                Destroy(this.gameObject);
            }
        }
    }
}
