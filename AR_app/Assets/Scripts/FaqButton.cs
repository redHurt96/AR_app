using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FaqButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _tutorial;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_tutorial.activeSelf)
            _tutorial.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_tutorial.activeSelf)
            _tutorial.SetActive(false);
    }
}
