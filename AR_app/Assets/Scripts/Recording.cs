using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Recorder;
//using UnityEditor.Recorder.Input;
using UnityEngine;
using UnityEngine.UI;

public class Recording : MonoBehaviour
{
    //[SerializeField] private Button _button;
    //[SerializeField] private Sprite _mainSprite;
    //[SerializeField] private Sprite _recordedSprite;

    //private RecorderController _recorderController;
    //private bool _isRecording = false;

    //private void Start()
    //{
    //    _button.onClick.AddListener(() =>
    //    {
    //        if (!_isRecording)
    //        {
    //            StartRecording();
    //            _button.image.sprite = _recordedSprite;
    //            _isRecording = true;
    //        }
    //        else
    //        {
    //            StopRecording();
    //            _button.image.sprite = _mainSprite;
    //            _isRecording = false;
    //        }
    //    });
    //}


    //public void StopRecording()
    //{
    //    _recorderController.StopRecording();
    //}

    //public void StartRecording()
    //{
    //    var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
    //    _recorderController = new RecorderController(controllerSettings);

    //    var videoRecorder = ScriptableObject.CreateInstance<MovieRecorderSettings>();
    //    videoRecorder.name = "My Video Recorder";
    //    videoRecorder.Enabled = true;
    //    //videoRecorder.VideoBitRateMode = VideoBitrateMode.High;

    //    videoRecorder.ImageInputSettings = new GameViewInputSettings
    //    {
    //        OutputWidth = 720,
    //        OutputHeight = 1280
    //    };

    //    videoRecorder.AudioInputSettings.PreserveAudio = true;

    //    string outputFile = Application.persistentDataPath + $"/ar_clip_{DateTime.Now.ToString("ddMMyyyy_hhmmss")}";
    //    videoRecorder.OutputFile = outputFile;
    //    Debug.LogWarning(outputFile);

    //    controllerSettings.AddRecorderSettings(videoRecorder);
    //    controllerSettings.SetRecordModeToManual();
    //    controllerSettings.FrameRate = 30;

    //    RecorderOptions.VerboseMode = true;
    //    _recorderController.PrepareRecording();
    //    _recorderController.StartRecording();
    //}
}
