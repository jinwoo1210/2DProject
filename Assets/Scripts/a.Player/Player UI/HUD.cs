using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, level, Kill, Time, Hp }
    public InfoType type;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameScene.instance.exp;
                float maxExp = GameScene.instance.nextExp[Mathf.Min(GameScene.instance.level, GameScene.instance.nextExp.Length - 1)];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.level:
                myText.text = string.Format("Lv.{0:F0}", GameScene.instance.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameScene.instance.kill);

                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2} : {1:D2}", min,sec);
                
                break;
            case InfoType.Hp:
                float curHp = GameScene.instance.hp;
                float maxHp = GameScene.instance.maxHp;
                mySlider.value = curHp / maxHp;
                break;
        }
    }
}
