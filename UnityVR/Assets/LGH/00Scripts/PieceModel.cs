using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceModel : MonoBehaviour
{
    int hitCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tool")
        {
            hitCount++;
        }

        if (hitCount > 10)
        {
            Vector3 hitNormal = collision.contacts[0].normal;

            MeshCollider col = GetComponent<MeshCollider>();
            col.isTrigger = true;

            Rigidbody rigid = GetComponent<Rigidbody>();
            rigid.useGravity = true;

            rigid.constraints = RigidbodyConstraints.None;

            rigid.AddForce(-hitNormal * 100f, ForceMode.Impulse);
            hitCount = 0;
            Debug.Log("Success");
        }
    }
}
