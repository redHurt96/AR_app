using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class VideoCaptureButton : MonoBehaviour
{
    [SerializeField] private Sprite _mainSprite;
    [SerializeField] private Sprite _recordedSprite;

    [Range(0f, 1f)]
    [SerializeField] private float _deselectedButtonAlpha = .5f;

    private Button _button;
    private bool _isRecording = false;
    private int _recorderAviHandler;

    private void Start()
    {
        _button = GetComponent<Button>();
        AttachListeners();
    }

    private void OnApplicationQuit() => DetachListeners();

    private void AttachListeners() =>
        _button.onClick.AddListener(OnButtonClick);

    private void DetachListeners() =>
        _button?.onClick.RemoveListener(OnButtonClick);

    private void OnButtonClick()
    {
        if (!_isRecording)
        {
            Debug.Log("Start capture");
            _isRecording = true;
            _button.image.sprite = _recordedSprite;
        }
        else
        {
            Debug.Log("Stop capture");
            _isRecording = false;
            _button.image.sprite = _mainSprite;
        }
    }

    private void SetAlpha(bool buttonIsActive)
    {
        Color color = _button.image.color;

        if (buttonIsActive && Mathf.Abs(_button.image.color.a - 1) >= .1f)
            _button.image.color = new Color(color.r, color.g, color.b, 1f);
        else if (buttonIsActive && Mathf.Abs(_button.image.color.a - 1) >= .1f)
            _button.image.color = new Color(color.r, color.g, color.b, _deselectedButtonAlpha);
    }
}