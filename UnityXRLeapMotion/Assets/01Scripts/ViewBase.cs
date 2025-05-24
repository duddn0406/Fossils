using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ViewBase : MonoBehaviour
{
    protected Dictionary<Type, Component[]> _components
        = new Dictionary<Type, Component[]>();

    protected void Bind<T>(Type type) where T : Component
    { 
        string[] names = Enum.GetNames(type);
        Dictionary<string, int> nameMap = new Dictionary<string, int>();
        for (int i = 0; i < names.Length; i++)
            nameMap[names[i]] = i;

        Component[] components = new Component[names.Length];
        _components[typeof(T)] = components;

        Component[] founds = GetComponentsInChildren<T>(true);
        foreach (var found in founds)
        {
            if (nameMap.TryGetValue(found.name, out var idx))
                components[idx] = found;
        }
    }
    public T Get<T>(int idx) where T : Component
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
        if (text != null)      
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
    public Slider GetSlider(int idx)
    {
        return Get<Slider>(idx);
    }
    public Image GetImage(int idx)
    {
        return Get<Image>(idx);
    }
    public TextMeshProUGUI GetTmp(int idx)
    {
        return Get<TextMeshProUGUI>(idx);
    }
    public TMP_InputField GetTMPInputField(int idx)
    {
        return Get<TMP_InputField>(idx);
    }
    public Transform GetTransform(int idx)
    {
        return Get<Transform>(idx);
    }
}
