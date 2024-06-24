using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("BulletsStat")]
    public float bulletspeed;
    public float damage;

    [Header("CardStat")]
    public int thiscardnumber;

    [Header("Animation")]
    Animator anim;



    private void Awake()
    {
        
        anim = GetComponent<Animator>();
      
    }
    private void OnEnable()
    {
      
       
    }


    public void Init(int cardnumber)
    { 
        switch (cardnumber)
        {
            case 0:
                damage = 1;
                bulletspeed = 5;
                break;

            case 1:
                damage = 30;
                bulletspeed = 2;
                break;

            case 3:
                damage = 100;
                bulletspeed = 0;
                break;

            default:
                break;
        }
        thiscardnumber = cardnumber;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * bulletspeed * Time.fixedDeltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Mob"))
            return;

        collision.GetComponent<Mob>().Damagecalculator(damage);

        switch (thiscardnumber)
        {
                case 0: 
                bulletspeed = 0;
                anim.SetBool("IsHit", true);
                break;

            case 1:
                break;
        }
       
    }

    public void delete()
    {
        gameObject.SetActive(false);
    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
