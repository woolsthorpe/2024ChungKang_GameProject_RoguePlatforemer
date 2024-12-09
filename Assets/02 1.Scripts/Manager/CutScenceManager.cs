using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScenceManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fadeImage;
    [SerializeField] private List<SpriteRenderer> backgroundList;
    [SerializeField] private float fadeTIme;
    [SerializeField] private float showBackgroundTime;
    [SerializeField] private AnimationCurve curve;

    [SerializeField] private Animator animator;
    void Start()
    {
        StartCoroutine(PlayEndingScence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator PlayEndingScence()
    {
        for(int i=0;i<backgroundList.Count-1; i++)
        {
            yield return StartCoroutine(fadeInOut(1, 0));
            yield return new WaitForSeconds(showBackgroundTime);
            yield return StartCoroutine(fadeInOut(0, 1));
            backgroundList[i].enabled = false;
        }
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("start");
        yield return new WaitForSeconds(0.7f);
        yield return StartCoroutine(fadeInOut(1, 0));
        yield return new WaitForSeconds(8.5f);
        yield return StartCoroutine(fadeInOut(0, 1));
        yield return new WaitForSeconds(1.8f);
        SceneManagers.instance.OnNextScence();
    }
    IEnumerator fadeInOut(int strart, int end)
    {
        float currentTime = 0;
        float percent=0;
        while(percent <1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTIme;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(strart, end, curve.Evaluate(percent));
            fadeImage.color = color;

            yield return null;
        }
        

    }
}
