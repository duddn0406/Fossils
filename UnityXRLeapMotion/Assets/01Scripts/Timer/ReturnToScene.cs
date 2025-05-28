using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToScene : MonoBehaviour
{
    [SerializeField] private float _maxTime;

    private float _curTime;

    private void Update()
    {
        _curTime += Time.deltaTime;

        if(_curTime > _maxTime)
        {
            SceneManager.LoadScene("00Scenes/MainMenuScene");
        }

        if (Input.GetKeyDown(KeyCode.A))
            SceneManager.LoadScene("00Scenes/MainMenuScene");

        if(Input.GetKeyDown(KeyCode.S))
        {
            if(SceneManager.GetActiveScene().name == "GameScene")
            {
                SceneManager.LoadScene("00Scenes/ResultScene");
            }
        }
    }
    public void SetCurTime(float curTime)
    {
        _curTime = curTime;
    }
}
