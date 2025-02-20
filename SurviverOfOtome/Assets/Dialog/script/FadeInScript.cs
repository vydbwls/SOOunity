using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    public void Fade()
    {
        StartCoroutine(FadeFlow());
        dialog.instance.isFadeIn_ = false;
    }
    IEnumerator FadeFlow()
    {
        time = 0f;
        Color alpha = Panel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }
}
