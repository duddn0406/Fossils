using System.Collections.Generic;
using UnityEngine;

public class ToolContactTrigger : MonoBehaviour
{
    /*[Tooltip("�� ������ ������ ��� ������Ʈ�� �±�")]
    public string targetTag = "Rock1";  // ������ ���� �ٸ� �±� ����*/

    // ���� �±� �� ������ �ϼ� �±� ����
    private Dictionary<string, string> toolToTargetTag = new Dictionary<string, string>
    {
        { "hand_pick",      "Rock1" },
        { "hand_shovel",    "Rock2" },
        { "margin_trowel",  "Rock3" },
        { "sm_brush",       "Rock4" },
        { "trowel",         "Rock5" }
    };

    private string targetTag;
    private HashSet<Collider> triggeredColliders = new HashSet<Collider>(); //�̹� ������ ��ȣ�ۿ��� ������Ʈ�� �� ��ȣ�ۿ� ���ϰ� ��

    private void Start()
    {
        string myTag = gameObject.tag;

        if (toolToTargetTag.TryGetValue(myTag, out string mappedTag))
        {
            targetTag = mappedTag;
            Debug.Log($"[{name}] �� �±�: '{myTag}' �� ������ ��� �±�: '{targetTag}'");
        }
        else
        {
            Debug.LogWarning($"[{name}] '{myTag}' �±״� ���εǾ� ���� �ʽ��ϴ�. Ʈ���� �۵� �� ��.");
            enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggeredColliders.Contains(other)) return;

        if (other.CompareTag(targetTag))
        {
            triggeredColliders.Add(other);
            Debug.Log($"[{name}] {targetTag} �±��� {other.name} �� �浹! �̺�Ʈ �߻�!");

            // TODO: ���⿡ ���� �̺�Ʈ (��: �ı�, ȿ��, ���� ��)
        }
    }
}

