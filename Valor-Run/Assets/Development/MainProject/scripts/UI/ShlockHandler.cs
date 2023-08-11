using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShlockHandler : MonoBehaviour
{
    [SerializeField] Sprite [] _shlockImages;
    [SerializeField] GameObject _closeButton;
    Color _color;

    private void OnEnable()
    {
        _color = GetComponent<RawImage>().color;

        int i = Random.Range(0, _shlockImages.Length);

        GetComponent<RawImage>().texture = _shlockImages[i].texture;

        StartCoroutine(EnableCloseButton());
    }

    IEnumerator EnableCloseButton()
    {
        while (_color.a < 1f) 
        {
            float newAlpha = Mathf.Clamp01(_color.a + (2 * Time.deltaTime));
            _color = new Color(_color.r, _color.g, _color.b, newAlpha);
            GetComponent<RawImage>().color = _color;

            yield return null;
        }
        yield return new WaitForSeconds(2f);
        _closeButton.SetActive(true);

    }

    private void OnDisable()
    {
        _closeButton.SetActive(false);
    }

}
