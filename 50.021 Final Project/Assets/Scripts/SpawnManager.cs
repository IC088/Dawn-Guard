using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    [System.Serializable]
    public class WaveSpawn 

        {
        public string name;
        public Transform enemy;

        public int count;
        public float rate;
        }


    public WaveSpawn[] wave;
    public Transform[] spawnPoints;
    private SpawnState  state = SpawnState.COUNTING;
    private int nextWave = 0;

    public float waveInterval = 5f;
    public float waveCountDown = 0f;

    private float searchCountDown = 1f;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("No Spawn Poinits Found");
        }
        waveCountDown = waveInterval;
    }

    private void Update()
    {

        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                //Begin new Round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountDown <= 0)
        {
            searchCountDown = 1f;
            if (state != SpawnState.SPAWNING)
            {
                //Starting wave

                if (nextWave == wave.Length - 1)
                {
                    nextWave = 0;
                }
                StartCoroutine(SpawnWave(wave[nextWave]));
            }
        }

        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;

        if (nextWave + 1 >= wave.Length + 1)
        {
            nextWave = 0;
            Debug.Log("Completed All waves");
        }

        else
        {
            nextWave++;
        }
    }

    IEnumerator SpawnWave(WaveSpawn _wave)
    {
        Debug.Log(_wave.name);
        state = SpawnState.SPAWNING;


        for (int i = 0; i < _wave.count; i++)

        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }


    void SpawnEnemy(Transform _enemy)

    {
        
        Transform _sp = spawnPoints[Random.Range(0,spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation); 
        Debug.Log(_enemy.name);


    }


    bool EnemyIsAlive()

    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)

        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)

            {
                return false;
            }
        }
        

        return true;
    }
}
