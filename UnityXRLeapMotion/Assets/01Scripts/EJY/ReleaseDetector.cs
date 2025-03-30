using UnityEngine;

public class ReleaseDetector : MonoBehaviour
{
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("PhysicalHand"))
        {
            // 도구 안의 ReturnToOrigin을 호출
            GetComponent<ReturnToOrigin>()?.SetHeldState(false);
        }
    }
}
