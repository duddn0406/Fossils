using Unity.VisualScripting;
using UnityEngine;

public class DirtModel : MonoBehaviour
{
    private int _hitCount;

    private bool _canAttach;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "sm_brush") //붓
        {
            GetDamage();
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Bone"))
        {
            Rigidbody rigid = GetComponent<Rigidbody>();
            rigid.useGravity = false;
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            rigid.angularVelocity = Vector3.zero;
            rigid.linearVelocity = Vector3.zero;
            GetComponent<SphereCollider>().isTrigger = true;
            this.gameObject.transform.parent = collision.transform;
            //this.transform.position -= new Vector3(0, -0.05f, 0);
        }
    }

    //흙 붓?으로 닿았을 때 이거 호출
    public void GetDamage()
    {
        _hitCount++;
        if (_hitCount == 5)
        {
            Destroy(this.gameObject);
        }
    }
}
