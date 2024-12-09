using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject icon;
    [SerializeField] private List<HpHeartIcon> heartIcons;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
            
    }

   

    public void ChangeHeartIcon(int current,int max)
    {
        if(max > heartIcons.Count)
        {
            for(int i=0; i< max-heartIcons.Count;i++)
            {
                GameObject obj=Instantiate(icon);
                obj.transform.SetParent(transform.GetChild(0));
                heartIcons.Add(obj.GetComponent<HpHeartIcon>());
               
            }
        }

        for(int i=0;i<max; i++)
        {
            if (i < current)
                heartIcons[i].isActiveHeart(true);
            else
                heartIcons[i].isActiveHeart(false);
        }
    }
}
