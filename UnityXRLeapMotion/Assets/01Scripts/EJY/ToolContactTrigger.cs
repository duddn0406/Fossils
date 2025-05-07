using System.Collections.Generic;
using UnityEngine;

public class ToolContactTrigger : MonoBehaviour
{
    /*[Tooltip("이 도구가 반응할 대상 오브젝트의 태그")]
    public string targetTag = "Rock1";  // 도구에 따라 다른 태그 설정*/

    // 도구 태그 → 반응할 암석 태그 매핑
    private Dictionary<string, string> toolToTargetTag = new Dictionary<string, string>
    {
        { "hand_pick",      "Rock1" },
        { "hand_shovel",    "Rock2" },
        { "margin_trowel",  "Rock3" },
        { "sm_brush",       "Rock4" },
        { "trowel",         "Rock5" }
    };

    private string targetTag;
    private HashSet<Collider> triggeredColliders = new HashSet<Collider>(); //이미 도구와 상호작용한 오브젝트는 더 상호작용 못하게 함

    private void Start()
    {
        string myTag = gameObject.tag;

        if (toolToTargetTag.TryGetValue(myTag, out string mappedTag))
        {
            targetTag = mappedTag;
            Debug.Log($"[{name}] 내 태그: '{myTag}' → 반응할 대상 태그: '{targetTag}'");
        }
        else
        {
            Debug.LogWarning($"[{name}] '{myTag}' 태그는 매핑되어 있지 않습니다. 트리거 작동 안 함.");
            enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggeredColliders.Contains(other)) return;

        if (other.CompareTag(targetTag))
        {
            triggeredColliders.Add(other);
            Debug.Log($"[{name}] {targetTag} 태그의 {other.name} 과 충돌! 이벤트 발생!");

            // TODO: 여기에 실제 이벤트 (예: 파괴, 효과, 점수 등)
        }
    }
}

