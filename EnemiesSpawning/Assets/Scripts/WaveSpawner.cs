using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState{SPAWNING, WAITING, COUNTING};

	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform enemy;
		public int count;
		public float delay;
	}

	public Transform[] spawnPoints;
	public Wave[] waves;
	private int nextWave = 0;

	public float timeBetweenWaves = 5f;
	private float waveCountDown = 0;
	private float searchCountDown = 1f;

	private SpawnState state = SpawnState.COUNTING;
	private System.Random rand = new System.Random();

	void Start()
	{
		waveCountDown = timeBetweenWaves;
	}

	void Update()
	{
		if(state == SpawnState.WAITING)
		{
			if(!EnemyIsAlive())
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if(waveCountDown <= 0)
		{
			if(state != SpawnState.SPAWNING)
			{
				StartCoroutine(SpawnWave(waves[nextWave]));
			}			
		}
		else
		{
			waveCountDown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
		Debug.Log("Wave Completed");

		state = SpawnState.COUNTING;
		waveCountDown = timeBetweenWaves;

		nextWave++;
		if(nextWave >= waves.Length)
		{
			nextWave = 0;
			Debug.Log("All Waves Complete");
		}
	}

	bool EnemyIsAlive()
	{
		searchCountDown -= Time.deltaTime;
		if(searchCountDown >= 0)
		{
			searchCountDown = 1f;
			if(GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				return false;
			}
		}		

		return true;
	}

	IEnumerator SpawnWave(Wave wave)
	{
		Debug.Log("Spawning Wave: " + wave.name);
		state = SpawnState.SPAWNING;

		for(int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(wave.delay);
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(Transform enemy)
	{
		Debug.Log("Spawning Enemy: " + enemy.name);

		// Give the enemy a predefined spawn point
		Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
		Instantiate(enemy, sp.position, sp.rotation);

		// Generage random enemy spawn locations within an area
		/*
		float x = rand.Next(-10, 10);
		float y = rand.Next(-10, 10);
		float z = 0;

		Instantiate(enemy, new Vector3(x, y, z), transform.rotation);
		 */
	}
}
