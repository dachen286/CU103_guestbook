using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Post : MonoBehaviour {

    public Text headerLable, contentLable, timeStampLabel;
    public DataStruct data { get; private set; }     //文章的JSON資料
    public Button imageButton;

	// Use this for initialization
	void Start () {
		
	}
    [SerializeField]
    ImageView imageViewPrefab;
    [SerializeField]
    PostUpload postUploadPrefab;


    public void SetBy(DataStruct data) {
        this.data = data;
        if (string.IsNullOrEmpty(data.title))
            data.title = "無題";
        if (string.IsNullOrEmpty(data.author))
            data.author = "匿名使用者";

        headerLable.text = $"{data.title} - {data.author} <color=#00F>#{data.id}</color>";
        contentLable.text = data.content;
        timeStampLabel.text = data.created_at;

        imageButton.gameObject.SetActive(data.img_name != "");

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
    }

    public void OnImageClick() {
        var imageView = Instantiate(imageViewPrefab);
        imageView.imageName = data.img_name;
    }

    public void OnReplayClick() {
        var postUpload = Instantiate(postUploadPrefab);
        postUpload.parentID= data.id;
        postUpload.OnPostUploadComplete.AddListener(() =>
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex));

        postUpload.titleField.text = "Re: " + data.title;

    }
}
