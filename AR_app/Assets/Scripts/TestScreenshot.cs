using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestScreenshot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(TakeScrShot());
        }
    }
    private IEnumerator TakeScrShot()
    {
        yield return new WaitForEndOfFrame();


        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        var bytes = tex.EncodeToPNG();
        Destroy(tex);

        File.WriteAllBytes(Application.persistentDataPath + "/screenshot.png", bytes);
        Debug.Log(Application.persistentDataPath + "/screenshot.png");

    }
}
