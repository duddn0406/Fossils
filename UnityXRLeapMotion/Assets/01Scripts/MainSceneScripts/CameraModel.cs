using UnityEngine;
using System.Threading.Tasks;

public class CameraModel : MonoBehaviour
{
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Quaternion _startRot;

    public Vector3 StartPos => _startPos;
    public Quaternion StartRot => _startRot;

    private bool isAnimating = false;

    void Awake()
    {
        //_startPos = transform.position;
        //_startRot = transform.rotation;
    }

    public async Task MoveToTargetAsync(Vector3 targetPos, Quaternion targetRot, float duration)
    {
        if (isAnimating) return;
        isAnimating = true;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            elapsed += Time.deltaTime;
            await Task.Yield();
        }

        transform.position = targetPos;
        transform.rotation = targetRot;
        isAnimating = false;
    }
}
