using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_skybox : MonoBehaviour {

    public float skySpeed = 0.05f;

	void Update () {

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skySpeed);
		
	}
}
