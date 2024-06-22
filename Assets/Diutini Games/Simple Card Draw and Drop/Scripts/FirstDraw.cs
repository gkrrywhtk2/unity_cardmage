using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleCardDrawAndSpread_CardDrag;

public class FirstDraw : MonoBehaviour
{
    // Start is called before the first frame update
    CardDrawSystem cardDrawSystem;

    void Start()
    {
        
    }
    private void Awake()
    {
        cardDrawSystem = FindObjectOfType<CardDrawSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw()
    {
        cardDrawSystem.StartDraw();
    }
}
