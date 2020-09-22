using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeSliderController : MonoBehaviour
{
    public Transform[] resizableObjects;
    

    public void ChangeSize(float newSize)
    {
        for (int i = 0; i < resizableObjects.Length; i++)
        {
            resizableObjects[i].localScale = new Vector3(newSize, newSize, newSize);
        }
    }    
}
