using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject prefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)){
            Instantiate(prefab,new Vector2(449,252.5f),quaternion.identity,GameObject.Find("Canvas").transform);
        }
    }
}
