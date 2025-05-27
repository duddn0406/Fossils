using Unity.VisualScripting;
using UnityEngine;

public class DirtModel : MonoBehaviour
{ 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "sm_brush") //붓
        {
            GetDamage();
        }
       
        if(collision.gameObject.layer == LayerMask.NameToLayer("Rock"))
        {
            Vector3 worldPos = this.transform.position;
            this.gameObject.transform.parent = collision.transform;
            this.transform.position = worldPos;
            Rigidbody rigid = GetComponent<Rigidbody>();
            rigid.useGravity = false;
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            rigid.angularVelocity = Vector3.zero;
            rigid.linearVelocity = Vector3.zero;
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sm_brush") //붓
        {
            GetDamage();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Bone"))
        {
            Vector3 worldPos = this.transform.position;
            this.gameObject.transform.parent = other.transform;
            this.transform.position = worldPos;
            Rigidbody rigid = GetComponent<Rigidbody>();
            rigid.useGravity = false;
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            rigid.angularVelocity = Vector3.zero;
            rigid.linearVelocity = Vector3.zero;
            GetComponent<SphereCollider>().isTrigger = true;

            FossilModel.instance.UpdateDirtCount(1);

            //this.gameObject.transform.parent = collision.transform;
            //this.transform.position -= new Vector3(0, -0.05f, 0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bone"))
        {
            FossilModel.instance.UpdateDirtCount(-1);
        }
    }

    //흙 붓?으로 닿았을 때 이거 호출
    public void GetDamage()
    {
        Destroy(this.gameObject);
    }
}
