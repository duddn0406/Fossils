using Unity.VisualScripting;
using UnityEngine;
using System;

public class BoneModel : MonoBehaviour
{
    [SerializeField] private int _health;
    private Rigidbody _rigid;
    private MeshCollider _col;

    public event Action<int> OnBoneDestroyed;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _col = GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hand_pick" 
            || other.gameObject.tag == "chisel"
            || other.gameObject.tag == "trowel")
        {
            _health++;
            if (_health == 5)
            {
                Debug.Log("break");
                _rigid.useGravity = true;
                _col.isTrigger = false;
                OnBoneDestroyed?.Invoke(-1);
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
            _health --;
        }
        else if(collisionObject.tag == "trowel") //모종삽
        {
            _health-=3;
        }

        if (_health < 0)
        {
            _rigid.useGravity = true;
            _col.isTrigger = false;
        }
    }
}
