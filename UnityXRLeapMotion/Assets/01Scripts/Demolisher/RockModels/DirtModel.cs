using Unity.VisualScripting;
using UnityEngine;

public class DirtModel : MonoBehaviour
{
    private void OnEnable()
    {
        FossilModel.instance.UpdateDirtCount(1);
    }
    private void OnDisable()
    {
        FossilModel.instance.UpdateDirtCount(-2);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "sm_brush") //붓
        {
            GetDamage();
            SoundManager.Instance.PlaySFX("Brush");
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
            SoundManager.Instance.PlaySFX("Brush");
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

            //this.gameObject.transform.parent = collision.transform;
            //this.transform.position -= new Vector3(0, -0.05f, 0);
        }
    }

    //흙 붓?으로 닿았을 때 이거 호출
    public void GetDamage()
    {
        Destroy(this.gameObject);
    }
}
