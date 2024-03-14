using System.Collections;
using UnityEngine;

public class TitleScene : BaseScene
{
    private bool isbutton;

    public void GameSceneLoad()
    {
        Manager.Scene.LoadScene("GameScene");
    }

    private void Update()
    {
        if (Input.anyKey && !isbutton)
        {
            GameSceneLoad();
            isbutton = true;
        }
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
