/* 
*   NatCorder
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCorder.Examples
{

    using UnityEngine;
    using System.Collections;
    using Clocks;
    using Inputs;
    using System;
#if UNITY_iOS
    using NatSuite.Sharing;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
using Permission = UnityEngine.Android.Permission;
#endif

    public class ReplayCam : MonoBehaviour
    {

        [Header("Recording")]
        public int videoWidth = 1280;
        public int videoHeight = 720;
        public bool recordMicrophone;

        private IMediaRecorder videoRecorder;
        private CameraInput cameraInput;
        public GameObject watermark;
        // private AudioInput audioInput;
        private AudioSource microphoneSource;

        private void Start()
        {
            // Start microphone
            // microphoneSource = gameObject.AddComponent<AudioSource>();
            //microphoneSource.mute =
            //microphoneSource.loop = true;
            //microphoneSource.bypassEffects =
            // microphoneSource.bypassListenerEffects = false;
            // microphoneSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
            //yield return new WaitUntil(() => Microphone.GetPosition(null) > 0);
            // microphoneSource.Play();
        }

        private void OnDestroy()
        {
            // Stop microphone
            // microphoneSource.Stop();
            //Microphone.End(null);
        }

        public void StartRecording()
        {
            //watermark.SetActive(true);

            // Start recording
            var frameRate = 30;
            var sampleRate = 0;
            var channelCount = 0;
            var recordingClock = new RealtimeClock();
            videoRecorder = new MP4Recorder(
                Camera.main.pixelWidth,
                Camera.main.pixelHeight,
                frameRate,
                sampleRate,
                channelCount,
                recordingPath =>
                {
                    Debug.Log($"Saved recording to: {recordingPath}");
                    var prefix = Application.platform == RuntimePlatform.IPhonePlayer ? "file://" : "";



#if UNITY_iOS
                    Handheld.PlayFullScreenMovie($"{prefix}{recordingPath}");

                    var payload = new SavePayload("Dirol Minions"); // album name here
                    payload.AddMedia($"{recordingPath}");
                    payload.Commit();
#endif

#if !UNITY_iOS



                    StartCoroutine(TakeScreenshotAndSave(recordingPath));


#endif
                    /*string fileName = string.Format("DirolMinions" + "_{0}.png", DateTime.Now.ToString("hhMMssfff"));
                    if(Application.platform == RuntimePlatform.IPhonePlayer)
                        NativeGallery.SaveVideoToGallery($"{prefix}{recordingPath}", "DirolMinions", fileName);
                    else
                        NativeGallery.SaveVideoToGallery($"{prefix}{recordingPath}", "Download", fileName);*/
                }
            );
            // Create recording inputs
            cameraInput = new CameraInput(videoRecorder, recordingClock, Camera.main);

            // Save the recording to the camera roll
            // We can save it to a specific album in the camera roll


            // audioInput = recordMicrophone ? new AudioInput(videoRecorder, recordingClock, microphoneSource, true) : null;
            // Unmute microphone
            // microphoneSource.mute = audioInput == null;
        }

        private IEnumerator TakeScreenshotAndSave(string _recordingPath)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if(!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                int maxTries = 3;
                do
                {
                    Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                    yield return new WaitForSeconds(1);
                    if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
                    {
                        //ToastHelper.ShowToast(OnirixI18N.Instance.Get("EXTERNAL_STORAGE_PERMISSION_DENIED"), true);
                        if(--maxTries == 0)
                        {
                            yield break;
                        }
                        else
                        {
                            yield return new WaitForSeconds(5);
                        }
                    }
                } while (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite));
            }
#endif


            yield return new WaitForEndOfFrame();
            Handheld.PlayFullScreenMovie(_recordingPath);

            string fileName = string.Format(Application.productName + "_{0}.mp4", DateTime.Now.ToString("hhMMssfff"));

            /*WWW www = new WWW(_recordingPath);
            yield return www;
            */
            NativeGallery.SaveVideoToGallery(_recordingPath, Application.productName, fileName);
            //Debug.Log("Permission result: " + NativeGallery.SaveVideoToGallery(_recordingPath, Application.productName, fileName));
            yield return new WaitForSeconds(1.0f);

        }

        public void StopRecording()
        {
            // Stop recording
            // audioInput?.Dispose();
            //watermark.SetActive(false);
            cameraInput.Dispose();
            videoRecorder.Dispose();
            // PreStartOnirix.Instance.GetVisorViewController.NavigationController.PopViewController();
            // Mute microphone
            //  microphoneSource.mute = true;
        }
    }
}