using UnityEngine;

public class FingerImageTouch : MonoBehaviour
{
    [Header("6개의 이미지 오브젝트")]
    public Transform[] imageObjects = new Transform[6];  // 이미지 5개

    [Header("손가락 끝 위치")]
    public Transform indexTip;

    [Header("터치 거리 (단위: 미터)")]
    public float touchDistance = 0.02f;
    public float touchCooldown = 1.0f; // 1초 동안 재터치 금지

    private bool[] hasTriggered = new bool[7];
    private float[] lastTouchTime = new float[7];

    public ObjectSpawner spawner; // Inspector에서 연결

    void Update()
    {
        if (indexTip == null) return;

        for (int i = 0; i < imageObjects.Length; i++)
        {
            if (imageObjects[i] == null) continue;

            float distance = Vector3.Distance(imageObjects[i].position, indexTip.position);

            // 현재 시간 - 마지막 터치 시간 > 쿨타임이어야만 터치 인식
            if (distance < touchDistance && !hasTriggered[i] && Time.time - lastTouchTime[i] > touchCooldown)
            {
                hasTriggered[i] = true;
                lastTouchTime[i] = Time.time;
                OnImageTouched(i);
            }
            // 손이 충분히 멀어졌을 때만 다시 터치 가능하도록 초기화
            else if (distance >= touchDistance * 1.5f)
            {
                hasTriggered[i] = false;
            }
        }
    }

    void OnImageTouched(int index)
    {
        Debug.Log($"이미지 {index + 1} 클릭됨!");

        // 🔊 도구 클릭 사운드 재생
        SoundManager.Instance.PlaySFX("Pick");
        spawner.SpawnObject(index);
    }
}
