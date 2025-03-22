using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CastleValleyGlobalShaderControl : MonoBehaviour
{
    [SerializeField] float globalSmooth;
    [SerializeField] float globalContrast;
    [SerializeField] Color globalTint;


    void Start()
    {
        setShaderValues();
    }

#if UNITY_EDITOR

void Update(){

setShaderValues();

}
#endif

    public void setShaderValues()
    {
        Shader.SetGlobalFloat("GlobalSmooth", globalSmooth);
        Shader.SetGlobalFloat("GlobalContrast", globalContrast);
        Shader.SetGlobalColor("GlobalTint", globalTint);
    }


}
