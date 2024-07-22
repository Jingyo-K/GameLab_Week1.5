using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    public string SceneToLoad;

    private bool _keyClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        _keyClicked = false;
        RankManager.Instance.ReadRankData();
        RankManager.Instance.UpdateRankUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !_keyClicked)
        {
            FadeManager.Instance.ChangeScene(SceneToLoad);
            _keyClicked = true;
        }
    }
}
