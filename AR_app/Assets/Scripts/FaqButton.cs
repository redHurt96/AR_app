using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FaqButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _tutorial;

    [Space]
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _disactiveSprite;

    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_tutorial.activeSelf)
        {
            _image.sprite = _activeSprite;
            _tutorial.SetActive(    true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_tutorial.activeSelf)
        {
            _image.sprite = _disactiveSprite;
            _tutorial.SetActive(false);
        }
    }
}
