using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    public Text nowmanatext;

    [Header("CheatUi(Test)")]
    public GameObject firstdrawB;
    public GameObject nomaldrawB;

    [Header("cardui")]
    public Text cardname;
    public Text cardexplation;
    public Image carduipanel;
    

    public void CheatOn()
    {
        firstdrawB.gameObject.SetActive(true);
        nomaldrawB.gameObject.SetActive(true);
    }
    public void CheatOff()
    {
        firstdrawB.gameObject.SetActive(false);
        nomaldrawB.gameObject.SetActive(false);
    }

    private void Awake()
    {
        carduipanel.GetComponent<Image>();
        nowmanatext.GetComponent<Text>();
       
    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.instanse.player.mana < 9.9f)
        {
            nowmanatext.text = GameManager.instanse.player.mana.ToString("F1");
        }
        else
        {
            nowmanatext.text = GameManager.instanse.player.mana.ToString("F0");
        }
        
    }
    public void CardUiSet(int cardid)
    {
        Color tempcolor = carduipanel.color;
        tempcolor.a = 0.1f;
        carduipanel.color = tempcolor;
        switch (cardid)
        {
            case 0:
                cardname.text = "빠른 걸음";
                cardexplation.text = "카드를 1~3장 뽑습니다.";
                break;
            case 1:
                cardname.text = "화염구";
                cardexplation.text = "거대한 화염구체를 날려 경로상의 대상에게 피해를 입힙니다.";
                break;
            case 2:
                cardname.text = "마나 순환";
                cardexplation.text = "잠시동안 마나 회복량이 크게 늘어납니다.";
                break;
            case 3:
                cardname.text = "점화";
                cardexplation.text = "가장 가까운 대상에게 큰 피해를 입힙니다.";
                break;

        }
    }
    public void CardUiReSet()
    {
        Color tempcolor = carduipanel.color;
        tempcolor.a = 0f;
        carduipanel.color = tempcolor;
        cardname.text = "";
        cardexplation.text = "";
    }
}
