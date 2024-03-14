using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public static GameScene instance;

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }

    public override void Clear()
    {
        
    }
}