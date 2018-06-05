using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour 
{
	// ***** Debugging Tools *****

	// Spawn an alien at each spawn point on creation
	// also disables regular wave spawning
	public bool testSpawns = false; 
	public Transform testEnemy;

	// ***** End Of Debugging Tools *****

    private const float HEALTH_BUFF_INCREASE = 10f;
    private const float DAMAGE_BUFF_INCREASE = 5f;
    private const float SCORE_BUFF_INCREASE = 5f;
    private const int HEALTH_BUFF_INTERVAL = 5;
    private const int DAMAGE_BUFF_INTERVAL = 10;

	public enum SpawnState{SPAWNING, WAITING, COUNTING};	
	private SpawnState state = SpawnState.COUNTING;

	public float secondsBetweenWaves = 5f;
	private float waveCountDown = 0;	
	// Number of seconds between each search for enemies still alive
	// A number too low will be too demanding on performance, and too
	// high will create too much of a delay. 1 second seems to work well
	private float searchCountDown = 1f;

	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform enemy;
		public int count;
		public float delay;
		// Number of spawns on either side of the center 
		// e.g. 'range = 2' means enemies can spawn 2 locations to the left or right of the center spawn
		// for a total of 5 possible locations
		public int range;
	}

	public List<Wave> waves;
	private int nextWaveIndex = 0;

	public Transform enemySpawnPoint;
	private List<Transform> spawns = new List<Transform>();	

	public float spawnRadius;
	public int numberOfSpawns;
	private float distanceBetweenSpawns;
	private float angleBetweenSpawns;
	private float angleOnPerimeter;

    private float damageBuff = 0;
    private float healthBuff = 0;
    private float scoreBuff = 0;

    private TextFade fade;
    private UpgradeMenu upgradeMenu;

	void Start()
	{
		CreateSpawns();
		waveCountDown = secondsBetweenWaves;
        upgradeMenu = GameObject.FindGameObjectWithTag("UpgradeMenu").GetComponent<UpgradeMenu>();
        upgradeMenu.CloseMenu(); // TODO should look at making the upgrade menu start closed
        fade = gameObject.GetComponent<TextFade>();
	}

	void Update()
	{        
        if (state == SpawnState.WAITING)
		{
            if (!EnemyIsAlive())
			{
				WaveCompleted();
                WaveNotifier.waveNum += 1; // TODO should make a method to increment the waveNum variable, rather than access directly
                upgradeMenu.EnableButton();
                fade.FadeIn(); // TODO should look at controlling timed fade in/out inside TextFade, and have a call to start it
            }
            else
			{
                // If there are enemies alive, no need to run the rest of the Update() code
                return;
			}
        }

		if(waveCountDown <= 0)
		{
			if(state != SpawnState.SPAWNING)
			{
                if (!testSpawns)
				{
					StartCoroutine(SpawnWave(waves[nextWaveIndex]));
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
        state = SpawnState.COUNTING;
		waveCountDown = secondsBetweenWaves;

		nextWaveIndex++;
		if(nextWaveIndex >= waves.Count)
		{
            CreateNextWave();
		}
	}

	bool EnemyIsAlive()
	{
        bool isAlive = true;
		searchCountDown -= Time.deltaTime;

		if(searchCountDown >= 0)
		{
			searchCountDown = 1f;

			if(GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				isAlive =  false;
			}
		}

		return isAlive;
	}

	IEnumerator SpawnWave(Wave wave)
	{
		state = SpawnState.SPAWNING;
        upgradeMenu.DisableButton();
        fade.FadeOut(); // TODO as described above for fade.FadeIn()
        CalculateBuffs();
        
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
			SpawnEnemies(wave.enemy, spawnPoints);
			yield return new WaitForSeconds(wave.delay);
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemies(Transform enemy, List<Transform> spawnPoints)
	{
		Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
		enemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

        EnemyBehaviour enemyBehaviour = enemy.gameObject.GetComponent<EnemyBehaviour>();

        enemyBehaviour.BuffHealth(healthBuff);
        enemyBehaviour.BuffScore(scoreBuff);
        enemy.gameObject.GetComponent<Weapon>().BuffDamage(damageBuff);
	}
	
	void CreateSpawns()
	{
		distanceBetweenSpawns = (float)(2 * System.Math.PI * spawnRadius) / numberOfSpawns;
		angleBetweenSpawns = (float)(distanceBetweenSpawns / (2 * System.Math.PI * spawnRadius)) * 360;		

		Vector3 startPoint = new Vector3(0, spawnRadius, 0);
		Vector3 nextPoint;

		float totalAngle = 0;
		float nextAngle = 0;

		for(int i = 0; i < numberOfSpawns; i++)
		{
			totalAngle += angleBetweenSpawns;
			nextAngle = totalAngle;

			nextPoint = GetNextPoint(startPoint, totalAngle, nextAngle);

			enemySpawnPoint.position = nextPoint;
			enemySpawnPoint.rotation = Quaternion.Euler(0, 0, -totalAngle);
			spawns.Add(Instantiate(enemySpawnPoint, enemySpawnPoint.position, enemySpawnPoint.rotation));
			
			if(testSpawns)
			{
				Instantiate(testEnemy, enemySpawnPoint.position, enemySpawnPoint.rotation);
			}		
		}
	}

    void CreateNextWave()
    {
        Wave nextWave = new Wave();
        Wave prevWave = waves[nextWaveIndex - 1];
        nextWave.count = prevWave.count + 1;
        nextWave.enemy = prevWave.enemy;
        nextWave.delay = prevWave.delay;
        nextWave.range = prevWave.range + 1;
        
        waves.Add(nextWave);
    }

    void CalculateBuffs()
    {
        // Buff enemy health after every HEALTH_BUFF_INTERVAL'th wave, and damage after
        // every DAMAGE_BUFF_INTERVAL'th wave
        if(nextWaveIndex % HEALTH_BUFF_INTERVAL == 0 && nextWaveIndex != 0)
        {
            healthBuff += HEALTH_BUFF_INCREASE;

            if(nextWaveIndex % DAMAGE_BUFF_INTERVAL == 0)
            {
                damageBuff += DAMAGE_BUFF_INCREASE;
            }

            scoreBuff += SCORE_BUFF_INCREASE;
        }
    }

	Vector3 GetNextPoint(Vector3 startPoint, float totalAngle, float nextAngle)
	{
		Vector3 nextPoint = startPoint;

		if(nextAngle == 180)
		{
			nextPoint.y = -spawnRadius;
		}
		else if(nextAngle != 0)
		{
			if(nextAngle > 180)
			{
				nextAngle = 360 - totalAngle;
			}

			angleOnPerimeter = (180 - nextAngle) / 2;

			float C = 90f;
			float c = spawnRadius / Sin(angleOnPerimeter) * Sin(nextAngle);
			float Y = 90 - angleOnPerimeter;
			float y = (c / Sin(C) * Sin(Y));
			float x = (float)(System.Math.Sqrt(c * c - y * y));

			bool good = false;
			int count = 0;
		
			while(!good)
			{
				if(GoodPoint(nextPoint, x, y))
				{
					good = true;					
				}
				else
				{
					if(GoodPoint(nextPoint, -x, y))
					{
						x *= -1f;
					}
					else if(GoodPoint(nextPoint, x, -y))
					{
						y *= -1f;
					}
					else if(GoodPoint(nextPoint, -x, -y))
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
					Debug.LogError("Infinite loop..... aborting");
					good = true;
				}
			}

			nextPoint.x += x;
			nextPoint.y += y;
		}

		return nextPoint;
	}

	// Checks if the location at point + (x, y, 0) is on the arch of the spawn circle
	// and isn't already a spawn point, using OnArch() and PointExists()
	bool GoodPoint(Vector3 point, float x, float y)
	{
		bool goodPoint = false;
		
		Vector3 nextPoint = new Vector3(point.x + x, point.y + y, 0);

		if(OnArch(nextPoint) && !PointExists(nextPoint))
		{
			goodPoint = true;
		}

		return goodPoint;
	}

	// A better method for calculating sine values for angles which handles converting
	// from degrees to radians
	public float Sin(float angle)
	{
		double a = DegreesToRadians(angle);
		angle = (float)System.Math.Sin(a);
		return angle;
	}

	public double DegreesToRadians(double angle)
	{
		return angle * (System.Math.PI / 180);
	}

	// Checks if the point parameter already exists in the spawns list
	bool PointExists(Vector3 point)
	{
		bool exists = false;

		float x1 = (float)System.Math.Round(point.x, 3);
		float y1 = (float)System.Math.Round(point.y, 3);

		for(int i = 0; i < spawns.Count; i++)
		{
			float x2 = (float)System.Math.Round(spawns[i].position.x, 3);
			float y2 = (float)System.Math.Round(spawns[i].position.y, 3);

			if(x1 == x2 && y1 == y2)
			{
				exists = true;
			}
		}

		return exists;
	}

	// Checkes if the point is on the circle of the spawn radius
	bool OnArch(Vector3 nextPoint)
	{
        bool onArch = true;
		float h = System.Math.Abs((nextPoint.x * nextPoint.x) + (System.Math.Abs(nextPoint.y * nextPoint.y)));
		h = (float)System.Math.Round(h);
		if(System.Math.Sqrt(h) != spawnRadius)
		{
			onArch = false;
		}

		return onArch;
	}
}
