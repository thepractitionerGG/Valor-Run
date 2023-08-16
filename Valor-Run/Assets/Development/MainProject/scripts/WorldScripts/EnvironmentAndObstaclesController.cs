using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAndObstaclesController : MonoBehaviour
{
    [SerializeField] GameObject[] _obstacleList;
    [SerializeField] GameObject[] _environment;
    int indexObs;
    int indexEnv;

    bool _wasEnabledOnce;

    private void OnEnable()
    {
        _wasEnabledOnce = true;
        SelectRandomObstacle();
        SelectRandomEnvironment();
    }

    private void SelectRandomEnvironment()
    {
        if (_wasEnabledOnce)
        {
            indexObs = Random.Range(0, _obstacleList.Length);
            _obstacleList[indexObs].SetActive(true);
        }
      
    }

    private void SelectRandomObstacle()
    {
        if (_wasEnabledOnce)
        {
            indexEnv = Random.Range(0, _environment.Length);
            _environment[indexEnv].SetActive(true);
        }
    }

    private void OnDisable()
    {
        _wasEnabledOnce = false;
        _obstacleList[indexObs].SetActive(false);
        _environment[indexEnv].SetActive(false);
    }
}
