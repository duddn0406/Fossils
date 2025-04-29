using UnityEngine;

public class MainMenuSceneUI : MonoBehaviour
{
    [SerializeField] private ContinentPresenter _continentPresenter;
    [SerializeField] private PointPresenter _pointPresenter;

    public void InitializeContinentView(ContinentData continentData)
    {
        _continentPresenter.gameObject.SetActive(true);
        _continentPresenter.Initialize(continentData);
    }

    public void InitializePointView(PointData pointData)
    {
        _pointPresenter.gameObject.SetActive(true);
        _pointPresenter.Initialize(pointData);
    }

    public void  ResetContinentView()
    {
        _continentPresenter.gameObject.SetActive(false);
        _continentPresenter.Initialize(null);
    }
    public void ResetPointView()
    {
        _pointPresenter.gameObject.SetActive(false);
        _pointPresenter.Initialize(null);
    }
}
