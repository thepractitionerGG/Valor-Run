using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Texture[] _loadingImages;
    public RawImage _image;
    Color _color;
    [SerializeField] Transform _loadingObject;
    [SerializeField] Transform _content;
    private void Start()
    {
        _color = _image.color;
        ShowLoadingScreens();
        SceneManager.LoadSceneAsync("Game");
    }

    private void ShowLoadingScreens()
    {
        int number = Random.Range(0, _loadingImages.Length);
        _image.texture = _loadingImages[number];
        StartCoroutine(SpawnPrefabWithDelayCoroutine());
    }

    private IEnumerator SpawnPrefabWithDelayCoroutine()
    {
        for (int i = 0; i < 7; i++)
        {
            Instantiate(_loadingObject, _content);
            yield return new WaitForSeconds(.5f);
        }
    }
}
