using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;


public class UiCard : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer cardIcon;
    public List<Sprite> CardSprites;
    public SpriteRenderer cardCostImage;
    public List<Sprite> cardcostsprites;
    public TMP_Text cardnametext;
    public TMP_Text cardtext_exp;


    public void show(SpriteRenderer Icon, TMP_Text name, TMP_Text desc, int cost)
    {
        cardIcon.sprite = Icon.sprite;
        cardnametext.text = name.text;
        cardtext_exp.text = desc.text;
        cardCostImage.sprite = cardcostsprites[cost];
     
    }

}
