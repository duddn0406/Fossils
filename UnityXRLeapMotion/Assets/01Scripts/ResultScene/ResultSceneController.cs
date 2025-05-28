using UnityEngine;

public class ResultSceneController : MonoBehaviour
{
    [SerializeField] private FadeManager _fadeManager;
    void Start()
    {
        _fadeManager.FadeIn();
    }
}
