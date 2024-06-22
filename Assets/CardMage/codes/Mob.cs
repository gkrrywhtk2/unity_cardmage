using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mob : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("MonsterStat")]
    public float speed;
    public float health;
    public float maxhealth;
    public float Damage;
    public bool ishit;

    [Header("HpBar")]
    public GameObject nullhpbar;
    public GameObject hpbarprefab;
    public GameObject hpcanvas;
    public RectTransform hpbarRect;
    public Slider hpslide;

    [Header("Animation")]
    Animator anim;


   [Header("Others")]
    Rigidbody2D rigid;
    Collider2D coll;
   

    

    private void Awake()
    {
      
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
       
        Init();
    }
    public void Init()
    {
        hpcanvas = GameObject.Find("UI_Canvas");
        nullhpbar = Instantiate(hpbarprefab,hpcanvas.transform);
        hpslide = nullhpbar.GetComponentInChildren<Slider>();
        hpbarRect = nullhpbar.GetComponent<RectTransform>();
        health = maxhealth;
        speed = 0.3f;
        Damage = 0.5f;
       // nullhpbar = Instantiate(hpbarprefab, new Vector2(hpcanvas.transform.position.x, hpcanvas.transform.position.y + 2000f), Quaternion.identity, hpcanvas.transform);
    }

    private void FixedUpdate()
    {
        if (!GameManager.instanse.isPlay)
            return;
        
        if (!ishit)
        {
         Vector2 nextVec = Vector2.left * speed * Time.fixedDeltaTime;
         rigid.MovePosition(rigid.position + nextVec);
        }

        Vector3 rect = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(transform.position.x + 0.05f, transform.position.y - 0.1f, transform.position.z));
        hpbarRect.anchoredPosition = rect;

        float curHealth = health;
        float maxHealth = maxhealth;
        hpslide.value = curHealth / maxhealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if(health > 0)
            {
                StartCoroutine(KnockBack());
            }
            else
            {
                StartCoroutine(Dead());
            }
         
        }

        if (collision.CompareTag("Player"))
        {
            Attack();
        } 
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Walk();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!GameManager.instanse.isPlay)
            return;

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Damagecalculator(Damage);
        }
    }

    public void Damagecalculator(float Damage)
    {
        health -= Damage;
    }


    IEnumerator KnockBack()
    {
        ishit = true;
        rigid.AddForce(Vector2.right * 0.05f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        ishit = false;
    }

    IEnumerator Dead()
    {
        coll.enabled = false;
        rigid.simulated = false;
        nullhpbar.gameObject.SetActive(false);
        anim.SetTrigger("IsDead");
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    void Attack()
    {
        anim.SetBool("IsAttacking", true);
        speed = 0;
    }
    void Walk()
    {
        anim.SetBool("IsAttacking", false);
        speed = 0.3f;
    }

}
