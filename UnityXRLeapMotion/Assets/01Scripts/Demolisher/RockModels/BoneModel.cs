using UnityEngine;

public class BoneModel : MonoBehaviour
{
    [SerializeField] private int _hitCount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Test")
        {
            _hitCount++;
            if (_hitCount == 5)
            {
                //CreateSoils();
                Destroy(this.gameObject);
            }
        }
    }
}
