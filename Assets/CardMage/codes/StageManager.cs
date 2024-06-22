using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    // Start is called before the first frame update

   

    public GameObject[] StageIcons;
    public RectTransform[] stageIconsRect;
    public TMP_Text stageLeveltext;
    public TMP_Text stageNametext;
    public TMP_Text stageAchtext;
    public StageIcons[] stageiconss;

    public int stageLevel_Big;
    public int stageLevel_Small;

   



    private void Awake()
    {
        stageLevel_Big = 1;
        stageLevel_Small = 1;

        for (int index = 0; index < 10; index++)
        {
            stageiconss[index] = StageIcons[index].GetComponent<StageIcons>();
            StageIcons[index].gameObject.SetActive(false);
        }

        // GameManager.instanse.GameStageLevel = 1;
    }

    public void IconSetting()
    {
        switch (stageLevel_Big)
        {
            case 1:
      
                StageIcons[0].gameObject.SetActive(true);
                stageiconss[0].IconSetting(1);
                StageIcons[1].gameObject.SetActive(true);
                stageiconss[1].IconSetting(0);
                stageIconsRect[0].anchoredPosition = new Vector2(-40, 780);
                stageIconsRect[1].anchoredPosition = new Vector2(60, 780);
             
                break;
        }
    }
    public void SmallStageSetting(int StageLevel_B, int StageLevel_S, int StageIndex)
    {

    }
    public void StageNameTextSetting(int IconNumbering)
    {
        switch (IconNumbering)
        {
            case 0:
                stageNametext.text = "[전투]";
                break;
            case 1:
                stageNametext.text = "[카드 추ㄱ]";
                break;

        }
        
     }
}



