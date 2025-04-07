using UnityEngine;

public class ReturnToOrigin : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;

    [Header("복귀 조건 영역 설정")]
    public Vector3 areaCenter = Vector3.zero;       // 기준 중심 위치
    public Vector3 areaSize = new Vector3(0.1f, 0.01f, 0.01f);  // 영역 크기 (x, y, z)

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!IsInsideBounds(transform.position))
        {
            ReturnToStart();
        }
    }

    private bool IsInsideBounds(Vector3 position)
    {
        Vector3 min = areaCenter - areaSize * 0.5f;
        Vector3 max = areaCenter + areaSize * 0.5f;

        return (position.x >= min.x && position.x <= max.x) &&
               (position.y >= min.y && position.y <= max.y) &&
               (position.z >= min.z && position.z <= max.z);
    }

    private void ReturnToStart()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        rb.isKinematic = false;

        Debug.Log("영역 밖으로 벗어나 도구를 원래 위치로 복귀시킴!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(areaCenter, areaSize);
    }
}


/*public class ReturnToOrigin : MonoBehaviour //도구 복귀 스크립트
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    public void SetHeldState(bool held)
    {
        if (!held)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.isKinematic = true;
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            rb.isKinematic = false;
        }
    }
}
*/