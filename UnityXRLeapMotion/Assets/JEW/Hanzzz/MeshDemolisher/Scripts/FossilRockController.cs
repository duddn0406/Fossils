using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hanzzz.MeshDemolisher;

public class FossilRockController : MonoBehaviour
{
    [Header("오브젝트 참조")]
    public GameObject fossil;        // 화석 오브젝트
    public GameObject rock;          // 이암 오브젝트 (Sphere)
    public Material interiorMaterial; // 조각 내부 재질

    [Header("파괴 설정")]
    [SerializeField] private float fragmentLiftHeight = 0.1f;  // 조각이 뜨는 높이
    [SerializeField] private float explosionForce = 300f;      // 폭발력
    [SerializeField] private float explosionRadius = 5f;       // 폭발 반경
    [SerializeField] private int baseBreakPoints = 20;         // 기본 브레이크포인트 개수
    [SerializeField] private float fragmentStabilizeDelay = 2f; // 조각 안정화 시간

    private MeshDemolisher demolisher;
    private int hitCount = 0;
    private List<Transform> breakPoints;
    private const int MAX_HITS = 3; // 총 3번 타격 가능

    void Start()
    {
        demolisher = new MeshDemolisher();
        breakPoints = new List<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && hitCount < MAX_HITS)
        {
            HitRock();
        }
    }

    private void HitRock()
    {
        hitCount++;
        CreateBreakPoints(hitCount);

        if (demolisher.VerifyDemolishInput(rock, breakPoints))
        {
            List<GameObject> fragments = demolisher.Demolish(rock, breakPoints, interiorMaterial);
            rock.SetActive(false);

            bool isLastHit = (hitCount == MAX_HITS);
            ProcessFragments(fragments, isLastHit);
        }
    }

    private void ProcessFragments(List<GameObject> fragments, bool isLastHit)
    {
        foreach (GameObject fragment in fragments)
        {
            // 조각을 살짝 위로 띄움
            fragment.transform.position += Vector3.up * fragmentLiftHeight;

            // 물리 컴포넌트 추가
            Rigidbody rb = fragment.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.mass = 1f;

            // 테이블 아래로 떨어지는 것을 방지하기 위한 콜라이더
            MeshCollider collider = fragment.AddComponent<MeshCollider>();
            collider.convex = true;

            if (isLastHit)
            {
                // 마지막 타격시 폭발력 추가 (위쪽으로도 약간 튀게)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.3f);
            }
        }

        // 마지막 타격시 조각들을 나중에 안정화
        if (isLastHit)
        {
            StartCoroutine(RemoveRigidbodiesAfterDelay(fragments));
        }
    }

    private void CreateBreakPoints(int currentHit)
    {
        breakPoints.Clear();

        int pointCount = baseBreakPoints * currentHit; // 타격수에 비례해서 증가
        float radius = 0.5f; // 포인트 퍼지는 반경

        for (int i = 0; i < pointCount; i++)
        {
            GameObject point = new GameObject($"BreakPoint_{currentHit}_{i}");
            point.transform.SetParent(transform); // 계층구조 정리

            // Y축으로는 덜 퍼지게 조정
            Vector3 randomPos = Random.insideUnitSphere;
            randomPos.y *= 0.5f; // Y축 방향으로는 절반만큼만 퍼짐
            point.transform.position = rock.transform.position + randomPos * radius;

            breakPoints.Add(point.transform);
        }
    }

    private IEnumerator RemoveRigidbodiesAfterDelay(List<GameObject> fragments)
    {
        yield return new WaitForSeconds(fragmentStabilizeDelay);
        foreach (GameObject fragment in fragments)
        {
            if (fragment != null)
            {
                Rigidbody rb = fragment.GetComponent<Rigidbody>();
                if (rb != null)
                    Destroy(rb);
            }
        }
    }

    private void OnDestroy()
    {
        // 생성된 브레이크포인트들 정리
        foreach (Transform point in breakPoints)
        {
            if (point != null)
                Destroy(point.gameObject);
        }
    }
}
