using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour {

    public Image HealthForeground;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisplayHealth(float percentage)
    {
        float clampedPercentage = Mathf.Clamp(percentage, 0f, 1f);
        HealthForeground.rectTransform.localScale = new Vector3(clampedPercentage, HealthForeground.rectTransform.localScale.y, HealthForeground.rectTransform.localScale.z);
    }
}
