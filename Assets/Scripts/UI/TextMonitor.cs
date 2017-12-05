using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMonitor : ResourcesMonitor {
    public Text text;
    public Image imageFeedback;

    Coroutine imageAnim;

    void Start()
    {
        keyPressedFeedback = Resources.Load<Sprite>("GRAPH/UI/keyPressed");
        keyReleasedFeedback = Resources.Load<Sprite>("GRAPH/UI/keyReleased");
    }

    public override void UpdateUI()
    {
        text.text = currentAmount + "/" + maxAmount;
        if(currentAmount != 0)
        {
            if(imageAnim != null)
            {
                StopCoroutine(imageAnim);
            }
            imageFeedback.rectTransform.localScale = Vector3.one;
            imageAnim = StartCoroutine(ImageAnim());
        }
    }

    public IEnumerator ImageAnim()
    {
        float feedbackTime = 0.0f;
        Vector3 baseScale = imageFeedback.rectTransform.localScale;
        Vector3 scaleAim = baseScale * 2.0f;

        while (imageFeedback.rectTransform.localScale != scaleAim)
        {
            feedbackTime += Time.deltaTime;
            imageFeedback.rectTransform.localScale = Vector3.Lerp(baseScale, scaleAim, feedbackTime / 0.1f);
            yield return null;
        }

        while (imageFeedback.rectTransform.localScale != baseScale)
        {
            feedbackTime -= Time.deltaTime;
            imageFeedback.rectTransform.localScale = Vector3.Lerp(baseScale, scaleAim, feedbackTime / 0.1f);
            yield return null;
        }

        yield return null;
    }
}
