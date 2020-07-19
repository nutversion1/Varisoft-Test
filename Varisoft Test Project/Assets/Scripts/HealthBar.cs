using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Transform bar;

    void Awake()
    {
        bar = transform.Find("Bar");
   
    }

    public void SetSize(float scale)
    {
        bar.localScale = new Vector3(Mathf.Clamp(scale, 0f, 1f), 1f);
    }
}
