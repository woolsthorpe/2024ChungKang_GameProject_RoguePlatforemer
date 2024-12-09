using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlatform : MonoBehaviour
{
   [SerializeField] private Transform playerPos;
    private BoxCollider2D colliderC;
    void Start()
    {
        colliderC = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos.position.y < this.transform.position.y)
            colliderC.enabled = false;
        else
            colliderC.enabled = true;
    }

    public void SetPlayerPos(Transform pos)
    {
        playerPos = pos;
    }
}
