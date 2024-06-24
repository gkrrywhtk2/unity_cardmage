using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSelectButton : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer cardIcon;
    public SpriteRenderer manacostIcon;
    public List<Sprite> CardSprites;
    public List<Sprite> manacosts;
    public TMP_Text cardnametext;
    public TMP_Text cardtext_exp;

    private void OnEnable()
    {
        
    }

    public void Show(int cardid)
    {
        switch (cardid)
        {
            case 0:
                manacostIcon.sprite = manacosts[1];
                cardnametext.text = "빠른 걸음";
                cardtext_exp.text = "카드를 1~3장 뽑습니다.";

                break;
            case 1:
                manacostIcon.sprite = manacosts[2];
                cardnametext.text = "화염구";
                cardtext_exp.text = "거대한 화염구체를 날려 경로상의 대상에게 피해를 입힙니다.";
                break;
            case 2:
                manacostIcon.sprite = manacosts[0];
                cardnametext.text = "마나 순환";
                cardtext_exp.text = "잠시동안 마나 회복량이 크게 늘어납니다.";
                break;
            case 3:
                manacostIcon.sprite = manacosts[3];
                cardnametext.text = "점화";
                cardtext_exp.text = "가장 가까운 대상에게 큰 피해를 입힙니다.";
                break;


        }
        cardIcon.sprite = CardSprites[cardid];
    }
}
