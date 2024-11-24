using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlatform : MonoBehaviour
{
   [SerializeField] private Transform playerPos;
    private BoxCollider2D collider;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos.position.y < this.transform.position.y)
            collider.enabled = false;
        else
            collider.enabled = true;
    }

    public void SetPlayerPos(Transform pos)
    {
        playerPos = pos;
    }
}
