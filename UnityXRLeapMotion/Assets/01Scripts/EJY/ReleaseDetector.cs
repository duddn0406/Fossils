using UnityEngine;

public class ReleaseDetector : MonoBehaviour
{
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("PhysicalHand"))
        {
            // ���� ���� ReturnToOrigin�� ȣ��
            GetComponent<ReturnToOrigin>()?.SetHeldState(false);
        }
    }
}
