using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImageView : MonoBehaviour {
       
    public RawImage image;
    public Slider progressBar;
    public string imageName;
    UnityWebRequest imageRequest; 

    // Use this for initialization
    void Start() {
        if (string.IsNullOrEmpty(imageName)) {
            Destroy(gameObject);
            return;
        }

        string url = "https://guest-book-penda286.c9users.io/img/" + imageName;
        imageRequest = UnityWebRequestTexture.GetTexture(url);
        imageRequest.SendWebRequest();

    }
    	
	// Update is called once per frame
	void Update () {
        if (imageRequest == null) return;
        //設置下載進度
        progressBar.value=imageRequest.downloadProgress;

        if (imageRequest.isDone) {
            image.texture = DownloadHandlerTexture.GetContent(imageRequest);
            image.gameObject.SetActive(true);
            progressBar.gameObject.SetActive(false);
            //設定圖片大小符合螢幕
            Vector2 imgSize = new Vector2(image.texture.width, image.texture.height);
            Vector2 scnSize = new Vector2(Screen.width, Screen.height);

            float rateW = imgSize.x / scnSize.x;
            float rateH = imgSize.y / scnSize.y;
            float rate = Mathf.Max(rateW, rateH);

            if (rate > 1f)
            {
                image.rectTransform.sizeDelta = imgSize / rate;
            }
            else {
                image.SetNativeSize();
            }


           

            imageRequest.Dispose();
            imageRequest = null;
        }

    }


    public void OnClose() {
        Destroy(gameObject);

    }
}
