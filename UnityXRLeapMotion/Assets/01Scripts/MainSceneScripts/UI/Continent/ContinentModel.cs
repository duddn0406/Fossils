using UnityEngine;
using System.Threading.Tasks;

public class ContinentModel : MonoBehaviour
{
    [SerializeField] private ContinentData _continentData;
    [SerializeField] private SpriteRenderer[] childSprites;
    [SerializeField] private GameObject[] childPoints;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _startSize;

    [SerializeField] private Vector3 _targetPos;
    [SerializeField] private Vector3 _targetSize;

    public ContinentData ContinentData => _continentData;
    public Vector3 StartPos => _startPos;
    public Vector3 StartSize => _startSize;
    public Vector3 TargetPos => _targetPos;
    public Vector3 TargetSize => _targetSize;

    private bool isMoveAnimating = false;
    private bool isSpriteAnimating = false;

    void Awake()
    {
        _startPos = transform.position;
        _startSize = transform.localScale;

        // ✅ 지도가 올라간 만큼 _targetPos를 동적으로 설정 (1.402f 보정)
        _targetPos = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z   // 필요 시 카메라 방향으로 살짝 이동
        );
    
}

    public async Task MoveToTargetAsync(Vector3 targetPos, Vector3 _targetSize, float duration)
    {
        if (isMoveAnimating) return;

        if (duration == 1f)
        {
            DeActivatePoints();
        }

        isMoveAnimating = true;

        Vector3 startPos = transform.position;
        Vector3 startScale = transform.localScale;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.localScale = Vector3.Lerp(startScale, _targetSize, t);
            elapsed += Time.deltaTime;
            await Task.Yield();
        }

        transform.position = targetPos;
        transform.localScale = _targetSize;

        isMoveAnimating = false;
        if (duration == 0.5f)
        {
            ActivatePoints();
        }
    }

    public async Task FadeInAndOutAsync(bool fadeIn, float duration)
    {
        if(isSpriteAnimating) return;
        isSpriteAnimating = true;

        if(fadeIn)
        {
            this.gameObject.SetActive(true);
        }

        float startAlpha = fadeIn ? 0f : 1f;
        float endAlpha = fadeIn ? 1f : 0f;

        float elapsed = 0f;
        if (fadeIn)
        {
            await Task.Delay(500);
        }
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, t);

            foreach (SpriteRenderer sprite in childSprites)
            {
                Color color = sprite.color;
                color.a = alpha;
                sprite.color = color;
            }

            elapsed += Time.deltaTime;
            await Task.Yield();
        }

        foreach (SpriteRenderer sprite in childSprites)
        {
            Color color = sprite.color;
            color.a = endAlpha;
            sprite.color = color;
        }

        if (!fadeIn)
        {
            this.gameObject.SetActive(false);
        }

        isSpriteAnimating = false;
    }
    public void DeActivatePoints()
    {
        for (int i = 0; i < childPoints.Length; i++)
        {
            childPoints[i].SetActive(false);
        }
    }
    public void ActivatePoints()
    {
        for (int i = 0; i < childPoints.Length; i++)
        {
            childPoints[i].SetActive(true);
        }
    }
}
