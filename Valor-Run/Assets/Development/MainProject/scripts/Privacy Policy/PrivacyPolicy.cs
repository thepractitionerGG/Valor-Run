using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyPolicy : MonoBehaviour
{
    public string url;

    public void OnButtonClick()
    {
        Application.OpenURL(url);
    }
}
