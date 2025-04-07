using UnityEngine;

public class ReturnToOrigin : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;

    [Header("���� ���� ���� ����")]
    public Vector3 areaCenter = Vector3.zero;       // ���� �߽� ��ġ
    public Vector3 areaSize = new Vector3(0.1f, 0.01f, 0.01f);  // ���� ũ�� (x, y, z)

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

        Debug.Log("���� ������ ��� ������ ���� ��ġ�� ���ͽ�Ŵ!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(areaCenter, areaSize);
    }
}


/*public class ReturnToOrigin : MonoBehaviour //���� ���� ��ũ��Ʈ
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