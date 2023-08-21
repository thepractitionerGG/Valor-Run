using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutEffect : MonoBehaviour
{
    Color _color;
    private void Start()
    {
       _color = GetComponent<RawImage>().color;
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        while (_color.a> 0f)
        {
            float newAlpha = Mathf.Clamp01(_color.a - (1 * Time.deltaTime));
            _color = new Color(_color.r, _color.g, _color.b, newAlpha);
            GetComponent<RawImage>().color = _color;

            yield return null;
        }
        gameObject.SetActive(false);

    }
}
