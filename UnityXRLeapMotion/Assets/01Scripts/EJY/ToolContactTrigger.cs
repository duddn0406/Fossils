using System.Collections.Generic;
using UnityEngine;

public class ToolContactTrigger : MonoBehaviour
{
    [Tooltip("이 도구가 반응할 대상 오브젝트의 태그")]
    public string targetTag = "Rock1";  // 도구에 따라 다른 태그 설정

    private HashSet<Collider> triggeredColliders = new HashSet<Collider>(); //이미 도구와 상호작용한 오브젝트는 더 상호작용 못하게 함

    private void OnTriggerEnter(Collider other)
    {
        // 이미 처리한 오브젝트면 무시
        if (triggeredColliders.Contains(other)) return;

        // 태그가 일치할 때만 작동
        if (other.CompareTag(targetTag))
        {
            triggeredColliders.Add(other);

            Debug.Log($"[{name}] {targetTag} 태그의 {other.name} 과의 충돌 감지. 이벤트 발생!");

            // TODO: 여기에 실제 이벤트 코드 작성
        }
    }
}

