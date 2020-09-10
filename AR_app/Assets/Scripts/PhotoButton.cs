using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PhotoButton : SimpleSingleton<PhotoButton>
{
    [SerializeField] private GameObject[] _uselessForPhoto;
    [SerializeField] private Blink _blink;
    [SerializeField] private Button _button;
    [SerializeField] private string _possiblePathToGallery;

    [Header("Debug")]
    [SerializeField] private Text _pathText;

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

    private const string MediaStoreImagesMediaClass = "android.provider.MediaStore$Images$Media";

    private void Awake()
    {
        _button.onClick.AddListener(() => StartCoroutine(TakePicture()));
    }

    private void OnDestroy() =>
        _button.onClick.RemoveAllListeners();

    public void HideUselessObjects() =>
        SetUselessObjectsVisibility(false);

    public void ShowUselessObjects() =>
        SetUselessObjectsVisibility(true);

    [ContextMenu("Picture")]
    private void Screenshot() => StartCoroutine(TakePicture());

    private void SetUselessObjectsVisibility(bool state)
    {
        foreach (var item in _uselessForPhoto)
            item.SetActive(state);
    }

    private IEnumerator TakePicture()
    {
        HideUselessObjects();
        yield return new WaitForEndOfFrame();

        string fileName = $"Screenshot_{DateTime.Now.ToString("yy_MM_dd_hh_mm_ss")}.png";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();
        //Convert to png
        byte[] imageBytes = screenImage.EncodeToPNG();


        //Save image to file
        //File.WriteAllBytes(path, imageBytes);

        NativeGallery.SaveImageToGallery(imageBytes, Application.productName, fileName);
        _pathText.text = path;

        ShowUselessObjects();
        //_blink.CreateBlink(.5f);
    }

    //from sfs asset

    //private static string SaveImageToGallery(Texture2D picture, string title)
    //{
    //    using (var mediaClass = new AndroidJavaClass(MediaStoreImagesMediaClass))
    //    {
    //        using (var contentResolver = Activity.Call<AndroidJavaObject>("getContentResolver"))
    //        {
    //            var image = Texture2DToAndroidBitmap(picture);
    //            var imageUrl = mediaClass.CallStatic<string>("insertImage", contentResolver, image, title);
                
    //            return imageUrl;
    //        }
    //    }
    //}

    //public static AndroidJavaObject Texture2DToAndroidBitmap(Texture2D texture2D)
    //{
    //    byte[] encoded = texture2D.EncodeToPNG();
    //    using (var bf = new AndroidJavaClass("android.graphics.BitmapFactory"))
    //    {
    //        return bf.CallStatic<AndroidJavaObject>("decodeByteArray", encoded, 0, encoded.Length);
    //    }
    //}
}