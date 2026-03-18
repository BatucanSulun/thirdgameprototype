using System.Net.NetworkInformation;
using UnityEditor.Rendering;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerupPrefab;
    private float spawnPosition = 9f;
    private int enemiesToSpawn = 3;
    [SerializeField] private int enemyCount;
    [SerializeField] private int waveNumber=1;
    void Start()
    {
        SpawnEnemyWaves(enemiesToSpawn);
        SpawnPowerups();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Merhaba Dünya");
        #region Aşağıdaki kodda ne oluyor??
        //Bie scene'deki bütün düşmanları bulabilmek için FindObjectByType metodunu kullanmamız lazım.
        //Bizim durumumuzda enemy script'i attach'lenmiş bütün objeleri bulmak istiyoruz ki düşman sayısını takip edebilelim.
        //Ancak bir veri tutuşmazlığı var FindObjectByType jeneriği array döndürüyor ancak enemyCount ise bir int.Bu yüzden Lenght prop'unu kullanıyoruz.
        #endregion
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
        if (enemyCount==0) //Sahnede hiç düşman kalmazsa bir önceki spawn ettiğin düşman sayısından bir fazlasını spawnla şeklinde metoda argüman veriyoruz.
        {
            waveNumber++;
            SpawnEnemyWaves(waveNumber);
            SpawnPowerups();
        }
    }

    private void SpawnEnemyWaves(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    private void SpawnPowerups()
    {
        Instantiate(powerupPrefab,GenerateSpawnPosition(),powerupPrefab.transform.rotation);    
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnX = Random.Range(spawnPosition, -spawnPosition);
        float spawnZ = Random.Range(spawnPosition, -spawnPosition);
        return new Vector3(spawnX, 0, spawnZ);
    }
}
