using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;
using UnityEngine.UI;
using System.Linq;


public class PostList : MonoBehaviour {

    //public Post postPrefab;
    public PostBunch bunchPrefabs;

	// Use this for initialization
	void Start () {
        StartCoroutine(GetPostList());
	}

    IEnumerator GetPostList() {
        string url = "https://guest-book-penda286.c9users.io/api.php";
        url += "?m=getPosts";

        using (var request = Post(url, "")) {
            yield return request.SendWebRequest();

            if (request.error != null) {
                Debug.LogWarning(request.error);
                yield break;
            }

            var list=JsonUtility.FromJson<ListTemplate>(request.downloadHandler.text);

            Transform content = GetComponent<ScrollRect>().content;

            var groups = from postData in list.datas group postData by postData.parent;

            var parents = groups.Single(group => group.Key==0);

            foreach (DataStruct parent in parents) {
                PostBunch bunch = Instantiate(bunchPrefabs, content);

                var replys = groups.SingleOrDefault(g => g.Key == parent.id)?.ToList() ?? new List<DataStruct>();
                replys.Reverse();

                bunch.Setup(parent, replys);
            }

            //foreach (DataStruct data in list.datas) {
            //    Post post = Instantiate(postPrefab, content);
            //    post.SetBy(data);

            //    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)post.transform);
            //}
        }
    }
}
