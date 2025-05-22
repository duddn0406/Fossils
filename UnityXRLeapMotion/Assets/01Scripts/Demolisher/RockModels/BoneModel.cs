using Unity.VisualScripting;
using UnityEngine;

public class BoneModel : MonoBehaviour
{
    [SerializeField] private int _health;
    private Rigidbody _rigid;
    private MeshCollider _col;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _col = GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hand_pick" 
            || other.gameObject.tag == "chisel"
            || other.gameObject.tag == "sm_brush")
        {
            _health++;
            if (_health == 5)
            {
                Debug.Log("break");
                _rigid.useGravity = true;
                _col.isTrigger = false;
                //Destroy(this.gameObject);
            }
        }
    }

    //뭐에 닿든 이거 호출.
    public void GetDamage(GameObject collisionObject)
    {
        if (collisionObject.tag == "hand_pick") //곡괭이
        {
            _health-=5;
        }
        else if (collisionObject.tag == "chisel") //끌
        {
            _health -= 3;
        }
        else if(collisionObject.tag == "sm_brush") //붓
        {
            _health--;
        }

        if (_health < 0)
        {
            _rigid.useGravity = true;
            _col.isTrigger = false;
        }
    }
}
