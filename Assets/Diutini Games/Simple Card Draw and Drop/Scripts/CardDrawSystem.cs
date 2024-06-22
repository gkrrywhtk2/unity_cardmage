using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using SimpleCardDrawAndSpread_HandCard;

// Hello. Thank you for purchasing Asset. In this set, the pulled cards are moved to the hand part and aligned at regular intervals.
// This script is not the correct answer, so please consider it as one of the implementation scripts. Thank you.

namespace SimpleCardDrawAndSpread_CardDrag
{
    public enum CardDragType
    {
        CardPos,
        MousePos,
    }
    

    public class CardDrawSystem : MonoBehaviour
    {
        

        //Variable that sets the region where the card is used.
        [Header("Card Use Ground")]
        public Transform CardUseGround;
        public float CardUseDistance;

        [Header("Card Draw")]
        public Transform CardCreatePos;
        public Transform CardHandPos;

        [Space(10)]
        public GameObject CardOb;
        public List<Sprite> CardSprites;
        public float CardDrawDelay;
        [HideInInspector] public List<GameObject> PlayerHandCardList;

        [Space(10)]
        public int FirstDrawCount;
        public int NomalDrawCount;

        [Header("Card Drag")]
        public CardDragType CardDragType;

        [Header("Card Spread")]
        public float HandCard_CardSpreadRange; //Must be entered as an integer.
        public float HandCard_EachCardSpreadDistance; //Must be entered as an integer.
        public float HandCard_EachCardSpreadLimitCount;

        [HideInInspector] public GameObject HandCard_CardSpreadOb;
        [HideInInspector] public GameObject HandCard_EachCardOb;

        [HideInInspector] public List<Vector3> HandCard_EachCardDistanceList;
        [HideInInspector] public List<float> HandCard_EachCardAngleList;

        [Header("Card Move Speed")]
        public float CardSpeed_Draw;
        public float CardSpeed_HandSpread;

        public CardData[] data;


        


        // Start is called before the first frame update
        void Start()
        {
           

            //The first time you start a game, you draw a card as many as the FirstDrawCount number.
            // StartCoroutine(PlayerCardDrawManager(FirstDrawCount));
        }

        public void StartDraw()
        {
            StartCoroutine(PlayerCardDrawManager(FirstDrawCount));
        }

        // Update is called once per frame
        //void Update()
        //{

        //}

        public void Button_CardDraw_Manager()
        {
            //Draw cards as many as the NomalDrawCount number by recalling a particular button or function.
            StartCoroutine(PlayerCardDrawManager(NomalDrawCount));
        }
        public void FastWalk(int CardPoint)
        {
            StartCoroutine(PlayerCardDrawManager(CardPoint));
        }

        public IEnumerator PlayerCardDrawManager(int CardCount)
        {

            int Rnum_CardSprite;
           
            for (int i = 0; i < CardCount; i++)
            {

                while (true)
                {
                    Rnum_CardSprite = Random.Range(0, CardSprites.Count);
                    if (GameManager.instanse.CardOn[Rnum_CardSprite] == true)
                        break;
                   
                }
                //Randomly determine the image for the card.
              
                

                //Create and set card objects.
                GameObject newOb = Instantiate(CardOb, CardCreatePos.position, Quaternion.identity);
                    newOb.transform.SetParent(CardHandPos, true);

                    //Change the central image of the card that you created using Rnum_CardSprite, which contains arbitrary numbers.
                    HandCardSystem input_HandCardSystem = newOb.GetComponent<HandCardSystem>();
                //input_HandCardSystem.CardIcon_Sprite.sprite = CardSprites[Rnum_CardSprite];
               // input_HandCardSystem.CardIcon_Sprite.sprite = data[Rnum_CardSprite].cardicon;




                    input_HandCardSystem.CardSetting(data[Rnum_CardSprite], GameManager.instanse.CardLevels[Rnum_CardSprite]);
             //   input_HandCardSystem.cardId = Rnum_CardSprite;
                  //  input_HandCardSystem.FirstCostSetting();
                
                    //Save to list for use in card sorting.
                    PlayerHandCardList.Add(newOb);

                    //Draw card layer setting.
                    CardLayerCheckManager();

                    //Set the numerical value to expand the card you hold in your hand first.
                    CardSpreadSettingManager();

                    //Delays card objects so that they do not overlap.
                    yield return new WaitForSeconds(CardDrawDelay);
                    
                
                
            }

        }


            //
        

        public void CardLayerCheckManager()
        {
            //First, delete an empty object in the list.
            for (int checkcount = 0; checkcount < PlayerHandCardList.Count;)
            {
                if (PlayerHandCardList[checkcount] == null)
                {
                    PlayerHandCardList.RemoveAt(checkcount);

                    checkcount = 0;
                }
                else
                {
                    checkcount++;
                }
            }

            //Card layer setting.
            for (int i = 0; i < PlayerHandCardList.Count; i++)
            {
                HandCardSystem input_HandCardSystem = PlayerHandCardList[i].GetComponent<HandCardSystem>();

                for (int i2 = 0; i2 < input_HandCardSystem.CardLayers.Length; i2++)
                {
                    input_HandCardSystem.CardLayers[i2].sortingOrder = i;
                    input_HandCardSystem.GetComponent<SortingGroup>().sortingOrder = i; //Avoid possible problems with masks or multiple images.
                }

            }


        }

        public void CardSpreadSettingManager()
        {
            //The way to expand the card in this script is to find the angle using two guide objects in the CardHandPos position.

            //Set to the initial value.
            HandCard_CardSpreadOb.transform.position = new Vector3(CardHandPos.position.x, CardHandPos.position.y, CardHandPos.position.z);
            HandCard_CardSpreadOb.transform.rotation = Quaternion.Euler(0, 0, 0);

            //Check if it's an integer or not.
            if (CardHandPos.position.y >= 0)//integer
            {
                HandCard_CardSpreadOb.transform.position = new Vector3(HandCard_CardSpreadOb.transform.position.x, -(CardHandPos.position.y + HandCard_CardSpreadRange), HandCard_CardSpreadOb.transform.position.z);

            }
            else
            {
                HandCard_CardSpreadOb.transform.position = new Vector3(HandCard_CardSpreadOb.transform.position.x, -(System.Math.Abs(CardHandPos.position.y) + HandCard_CardSpreadRange), HandCard_CardSpreadOb.transform.position.z);

            }

            //Move by numeric value
            HandCard_EachCardOb.transform.localPosition = new Vector3(0, HandCard_CardSpreadRange, 0);



            //Initial setting of numerical values to unfold the card.
            float SpreadPoint_Start = 0;
            float SpreadPoint_End = 0;

            //If the number of cards exceeds a certain number, the value is obtained to set it not to widen left and right.
            if (PlayerHandCardList.Count > HandCard_EachCardSpreadLimitCount)
            {
                float num1 = PlayerHandCardList.Count - HandCard_EachCardSpreadLimitCount; //Finds the number of cards that exceed the set value.
                float num2 = num1 / PlayerHandCardList.Count * 100; //Find out what percentage of the total number of cards you have with num1.
                float num3 = HandCard_EachCardSpreadDistance * (1 - num2 / 100); //Finds how much percentage of the num2 value should be reduced from the existing value at which the card is deployed.

                SpreadPoint_Start = (num3 * (PlayerHandCardList.Count - 1)) / 2;
                SpreadPoint_End = num3;
            }
            else
            {
                //Find the angle that will unfold as many cards as you draw.
                //SpreadPoint_Start is the left end and SpreadPoint_End is the right end angle.
                SpreadPoint_Start = (HandCard_EachCardSpreadDistance * (PlayerHandCardList.Count - 1)) / 2;
                SpreadPoint_End = HandCard_EachCardSpreadDistance;
            }

            //Initializes the position and angle values for each card.
            HandCard_EachCardDistanceList.Clear();
            HandCard_EachCardAngleList.Clear();

            //Use the repeat statement to rotate HandCard_CardSpreadOb to store the x, y values in that position.
            for (int i = 0; i < PlayerHandCardList.Count; i++)
            {
                //This method ensures that the card is not aligned properly due to the decimal value, and that the rotated value is still available for the card rotation value.
                HandCard_CardSpreadOb.transform.rotation = Quaternion.Euler(0, 0, (SpreadPoint_Start - ((SpreadPoint_End) * i)));

                HandCard_EachCardDistanceList.Add(HandCard_EachCardOb.transform.position);
                HandCard_EachCardAngleList.Add(HandCard_CardSpreadOb.transform.eulerAngles.z);
            }

            //

        }
    }
}

