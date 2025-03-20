using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ViewBase : MonoBehaviour
{
    // 어떤 특정 UI 부분의 부모로서 자식 오브젝트들을 미리 찾아놓을 것
    // 각 자식오브젝트를 쉽게 편집하도록 준비.


    // 찾은 컴포넌트들을 저장하는 딕셔너리
    protected Dictionary<Type, UnityEngine.Component[]> _components
        = new Dictionary<Type, UnityEngine.Component[]>();



    /// <summary>
    /// 특정 타입의 컴포넌트를 enum을 활용해 바인딩.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    protected void Bind<T>(Type type) where T : UnityEngine.Component
    {
        // type으로 어떤 enum 타입을 받을 것.
        // enum형으로 자식오브젝트 이름들을 직접 적어 넣고
        // 그것들을 미치 찾아 놓는 기능

        string[] names = Enum.GetNames(type);
        Dictionary<string, int> nameMap = new Dictionary<string, int>();
        for (int i = 0; i < names.Length; i++)
            nameMap[names[i]] = i;

        UnityEngine.Component[] components = new UnityEngine.Component[names.Length];
        _components[typeof(T)] = components;

        UnityEngine.Component[] founds = GetComponentsInChildren<T>(true);
        foreach (var found in founds)
        {
            if (nameMap.TryGetValue(found.name, out var idx))
                components[idx] = found;
        }
    }

    /// <summary>
    /// 바인딩해 둔 컴포넌트 반환
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="idx"></param>
    /// <returns></returns>
    public T Get<T>(int idx) where T : UnityEngine.Component
    {
        if (_components.TryGetValue(typeof(T), out var components) == this
            && idx >= 0 && idx < components.Length)
        {
            return components[idx] as T;
        }

        return null;
    }

    public void SetTextText(int idx, string str)
    {
        Text text = GetText(idx);
        if (text != null)        // if(text)
            text.text = str;
    }

    public void SetTmpText(int idx, string str)
    {
        TextMeshProUGUI tmp = GetTmp(idx);
        if (tmp != null)
            tmp.text = str;
    }
    public void SetImageSprite(int idx, Sprite sprite)
    {
        Image image = GetImage(idx);
        if (image != null)
            image.sprite = sprite;
    }
    public void AddButtonListener(int idx, UnityAction action)
    {
        Button button = GetButton(idx);
        if (button != null)
            button.onClick.AddListener(action);
    }
    public void SetTransformActive(int idx, bool isActive)
    {
        Transform transform = GetTransform(idx);
        if (transform != null)
            transform.gameObject.SetActive(isActive);
    }


    public Text GetText(int idx)
    {
        return Get<Text>(idx);
    }
    public Button GetButton(int idx)
    {
        return Get<Button>(idx);
    }
    public Image GetImage(int idx)
    {
        return Get<Image>(idx);
    }
    public TextMeshProUGUI GetTmp(int idx)
    {
        return Get<TextMeshProUGUI>(idx);
    }
    public Transform GetTransform(int idx)
    {
        return Get<Transform>(idx);
    }

}

