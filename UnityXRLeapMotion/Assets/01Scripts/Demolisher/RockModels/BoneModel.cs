using UnityEngine;

public class BoneModel : MonoBehaviour
{
    [SerializeField] private int _hitCount;
    private Rigidbody _rigid;
    private MeshCollider _col;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _col = GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Test"))
        {
            _hitCount++;
            if (_hitCount == 5)
            {
                Debug.Log("break");
                _rigid.useGravity = true;
                _col.isTrigger = false;
                //Destroy(this.gameObject);
            }
        }
    }

    //뭐에 닿든 이거 호출.
    public void GetDamage()
    {
        _hitCount++;
        if (_hitCount == 5)
        {
            _rigid.useGravity = true;
            Destroy(this.gameObject);
        }
    }
}
