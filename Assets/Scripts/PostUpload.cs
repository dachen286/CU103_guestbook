using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;

public class PostUpload : MonoBehaviour {

    public InputField autherField, titleField, contentField;
    public Text imageNameLbl;
    public RawImage previewImage;
    UnityWebRequest loadRequest;
    public int parentID;

    public UnityEvent OnPostUploadComplete = new UnityEvent();

    public void OnPickImageClick() {
        string imagePath = "";
#if UNITY_EDITOR
        imagePath=UnityEditor.EditorUtility.OpenFilePanelWithFilters("選擇圖片","",new string[] { "圖片檔案","png,jpg,jpeg"});
#elif UNITY_ANDROID
        Android,javaOpenImagePicker();
#endif 

        if (imagePath != "") {
            StartCoroutine(LoadLacalImage(imagePath));
        }

    }

    public void OnSendClick() {
        StartCoroutine(PostUploadHandler());
    }

    IEnumerator LoadLacalImage(string imagePath) {
        var request= UnityWebRequestTexture.GetTexture("file:///" + imagePath);
        yield return request.SendWebRequest();

        if (request.error != null) {
            Debug.LogWarning(request.error);
            yield break;
        }

        loadRequest = request;
        imageNameLbl.text = System.IO.Path.GetFileName(imagePath);
        previewImage.texture = DownloadHandlerTexture.GetContent(request);
    }

    IEnumerator PostUploadHandler() {
        string url = "https://guest-book-penda286.c9users.io/api.php";
        url += "?m=writePost";
        WWWForm form = new WWWForm();
        form.AddField("author", autherField.text);
        form.AddField("title", titleField.text);
        form.AddField("content", contentField.text);
        form.AddField("parent",parentID);

        if (loadRequest != null) {
            form.AddBinaryData("image",
                                loadRequest.downloadHandler.data,
                                System.IO.Path.GetFileName(loadRequest.url));
        }

        using (var request =  UnityWebRequest.Post(url,form)) {
            yield return request.SendWebRequest();

            if (request.isNetworkError) {
                Debug.LogWarning("網路發生錯誤");
                yield break;
            }

            if (request.downloadHandler.text == "success")
            {
                print("發文成功");
                OnPostUploadComplete.Invoke();
                Destroy(gameObject);
            }
            else {
                Debug.LogWarning($"發文失敗:{request.downloadHandler.text}");
            }
        }
    }

    public void OnClose() {
        Destroy(gameObject);
    }
}
