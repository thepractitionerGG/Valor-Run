using UnityEngine;

public class ItemSpawnerUpgrade : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject firePrefab;
    public GameObject arrowPrefab;
    public GameObject elephantPrefab;
    public Transform[] spawnPoints;
    public int maxCoinsPerRow = 6;
    public int maxCoinRows = 1;
    public int maxFireRows = 1;
    public int maxArrowsPerRow = 4;
    public int maxElephantsPerRow = 1;
    public float coinGap = 1f;
    public float obstacleSpeed = 5f;

    private bool isPlayerOnPlatform;
    private GameObject[] spawnedCoins;
    private GameObject[] spawnedFires;
    private GameObject[] spawnedArrows;
    private GameObject[] spawnedElephants;

    private void OnEnable()
    {
        isPlayerOnPlatform = false;
        spawnedCoins = new GameObject[0];
        spawnedFires = new GameObject[0];
        spawnedArrows = new GameObject[0];
        spawnedElephants = new GameObject[0];

        SpawnCoins();
        SpawnFires();
        SpawnArrows();
        SpawnElephants();
    }

    private void OnDisable()
    {
        DestroySpawnedObjects();
    }

    private void SpawnCoins()
    {
        int totalCoins = Random.Range(maxCoinsPerRow, maxCoinsPerRow * maxCoinRows + 1);
        int coinsPerRow = Mathf.FloorToInt(totalCoins / maxCoinRows);
        int remainingCoins = totalCoins % maxCoinRows;

        float startingZ = spawnPoints[0].position.z;
        float gap = coinGap;

        for (int row = 0; row < maxCoinRows; row++)
        {
            int coinsInThisRow = coinsPerRow;

            if (row < remainingCoins)
                coinsInThisRow++;

            float startingX = spawnPoints[Random.Range(0, spawnPoints.Length)].position.x;

            for (int i = 0; i < coinsInThisRow; i++)
            {
                Vector3 coinPosition = new Vector3(startingX, 0f, startingZ + (i * gap));
                GameObject coin = Instantiate(coinPrefab, coinPosition, Quaternion.identity);
                spawnedCoins = AddObjectToArray(spawnedCoins, coin);
            }

            startingZ += coinGap;
        }
    }

    private void SpawnFires()
    {
        int fireRows = Random.Range(0, maxFireRows + 1);
        float startingZ = spawnPoints[0].position.z;
        float gap = coinGap;

        for (int row = 0; row < maxCoinRows; row++)
        {
            if (row < fireRows)
            {
                float startingX = spawnPoints[Random.Range(0, spawnPoints.Length)].position.x;

                Vector3 firePosition = new Vector3(startingX, 0f, startingZ);
                GameObject fire = Instantiate(firePrefab, firePosition, Quaternion.identity);
                spawnedFires = AddObjectToArray(spawnedFires, fire);
            }

            startingZ += coinGap;
        }
    }

    private void SpawnArrows()
    {
        int arrowsPerRow = Random.Range(0, maxArrowsPerRow + 1);
        float startingZ = spawnPoints[0].position.z;
        float gap = coinGap;

        for (int row = 0; row < maxCoinRows; row++)
        {
            float startingX = spawnPoints[Random.Range(0, spawnPoints.Length)].position.x;

            for (int i = 0; i < arrowsPerRow; i++)
            {
                Vector3 arrowPosition = new Vector3(startingX, 0f, startingZ);
                GameObject arrow = Instantiate(arrowPrefab, arrowPosition, Quaternion.identity);
                spawnedArrows = AddObjectToArray(spawnedArrows, arrow);
            }

            startingZ += coinGap;
        }
    }

    private void SpawnElephants()
    {
        int elephantsPerRow = Random.Range(0, maxElephantsPerRow + 1);
        float startingZ = spawnPoints[0].position.z;
        float gap = coinGap;

        for (int row = 0; row < maxCoinRows; row++)
        {
            float startingX = spawnPoints[Random.Range(0, spawnPoints.Length)].position.x;

            for (int i = 0; i < elephantsPerRow; i++)
            {
                Vector3 elephantPosition = new Vector3(startingX, 0f, startingZ);
                GameObject elephant = Instantiate(elephantPrefab, elephantPosition, Quaternion.identity);
                spawnedElephants = AddObjectToArray(spawnedElephants, elephant);
            }

            startingZ += coinGap;
        }
    }

    private GameObject[] AddObjectToArray(GameObject[] array, GameObject obj)
    {
        GameObject[] newArray = new GameObject[array.Length + 1];
        array.CopyTo(newArray, 0);
        newArray[newArray.Length - 1] = obj;
        return newArray;
    }

    private void DestroySpawnedObjects()
    {
        foreach (GameObject coin in spawnedCoins)
        {
            Destroy(coin);
        }

        foreach (GameObject fire in spawnedFires)
        {
            Destroy(fire);
        }

        foreach (GameObject arrow in spawnedArrows)
        {
            Destroy(arrow);
        }

        foreach (GameObject elephant in spawnedElephants)
        {
            Destroy(elephant);
        }
    }

    private void Update()
    {
        if (isPlayerOnPlatform)
        {
            MoveMovableTraps();
        }
    }

    private void MoveMovableTraps()
    {
        foreach (GameObject arrow in spawnedArrows)
        {
            arrow.transform.Translate(Vector3.back * obstacleSpeed * Time.deltaTime);
        }

        foreach (GameObject elephant in spawnedElephants)
        {
            elephant.transform.Translate(Vector3.back * obstacleSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
        }
    }
}
