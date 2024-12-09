using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpHeartIcon : MonoBehaviour
{
    [SerializeField] private Image fullHeartImage;
    [SerializeField] private Image emptyHeartImage;

    private void Start()
    {
        
    }

    public void isActiveHeart(bool isActive)
    {
        if (isActive)
            fullHeartImage.enabled = true;
        else
            fullHeartImage.enabled = false;
    }
}
