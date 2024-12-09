using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class camera : MonoBehaviour
{
    [SerializeField]
    public GameObject[] camera_p;
    public float time = 0;
    bool[] a = {false,false,false,false,false};
    void Start()
    {
        
    }
    public void Setcamera(){
        camera_p[0].SetActive(false);
        camera_p[1].SetActive(true);
        
    }
    public void Setcamera1(){
        camera_p[0].SetActive(true);
        camera_p[1].SetActive(false);
        
    }
    // Update is called once per frame
    void Update()
    {
        time += 1*Time.deltaTime;

        if(time > 2&&a[0]==false){
            a[0] = true;
            camera_p[1].SetActive(false);
            camera_p[2].SetActive(true);
        }
        
        if(time > 3&&a[1]==false){
            a[1] = true;
            camera_p[2].SetActive(false);
            camera_p[3].SetActive(true);
        }
        
        if(time > 4&&a[2]==false){
            a[2] = true;
            camera_p[3].SetActive(false);
            camera_p[4].SetActive(true);
        }
        
        if(time > 5&&a[3]==false){
            a[3] = true;
            camera_p[4].SetActive(false);
            camera_p[0].SetActive(true);
        }
    }
}
