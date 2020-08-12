using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TutorialButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();    
    }

    public void AddListener(UnityAction action)
    {
        button?.onClick.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        button?.onClick.RemoveListener(action);
    }
}
