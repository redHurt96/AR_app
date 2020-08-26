using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class PhotoShot : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Button _snapshotButton;
    [SerializeField] private Camera _camera;

    [SerializeField] private string _savingPath;

    private WebCamTexture webCamTexture;

    void Start()
    {
        _savingPath = Application.persistentDataPath;
        _camera = Camera.main;

        //webCamTexture = new WebCamTexture();
        //GetComponent<Renderer>().material.mainTexture = webCamTexture;
        //webCamTexture.Play();

        _snapshotButton.onClick.AddListener(Snapshot);
    }

    private void OnDestroy()
    {
        _snapshotButton.onClick.RemoveListener(Snapshot);
    }

    private void Snapshot() => StartCoroutine(TakePhoto());

    IEnumerator TakePhoto()
    {
        Debug.Log("take photo");
        yield break;
        #region Old version
        //_canvas.SetActive(false);
        //yield return new WaitForEndOfFrame();

        //Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        //photo.SetPixels(webCamTexture.GetPixels());
        //photo.Apply();

        ////Simple saving to persistent data path
        //byte[] bytes = photo.EncodeToPNG();
        //File.WriteAllBytes(Path.Combine(_savingPath, $"ar_app{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.png"), bytes); 
        #endregion
        



       // SaveImageToGallery(photo, $"ar_app{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.png", $"some description");

        _canvas.SetActive(true);
    }

    #region Static
    private const string MediaStoreImagesMediaClass = "android.provider.MediaStore$Images$Media";

    public static AndroidJavaObject Activity
    {
        get
        {
            if (_activity == null)
            {
                var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                _activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
            return _activity;
        }
    }

    private static AndroidJavaObject _activity;

    public static string SaveImageToGallery(Texture2D texture2D, string title, string description)
    {
        using (var mediaClass = new AndroidJavaClass(MediaStoreImagesMediaClass))
        {
            using (var cr = Activity.Call<AndroidJavaObject>("getContentResolver"))
            {
                var image = Texture2DToAndroidBitmap(texture2D);
                var imageUrl = mediaClass.CallStatic<string>("insertImage", cr, image, title, description);
                return imageUrl;
            }
        }
    }

    public static AndroidJavaObject Texture2DToAndroidBitmap(Texture2D texture2D)
    {
        byte[] encoded = texture2D.EncodeToPNG();
        using (var bf = new AndroidJavaClass("android.graphics.BitmapFactory"))
        {
            return bf.CallStatic<AndroidJavaObject>("decodeByteArray", encoded, 0, encoded.Length);
        }
    }
    #endregion
}