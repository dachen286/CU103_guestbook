using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPost : MonoBehaviour {

    [SerializeField]
    PostUpload postUploadPrefab;

    public void OnClick() {
       var postUpload= Instantiate(postUploadPrefab);
        //postUpload.parentID = 0;
        postUpload.OnPostUploadComplete.AddListener(() =>
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex));

    }

}
