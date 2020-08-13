using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class PhotoShot : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Button _snapshotButton; 

    [SerializeField] private string _savingPath;

    private WebCamTexture webCamTexture;

    void Start()
    {
        webCamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
        webCamTexture.Play();

        _snapshotButton.onClick.AddListener(Snapshot);
    }

    private void OnDestroy()
    {
        _snapshotButton.onClick.RemoveListener(Snapshot);
    }

    private void Snapshot() => StartCoroutine(TakePhoto());

    IEnumerator TakePhoto()  // Start this Coroutine on some button click
    {
        _canvas.SetActive(false);
        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        byte[] bytes = photo.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(_savingPath,$"ar_app{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.png"), bytes);

        _canvas.SetActive(true);
    }
}