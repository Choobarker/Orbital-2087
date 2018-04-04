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
		public float rate;
	}

	public Wave[] waves;
	private int nextWave = 0;

	public float timeBetweenWaves = 5f;
	public float waveCountDown = 0;

	private SpawnState state = SpawnState.COUNTING;

	void Start()
	{
		waveCountDown = timeBetweenWaves;
	}

	void Update()
	{
		if(waveCountDown <= 0)
		{
			if(state != SpawnState.SPAWNING)
			{
				// start spawning wave

				waveCountDown = timeBetweenWaves;
			}
			else
			{
				waveCountDown -= Time.deltaTime;
			}
		}
	}

	IEnumerator SpawnWave(Wave wave)
	{
		state = SpawnState.SPAWNING;

		// Spawn

		state = SpawnState.WAITING;

		yield break;
	}
}
