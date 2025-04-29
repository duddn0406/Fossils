using Hanzzz.MeshDemolisher;
using JetBrains.Annotations;
using UnityEngine;

public class DemolisherController : MonoBehaviour
{
    [SerializeField] private MeshDemolisherExample _demolisher;
    [SerializeField] private PointGenerator _pointGenerator;

    [SerializeField] private GameObject targetObject;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GeneratePointAndDemolish(targetObject, 10, 0.5f);
        }
    }

    public void GeneratePointAndDemolish(GameObject targetObject, int pointCount, float demolishScale)
    {
        Debug.Log("Hi");

        GameObject clone = new GameObject();
        GameObject pointClone = new GameObject();
        GameObject resultClone = new GameObject();

        pointClone.transform.SetParent(clone.transform);
        resultClone.transform.SetParent(clone.transform);

        _pointGenerator.domainGameObject = targetObject;
        _pointGenerator.pointCount = pointCount;
        _pointGenerator.pointsParent = pointClone.transform;

        _pointGenerator.Generate();

        pointClone.SetActive(false);

        _demolisher.TargetGameObject = targetObject;
        _demolisher.ResultScale = demolishScale;
        _demolisher.BreakPointsParent = pointClone.transform;
        _demolisher.ResultParent = resultClone.transform;

        _demolisher.Demolish();
    }
}
