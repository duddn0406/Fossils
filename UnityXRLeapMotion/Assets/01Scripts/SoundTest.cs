using UnityEngine;

public class SoundTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.Instance.PlaySFX("Pick");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManager.Instance.PlaySFX("RockDestroy");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SoundManager.Instance.PlayBGM("Wave");
        }
    }
}