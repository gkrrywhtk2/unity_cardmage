using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageIcons : MonoBehaviour
{
    public SpriteRenderer Icon;
    public Sprite[] Icons;
    public bool IsCardplus;



    public void IconSetting(int IconNumber)
    {
        Icon.sprite = Icons[IconNumber];
        
    }


   

    void SecondSetting()
    {
        if(IsCardplus == true)
        {
            Icon.sprite = Icons[0];
        }
    }

    void LightON()
    {
        Icon.GetComponent<Color>();
        Icon.color = new Color(33/255, 168/225, 43/225, 1);

    }


}
