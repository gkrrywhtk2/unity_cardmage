using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] cardselects;
    private void Awake()
    {
        Debug.Log("isON?");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        show();

       
    }

    public void show()
    {
        foreach (GameObject cardselect in cardselects)//  초기화 입니다.
        {                                            
            cardselect.gameObject.SetActive(false);
        }

        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, GameManager.instanse.CardOn.Count);
            ran[1] = Random.Range(0, GameManager.instanse.CardOn.Count);
            ran[2] = Random.Range(0, GameManager.instanse.CardOn.Count);

            if (ran[0] != ran[1] && ran[0] != ran[2] && ran[1] != ran[2])
                break;
        }
        for (int index = 0; index < ran.Length; index++)
        {
            cardselects[index].gameObject.SetActive(true);
            cardselects[index].GetComponent<CardSelectButton>().Show(ran[index]);

        }
    }
}
