using UnityEngine;

public class MainMenuSceneUI : MonoBehaviour
{
    [SerializeField] private ContinentPresenter _continentPresenter;
    [SerializeField] private PointPresenter _pointPresenter;
    [SerializeField] private GameObject _continentViewPanel;
    [SerializeField] private GameObject _pointViewPanel;

    public void InitializeContinentView(ContinentData continentData)
    {
        _continentViewPanel.SetActive(true);
        _continentPresenter.Initialize(continentData);
    }

    public void InitializePointView(PointData pointData)
    {
        _pointViewPanel.SetActive(true);
        _pointPresenter.Initialize(pointData);
    }

    public void  ResetContinentView()
    {
        _continentViewPanel.SetActive(false);
        _continentPresenter.Initialize(null);
    }
    public void ResetPointView()
    {
        _pointViewPanel.SetActive(false);
        _pointPresenter.Initialize(null);
    }
}
