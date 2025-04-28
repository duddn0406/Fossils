using Hanzzz.MeshDemolisher;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DemolisherController : MonoBehaviour
{
    [SerializeField] private MyDemolisher _demolisher;
    [SerializeField] private PointGenerator _generator;

    [SerializeField] private GameObject _resultGameObject;

    /// <summary>
    /// Demolisher을 위한 Point 생성
    /// </summary>
    /// <param name="target">taget에 대한 Point 생성</param>
    /// <param name="pointCount">생성할 Point 개수</param>
    public void CreatePoints(GameObject target, int pointCount) 
    {
        _generator.domainGameObject = target;
        _generator.pointCount = pointCount;

        _generator.Generate();
    }

    /// <summary>
    /// Demolish 실행
    /// </summary>
    /// <param name="target">Demolish할 대상</param>
    /// <param name="resultScale">결과 크기(빈 정도)</param>
    /// <param name="mat">메터리얼</param>
    public void CreateDemolish(GameObject target, float resultScale, Material mat)
    {
        if (_resultGameObject.transform.childCount > 0)
        {
            GameObject clone = new GameObject();
            for(int i=0; i< _resultGameObject.transform.childCount; i++)
            {
                Transform child = _resultGameObject.transform.GetChild(i);
                child.SetParent(clone.transform);
            }
        }

        _demolisher.TargetGameObject = target;
        _demolisher.ResultScale = resultScale;
        if(mat != null) 
            _demolisher.InteriorMaterial = mat;

        _demolisher.CustomDemolish();
    }
}
