using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityMap : MonoBehaviour
{
    // Start is called before the first frame update

    public RectTransform Rect;
    private void Awake()
    {
      
      
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rect.anchoredPosition += new Vector2(6000, 0);
            Debug.Log("asd");
        }
    }
}
