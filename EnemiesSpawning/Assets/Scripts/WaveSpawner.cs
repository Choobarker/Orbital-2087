using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	// ***** Debugging Tools *****

	// Spawn an alien at each spawn point on creation
	// also disables regular wave spawning
	bool testSpawns = false;
	// ***** End Of Debugging Tools *****

	public enum SpawnState{SPAWNING, WAITING, COUNTING};

	public Transform outline;

	public float timeBetweenWaves = 5f;
	public float waveCountDown = 0;
	private float searchCountDown = 1f;

	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform enemy;
		public int count;
		public int range;
		public float delay;
	}

	public Wave[] waves;
	private int nextWave = 0;

	public List<Transform> spawns = new List<Transform>();
	public Transform enemySpawnPoint;

	

	private SpawnState state = SpawnState.COUNTING;
	private System.Random rand = new System.Random();

	public Transform enemyYa;

	public float playerViewDistance;
	public int numberOfSpawns;
	public float distanceBetweenSpawns;
	public float angleBetweenSpawns;
	public float angleOnPerimeter;
	

	void Start()
	{
		//GetSpawns();
		AnotherGetSpawns();
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
				if(!testSpawns)
				{
					StartCoroutine(SpawnWave(waves[nextWave]));
				}
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

		List<Transform> spawnPoints = new List<Transform>();
		if((wave.range * 2 + 1) > spawns.Count)
		{
			spawnPoints = spawns;
		}
		else
		{
			for(int i = 1; i <= wave.range; ++i)
			{
				spawnPoints.Add(spawns[spawns.Count - i]);
			}

			for(int i = 0; i <= wave.range; ++i)
			{
				spawnPoints.Add(spawns[i]);
			}
		}

		for(int i = 0; i < wave.count; i++)
		{			
			SpawnEnemy(wave.enemy, spawnPoints);
			yield return new WaitForSeconds(wave.delay);
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(Transform enemy, List<Transform> spawnPoints)
	{
		Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

		// Spawn the enemy at a predefined location
	}

	void SpawnEnemyAtPoint()
	{
	
	}

	void GetSpawns()
	{
		distanceBetweenSpawns = (float)(2 * System.Math.PI * playerViewDistance) / numberOfSpawns;
		angleBetweenSpawns = (float)(distanceBetweenSpawns / (2 * System.Math.PI * playerViewDistance)) * 360;
		angleOnPerimeter = (180 - angleBetweenSpawns) / 2;

		Vector3 nextPoint = new Vector3(0, playerViewDistance, 0);
		Vector3 lastPoint;
		Vector3 pointBeforeLast = Vector3.zero;

		for(int i = 0; i < numberOfSpawns; i++)
		{
			enemySpawnPoint.position = nextPoint;
			spawns.Add(Instantiate(enemySpawnPoint, enemySpawnPoint.position, enemySpawnPoint.rotation));
			lastPoint = nextPoint;

			Debug.Log(nextPoint.x + " " + nextPoint.y);

			Instantiate(enemyYa, enemySpawnPoint.position, enemySpawnPoint.rotation);

			float C = 90f;
			float c = playerViewDistance / Sin(angleOnPerimeter) * Sin(angleBetweenSpawns);
			float Y = 90 - angleOnPerimeter;
			float y = (c / Sin(C) * Sin(Y));
			float x = (float)(System.Math.Sqrt(c * c - y * y));
			
			Debug.Log("cal x: " + x + " calc y: " + y);

			bool good = false;
			int count = 0;

			while(!good)
			{
				if(OnArch(new Vector3(nextPoint.x + x, nextPoint.y + y, 0)) && !SamePoints(new Vector3(nextPoint.x + x, nextPoint.y + y, 0), pointBeforeLast))
				{
					Debug.Log("good");
					good = true;					
				}
				else
				{
					if(OnArch(new Vector3(nextPoint.x - x, nextPoint.y + y, 0)) && !SamePoints(new Vector3(nextPoint.x - x, nextPoint.y + y, 0), pointBeforeLast))
					{
						x *= -1;
					}
					else if(OnArch(new Vector3(nextPoint.x + x, nextPoint.y - y, 0)) && !SamePoints(new Vector3(nextPoint.x + x, nextPoint.y - y, 0), pointBeforeLast))
					{
						y *= -1;
					}
					else if(OnArch(new Vector3(nextPoint.x - x, nextPoint.y - y, 0)) && !SamePoints(new Vector3(nextPoint.x - x, nextPoint.y - y, 0), pointBeforeLast))
					{
						x *= -1;
						y *= -1;						
					}
					else
					{
						float temp = x;
						x = y;
						y = temp;
					}
				}
				count++;

				if(count > 2)
				{
					Debug.Log("infinite loop..... aborting");
					good = true;
				}
			}

			//nextPoint = new Vector3(nextPoint.x + x, nextPoint.y + y, 0);
			nextPoint.x += x;
			nextPoint.y += y;

			pointBeforeLast = lastPoint;

			Debug.Log("final x: " + nextPoint.x + " y: " + nextPoint.y);
		}
	}

	void AnotherGetSpawns()
	{
		distanceBetweenSpawns = (float)(2 * System.Math.PI * playerViewDistance) / numberOfSpawns;
		angleBetweenSpawns = (float)(distanceBetweenSpawns / (2 * System.Math.PI * playerViewDistance)) * 360;		

		Vector3 startPoint = new Vector3(0, playerViewDistance, 0);
		Vector3 nextPoint = startPoint;

		float totalAngle = 0;
		float nextAngle = 0;

		for(int i = 0; i < numberOfSpawns; i++)
		{
			enemySpawnPoint.position = nextPoint;
			enemySpawnPoint.rotation = Quaternion.Euler(0, 0, -totalAngle);
			spawns.Add(Instantiate(enemySpawnPoint, enemySpawnPoint.position, enemySpawnPoint.rotation));
			
			if(testSpawns)
			{
				Instantiate(enemyYa, enemySpawnPoint.position, enemySpawnPoint.rotation);
			}

			nextPoint = startPoint;

			totalAngle += angleBetweenSpawns;
			nextAngle = totalAngle;

			if(nextAngle == 180)
			{
				Debug.Log("nextAngle = 180");
				nextPoint.y = -playerViewDistance;
			}
			else
			{
				if(nextAngle > 180)
				{
					nextAngle = 360 - totalAngle;
				}

				angleOnPerimeter = (180 - nextAngle) / 2;

				float C = 90f;
				float c = playerViewDistance / Sin(angleOnPerimeter) * Sin(nextAngle);
				float Y = 90 - angleOnPerimeter;
				float y = (c / Sin(C) * Sin(Y));
				float x = (float)(System.Math.Sqrt(c * c - y * y));

				Debug.Log("cal x: " + x + " calc y: " + y);

				bool good = false;
				int count = 0;
			
				while(!good)
				{
					if(OnArch(new Vector3(nextPoint.x + x, nextPoint.y + y, 0)) && !PointExists(new Vector3(nextPoint.x + x, nextPoint.y + y, 0)))
					{
						Debug.Log("good");
						good = true;					
					}
					else
					{
						if(OnArch(new Vector3(nextPoint.x - x, nextPoint.y + y, 0)) && !PointExists(new Vector3(nextPoint.x - x, nextPoint.y + y, 0)))
						{
							x *= -1f;
						}
						else if(OnArch(new Vector3(nextPoint.x + x, nextPoint.y - y, 0)) && !PointExists(new Vector3(nextPoint.x + x, nextPoint.y - y, 0)))
						{
							y *= -1f;
						}
						else if(OnArch(new Vector3(nextPoint.x - x, nextPoint.y - y, 0)) && !PointExists(new Vector3(nextPoint.x - x, nextPoint.y - y, 0)))
						{
							x *= -1f;
							y *= -1f;						
						}
						else
						{
							float temp = x;
							x = y;
							y = temp;
						}
					}
					count++;

					if(count > 2)
					{
						Debug.Log("infinite loop..... aborting");
						good = true;
					}
				}

				nextPoint.x += x;
				nextPoint.y += y;
			}			
		}

	}

	float Sin(float angle)
	{
		double a = DegreesToRadians(angle);
		angle = (float)System.Math.Sin(a);
		return angle;
	}

	double DegreesToRadians(float angle)
	{
		return angle * (System.Math.PI / 180);
	}

	bool PointExists(Vector3 point)
	{
		bool exists = false;

		for(int i = 0; i < spawns.Count; i++)
		{
			float x1 = (float)System.Math.Round(spawns[i].position.x);
			float y1 = (float)System.Math.Round(spawns[i].position.y);

			float x2 = (float)System.Math.Round(point.x);
			float y2 = (float)System.Math.Round(point.y);

			if(x1 == x2 && y1 == y2)
			{
				exists = true;
			}
		}

		Debug.Log("Exists: " + exists);

		return exists;
	}

	bool OnArch(Vector3 nextPoint)
	{
		float h = System.Math.Abs((nextPoint.x * nextPoint.x) + (System.Math.Abs(nextPoint.y * nextPoint.y)));
		Debug.Log("x len: "  + nextPoint.x + " y len: " + nextPoint.y);
		h = (float)System.Math.Round(h);
		Debug.Log(h);
		if(System.Math.Sqrt(h) != playerViewDistance)
		{
			Debug.Log("Not on arch");
			return false;
		}

		Debug.Log("On arch");

		return true;
	}

	bool SamePoints(Vector3 lastPoint, Vector3 nextPoint)
	{
		if(lastPoint.x == nextPoint.x && lastPoint.y == nextPoint.y)
		{
			Debug.Log("same points");
			return true;
		}

		Debug.Log("Different Points");

		return false;
	}
}
