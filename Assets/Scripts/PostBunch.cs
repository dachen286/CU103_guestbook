using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostBunch : MonoBehaviour {

    [SerializeField]
    Post postPrefab, replyPrefab;

    public void Setup(DataStruct parent, List<DataStruct> replys) {
        var post = Instantiate(postPrefab, transform);
        post.SetBy(parent);

        foreach (var data in replys) {
            var reply = Instantiate(replyPrefab, transform);
            reply.SetBy(data);
        }

    }

}
