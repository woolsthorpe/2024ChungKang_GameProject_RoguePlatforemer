/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : Monster
{
    bool ready = true;
    float defualtTime = 1f;
    float val = 0.9f;
    [SerializeField]
    GameObject[] Pattern;
    void Start()
    {
        HP = 100f;
    }
    public override void OnHit(float DMG)
    {

    }
    // Update is called once per frame
    void Update()
    {
        Pattern[0].GetComponent<SpriteRenderer>().color = new Color(1,val,0,1);
        if(val == 0){

        }else{
            val -= 0.001f;
        }
    }
}
*/
    