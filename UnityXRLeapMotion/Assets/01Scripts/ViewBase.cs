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
    // � Ư�� UI �κ��� �θ�μ� �ڽ� ������Ʈ���� �̸� ã�Ƴ��� ��
    // �� �ڽĿ�����Ʈ�� ���� �����ϵ��� �غ�.


    // ã�� ������Ʈ���� �����ϴ� ��ųʸ�
    protected Dictionary<Type, UnityEngine.Component[]> _components
        = new Dictionary<Type, UnityEngine.Component[]>();



    /// <summary>
    /// Ư�� Ÿ���� ������Ʈ�� enum�� Ȱ���� ���ε�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    protected void Bind<T>(Type type) where T : UnityEngine.Component
    {
        // type���� � enum Ÿ���� ���� ��.
        // enum������ �ڽĿ�����Ʈ �̸����� ���� ���� �ְ�
        // �װ͵��� ��ġ ã�� ���� ���

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
    /// ���ε��� �� ������Ʈ ��ȯ
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

