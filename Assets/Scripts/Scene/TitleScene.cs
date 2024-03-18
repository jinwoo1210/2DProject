using System.Collections;
using UnityEngine;

public class TitleScene : BaseScene
{
    private bool isbutton;

    public void GameSceneLoad()
    {
        Manager.Scene.LoadScene("GameScene");
    }

    private void Start()
    {
        SoundManager.Instance.PlaySfx(SoundManager.Sfx.TitleIntro);
    }

    private void Update()
    {
        if (Input.anyKey && !isbutton)
        {
            SoundManager.Instance.PlaySfx(SoundManager.Sfx.Button);
            GameSceneLoad();
            isbutton = true;
        }
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
