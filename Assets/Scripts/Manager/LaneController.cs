using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class LaneController : MonoBehaviour
{


    public EnemyController[] enemies;
    private bool enemySpawnReady = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemySpawnReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawnReady)
        {
            StartCoroutine("EnemySpawnCoroutine");
            
        }
    }

    private IEnumerator EnemySpawnCoroutine()
    {
        enemySpawnReady = false;
        float rndVariance = Random.Range(0.1f, 3f);
        int _gameDifficulty = GameManager.instance.difficulty;

        yield return new WaitForSeconds(1f + rndVariance / (_gameDifficulty+1));
        enemySpawnReady = true;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        int _randomEnemy = Random.Range(0, enemies.Length); //spawn random of any

        //fast enemies should be common, moderate ones kinda frequent, tanks more rare
        //should I increase the amount of enemies spawned? vary which lanes spawn first? decrease delay?
        //0 = fast (0.9% chance)
        //1 = mod (0.7% chance)
        //2 = tank (0.2% chance)
        int _selectedEnemy = 0;
        int _oneOfTenChance = Random.Range(0, 7);
        switch (_oneOfTenChance)
        {
            case 0:
                _selectedEnemy = 0; break;
            case 1:
                _selectedEnemy = 0; break;
            case 2:
                _selectedEnemy = 1; break;
            case 3:
                _selectedEnemy = 1; break;
            case 4:
                _selectedEnemy = 1; break;
            case 5:
                _selectedEnemy = 1; break;
            case 6:
                _selectedEnemy = 2; break;

        }
        EnemyController spawnedEnemy = Instantiate(enemies[_selectedEnemy], transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Vector3 _currentPos = (new Vector3(transform.position.x, transform.position.y, transform.position.z));
        Vector3 _size = new Vector3(0.6f, 0.6f, 0f);
        Gizmos.DrawWireCube(_currentPos, _size);
    }
}
