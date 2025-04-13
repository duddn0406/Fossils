using UnityEngine;

public class FingerImageTouch : MonoBehaviour
{
    [Header("5���� �̹��� ������Ʈ")]
    public Transform[] imageObjects = new Transform[5];  // �̹��� 5��

    [Header("�հ��� �� ��ġ")]
    public Transform indexTip;

    [Header("��ġ �Ÿ� (����: ����)")]
    public float touchDistance = 0.02f;
    public float touchCooldown = 1.0f; // 1�� ���� ����ġ ����

    private bool[] hasTriggered = new bool[5];
    private float[] lastTouchTime = new float[5];

    public ObjectSpawner spawner; // Inspector���� ����

    void Update()
    {
        if (indexTip == null) return;

        for (int i = 0; i < imageObjects.Length; i++)
        {
            if (imageObjects[i] == null) continue;

            float distance = Vector3.Distance(imageObjects[i].position, indexTip.position);

            // ���� �ð� - ������ ��ġ �ð� > ��Ÿ���̾�߸� ��ġ �ν�
            if (distance < touchDistance && !hasTriggered[i] && Time.time - lastTouchTime[i] > touchCooldown)
            {
                hasTriggered[i] = true;
                lastTouchTime[i] = Time.time;
                OnImageTouched(i);
            }
            // ���� ����� �־����� ���� �ٽ� ��ġ �����ϵ��� �ʱ�ȭ
            else if (distance >= touchDistance * 1.5f)
            {
                hasTriggered[i] = false;
            }
        }
    }

    void OnImageTouched(int index)
    {
        Debug.Log($"�̹��� {index + 1} Ŭ����!");
        spawner.SpawnObject(index);
    }
}
