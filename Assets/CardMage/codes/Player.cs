using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleCardDrawAndSpread_HandCard;
using SimpleCardDrawAndSpread_CardDrag;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("PlayerStat")]
    public float health;
    public float maxhealth;
    public bool islive;
    public float mana;
    public float maxmana;
    public float manarecovery;
    public float drawpoint;
    public float maxdrawpoint;
    public float drawrecovery;
    public float movespeed;


    [Header("Bar")]
    public GameObject nullhpbar;
    public GameObject hpbarobject;
    Slider hpbarslider;
    RectTransform hpbarrect;
    public GameObject hpcanvas;
    //
    public GameObject mpbar;
    Slider mpbarslider;
    //
    public GameObject nulldrawbar;
    public GameObject drawbarobject;
    Slider drawbarslider;
    RectTransform drawbarrect;



    [Header("Skills")]
    public GameObject fireball;
    public GameObject s_0fireball;

    [Header("Others")]
    
    HandCardSystem handcardSystem;
    CardDrawSystem carddrawSystem;
    Scaner scaner;
    public bool runOn;
    


    [Header("Animation")]
    Animator anim;

        
  

    public void Awake() 
    {
        anim = GetComponent<Animator>();
        handcardSystem = FindObjectOfType<HandCardSystem>();
        carddrawSystem = FindObjectOfType<CardDrawSystem>();
        scaner = GetComponent<Scaner>();
        Init();
        carddrawSystem.FastWalk(3);//test
      
    }

    void Init()
    {
        islive = true;
        health = maxhealth;
        mana = 0;
        drawpoint = 0;
        manarecovery = 0.5f;
        drawrecovery = 0.5f;
       //hpbarInit
        nullhpbar = Instantiate(hpbarobject, hpcanvas.transform);
        hpbarslider = nullhpbar.GetComponentInChildren<Slider>();
        hpbarrect = nullhpbar.GetComponent<RectTransform>();
        mpbarslider = mpbar.GetComponent<Slider>();
        //drawbraInit
        nulldrawbar = Instantiate(drawbarobject, hpcanvas.transform);
        drawbarslider = nulldrawbar.GetComponentInChildren<Slider>();
        drawbarrect = nulldrawbar.GetComponent<RectTransform>();
       
        StartCoroutine(AutoAttack());
    }

    private void FixedUpdate()
    {
        HpBarUpdate();
        MpBarUpdate();
        DrawBarUpdate();

        if (runOn)
        {
            transform.Translate(Vector3.right * movespeed * Time.fixedDeltaTime);   
        }


        if (runOn)
            return;
           
        if(mana <= 10)
        {
            mana += manarecovery * Time.deltaTime;
        }
        if(drawpoint <= 10)
        {
            drawpoint += drawrecovery * Time.deltaTime;
            if(drawpoint > 10)
            {
                drawpoint = 0;
                carddrawSystem.Button_CardDraw_Manager();
            }
        }

        if (health < 0 && islive)
        {
            anim.SetTrigger("IsDead");
            islive = false;
        }

    }

   

    void HpBarUpdate()
    {
        Vector3 rect = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(transform.position.x + 0.05f, transform.position.y - 0.1f, transform.position.z));
        hpbarrect.anchoredPosition = rect;

        float curHealth = health;
        float maxHealth = maxhealth;
        hpbarslider.value = curHealth / maxhealth;
    }
    void MpBarUpdate()
    {
        float curMp = mana;
        float maxMp = maxmana;
        mpbarslider.value = mana / maxmana;
    }
    void DrawBarUpdate()
    {
        Vector3 rect = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(transform.position.x + 0.05f, transform.position.y - 0.2f, transform.position.z));
        drawbarrect.anchoredPosition = rect;
        drawbarslider.value = drawpoint / maxdrawpoint;
    }


    public void Damagecalculator(float Damage)
    {
        if (!islive)
            return;

        health -= Damage;
            
    }

   public void ReturnReady() //Set under animtor
    {
        anim.SetBool("Ready", true);
        anim.SetBool("Slash", false );
       
    }

    public void StageUpOn()
    {
        StartCoroutine(StageUP());
    }

    IEnumerator StageUP()
    {
        anim.SetBool("Run", true);
        runOn = true;
        movespeed = 3;
        yield return new WaitForSeconds(2f);
        anim.SetBool("Run", false);
        runOn = false;
        movespeed = 0;
    }

    IEnumerator AutoAttack()
    {
        if (GameManager.instanse.isPlay)
        {
               if (!runOn)
              {
            anim.SetBool("Slash", true);
            anim.SetBool("Ready", false);
            yield return new WaitForSeconds(0.3f);
            GameObject autoattack =  GameManager.instanse.pool.Get(1);
            autoattack.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y + 0.3f);
            autoattack.GetComponent<bullet>().Init(0);
                }


            yield return new WaitForSeconds(2f);
            StartCoroutine(AutoAttack());
        }
        

       
    }
    public void CardUnderstand(int cardid, int manacost, int CardLv)
    {

        switch (cardid)
        {
            case 0:
                Card_0_UseFastWalk(CardLv);
                break;

            case 1:
                anim.SetBool("Ready", false);
                anim.SetBool("Slash", true);
               
                GameObject autoattack = GameManager.instanse.pool.Get(2);
                autoattack.transform.position = new Vector2(transform.position.x + 0.4f, transform.position.y - 0.3f);
                autoattack.GetComponent<bullet>().Init(1);

                break;
            case 2:
                StartCoroutine(Card_2ManaCirculation());
                break;
            case 3:
                GameObject Ignite = GameManager.instanse.pool.Get(3);
                Ignite.GetComponent<bullet>().Init(3);
                if(scaner.targets == true)
                {
                    Ignite.transform.position = scaner.targets.transform.position + new Vector3(0, 1.1f);
                }
                else
                {
                    Ignite.transform.position = transform.position + new Vector3(1, 1.1f);
                }

               
               
                break;
        }
        mana -= manacost;
    }

    void Card_0_UseFastWalk(int cardlv)
    {
        int point = Random.Range(1, cardlv + 1);
        carddrawSystem.FastWalk(point);
        Debug.Log(point);
    }

    IEnumerator Card_2ManaCirculation()
    {
        float nowmanarecovery = manarecovery;

        manarecovery = nowmanarecovery + 1;
        yield return new WaitForSeconds(2f);
        manarecovery = nowmanarecovery;
    }
}


        

