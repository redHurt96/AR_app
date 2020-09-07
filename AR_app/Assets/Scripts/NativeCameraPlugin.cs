using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NativeCameraPlugin : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _mainSprite;
    [SerializeField] private Sprite _recordedSprite;

    private bool _isRecording = false;

    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            if (!_isRecording)
            {
                NativeCamera.RecordVideo(null);
                _button.image.sprite = _recordedSprite;
                _isRecording = true;
            }
            else
            {
                _button.image.sprite = _mainSprite;
                _isRecording = false;
            }
        });
    }
}
