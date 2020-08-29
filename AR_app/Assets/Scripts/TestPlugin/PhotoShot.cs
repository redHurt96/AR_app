using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoShot : MonoBehaviour
{
    [SerializeField] private CaptureAndSave _captureAndSave;
    [SerializeField] private Button _captureButton;

    private void Awake()
    {
        _captureButton.onClick.AddListener(() =>
        {
            _captureAndSave.CaptureAndSaveToAlbum();
        });
    }
}