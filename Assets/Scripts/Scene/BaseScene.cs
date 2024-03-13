using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public abstract IEnumerator LoadingRoutine();

    private void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {

    }


    public abstract void Clear();
}