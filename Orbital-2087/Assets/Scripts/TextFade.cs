using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFade : MonoBehaviour 
{
	public bool isFaded = false;
    public CanvasGroup uiElement;    

    private void Awake()
    {
        uiElement.alpha = 1;
        FadeOut();
    }

    public void FadeIn()
    {
        isFaded = false;
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
    }
  
    public void FadeOut()
    {
        isFaded = true;
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1f)
    {
        float timeStartedLerping = Time.time;
        float timeSinceStrated = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStrated / lerpTime;

        while (true)
        {
            timeSinceStrated = Time.time - timeStartedLerping;
            percentageComplete = timeSinceStrated / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }
    }
}