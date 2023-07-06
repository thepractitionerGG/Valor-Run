using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenrateLevel : MonoBehaviour
{
    public GameObject[] sections;
    public int zPos = 50;
    public bool creatingSection = false;
    public int secNum;

    // Update is called once per frame
    void Update()
    {
        if (creatingSection == false)
        {
            creatingSection = true;
            StartCoroutine(GenrateSection());
        }
    }

    IEnumerator GenrateSection()
    {
        secNum = Random.Range(0, 3);
        Instantiate(sections[secNum], new Vector3(0, 1, zPos), Quaternion.identity);
        zPos += 40;
        yield return new WaitForSeconds(2f);
        creatingSection = false;
    }
}
