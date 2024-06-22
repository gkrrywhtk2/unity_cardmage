using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scaner : MonoBehaviour
{
    // Start is called before the first frame update
    public float scanrange;
    public RaycastHit2D targets;
    public LayerMask targetlayer;
    public Transform nearesttarget;

     public void FixedUpdate()
    {


        RaycastHit2D target = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.3f), transform.right, scanrange, targetlayer);
        // Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.3f), transform.right * scanrange, Color.yellow);
        targets = target;

    }
}

    
    


