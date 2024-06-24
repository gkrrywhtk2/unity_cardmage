using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instanse;
    public Player player;
    public PoolManager pool;
    public StageManager stagemanager;
    public HUD hud;
    public UiCard uicard;
    public bool isPlay;

    [Header("Can Use Card")]
    public List<bool> CardOn;
    public int[] CardLevels;

    [Header("GameLevel")]
    public int GameStageLevel;




    private void Awake()
    {
        for(int index = 0; index < CardLevels.Length; index++)
        {
            CardLevels[index] = 1;
        }

       
        Application.targetFrameRate = 60;
        instanse = this;
        isPlay = true;
       
    }
    private void Start()
    {
        SetResolution(); // 초기에 게임 해상도 고정
    }

    /* 해상도 설정하는 함수 */
    public void SetResolution()
    {
        int setWidth = 1080; // 사용자 설정 너비
        int setHeight = 1920; // 사용자 설정 높이

     

        Screen.SetResolution(setWidth, setHeight, true); // SetResolution 함수 제대로 사용하기

       
    }
}
