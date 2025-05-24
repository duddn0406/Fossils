using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

//public class FossilView : ViewBase
//{
//    [SerializeField] private float _tmpAnimDuration = 0.5f;
//    [SerializeField] private float _animSpan = 0.5f;
//
//    private Coroutine _stressCor;
//    private Coroutine _subscriberCor;
//
//    public enum Images
//    {
//        StressImage,
//        FamousImage
//    }
//    
//    public enum Tmps
//    {
//        SubscriberText,  
//    }
//    public enum Buttons
//    {
//        StateButton,
//    }
//
//    public event Action OnStateButtonClicked;
//
//    private void Awake()
//    {
//        Bind<TextMeshProUGUI>(typeof(Tmps));
//        Bind<Image>(typeof(Images));
//        Bind<Button>(typeof(Buttons));
//    }
//
//    private IEnumerator ChangeColorCoroutine(Image image, int amount)
//    {
//        float start = image.fillAmount;
//        float end = Mathf.Max(0f, amount) / 100f;
//        float elapsed = 0f;
//
//        while (elapsed < 0.3f)
//        {
//            elapsed += Time.deltaTime;
//            float t = elapsed / 0.3f;
//            image.fillAmount = Mathf.Lerp(start, end, t);
//            yield return null;
//        }
//
//        image.fillAmount = end;
//    }
//
//    public void UpdatePlayerStress(int value)
//    {
//        Image image = GetImage((int)Images.StressImage);
//
//        if(_stressCor != null) 
//            StopCoroutine(_stressCor);
//
//        _stressCor = StartCoroutine(ChangeColorCoroutine(image, value));
//    }
//
//    public void UpdatePlayerSubscribers(int value)
//    {
//        TextMeshProUGUI tmp = GetTmp((int)Tmps.SubscriberText);
//
//        if (_subscriberCor != null)
//            StopCoroutine(_subscriberCor);
//
//        _subscriberCor = StartCoroutine(ShowResultCo(tmp, value));
//        
//        _previousSubscriber = value;
//    }
//}
