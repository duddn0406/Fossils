using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanzzz.MeshDemolisher;

public class GameManager : MonoBehaviour
{
    [SerializeField] MeshDemolisherExample target;
    [SerializeField] Transform resultTransform;
    [SerializeField] List<PieceModel> models;

    private void Start()
    {
        target.Demolish();
        SetModels();
    }

    void SetModels()
    {
        for(int i=0; i<resultTransform.childCount; i++)
        {
            PieceModel model = resultTransform.GetChild(i).gameObject.AddComponent<PieceModel>();
            Rigidbody rigid = model.gameObject.AddComponent<Rigidbody>();
            MeshCollider col = model.gameObject.AddComponent<MeshCollider>();
            col.convex = true;
            rigid.useGravity = false;
            RigidbodyConstraints constraints = rigid.constraints;
            constraints |= RigidbodyConstraints.FreezePositionX;
            constraints |= RigidbodyConstraints.FreezePositionY;
            constraints |= RigidbodyConstraints.FreezePositionZ;
            constraints |= RigidbodyConstraints.FreezeRotation;
            rigid.constraints = constraints;
            models.Add(model);
        }
    }
}
