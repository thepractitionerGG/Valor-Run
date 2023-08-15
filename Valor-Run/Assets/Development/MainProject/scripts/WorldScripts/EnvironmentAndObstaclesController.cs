using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAndObstaclesController : MonoBehaviour
{
    GameObject[] _obstacleList;
    GameObject[] _environment;
    int indexObs;
    int indexEnv;

    private void OnEnable()
    {
        SelectRandomObstacle();
        SelectRandomEnvironment();
    }

    private void SelectRandomEnvironment()
    {
        indexEnv = Random.Range(0, _environment.Length);
        _obstacleList[indexEnv].SetActive(true);
    }

    private void SelectRandomObstacle()
    {
        indexObs = Random.Range(0, _obstacleList.Length);
        _environment[indexObs].SetActive(true);
    }

    private void OnDisable()
    {
        _obstacleList[indexEnv].SetActive(false);
        _environment[indexObs].SetActive(false);
        indexObs = 0;
        indexEnv = 0;
    }
}
