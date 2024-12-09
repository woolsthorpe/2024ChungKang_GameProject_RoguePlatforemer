using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : Monster
{
    float alpah1 = 1;
    float alpah2 = 1;
    [SerializeField]
    GameObject plant;
    [SerializeField]
    GameObject r_light;
    [SerializeField]
    GameObject l_light;
    
    flower[] chil1;
    SpriteRenderer[] chil;
    void Start()
    {
        HP = 250f;
        chil1  = plant.GetComponentsInChildren<flower>();
        chil    = plant.GetComponentsInChildren<SpriteRenderer>();
    }
    public override void OnHit(float DMG)
    {
        HP -= DMG;
        if(HP<0){
            sleep = false;
            for(int i = 0;i<4;i++){
                try{
                    chil1[i].GetComponent<flower>().IsStop();
                }catch{

                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(sleep == false){
            plant.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alpah1);
            for(int i = 1;i<4;i++){
                try{
            chil[i].color = new Color(0.25f,0.25f,0.25f,alpah1);
                }catch{

                }
            }
            alpah1 -= 0.001f;
            //ㅊGetComponent<camera>().Setcamera();
        }
        if(alpah1 <0 && Stop == false){
            
            r_light.GetComponent<SpriteRenderer>().color = new Color(0,0,0,alpah2);
            alpah2-=0.01f;
        }
        if(alpah2 < 0){
            Stop = true;
        }
        if(Stop == true){
            l_light.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alpah2);
            alpah2+=0.01f;
        }
    }
}
    