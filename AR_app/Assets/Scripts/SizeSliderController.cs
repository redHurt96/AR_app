using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeSliderController : MonoBehaviour
{
    public Transform[] resizableObjects;


    private void Start()
    {
        float startSize = GetComponent<Slider>().value;
        for (int i = 0; i < resizableObjects.Length; i++)
        {
            resizableObjects[i].localScale = new Vector3(startSize, startSize, startSize);
        }
    }
    public void ChangeSize(float newSize)
    {
        for (int i = 0; i < resizableObjects.Length; i++)
        {
            resizableObjects[i].localScale = new Vector3(newSize, newSize, newSize);
        }
    }    
}
