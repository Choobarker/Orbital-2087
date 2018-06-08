using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private const float BOOST_DROP_RATE = 20;
    
	public float speed = 1;
    private float health = 35f;
	private float moveTime;
	private float timeMoved = 0f;
	private float minMovePercent = 10;
	private float maxMovePercent = 50;
    private float score = 15;
    private float minScore = 10;
    private float scoreTimer = 5;
    private float timePassed = 0;

    private bool alive = false;
	private bool inPosition = false;

	public Transform explosion;
	public Transform hitSplash;
    public Transform fireRateBoost;
    public Transform shieldBoost;
    public Transform speedBoost;

	private Vector3 direction;

    private ScoreKeeping scoreKeeping;
	
	void Start()
	{
		alive = true;
        timePassed = Time.time;

		SetDirection();
		SetMovementTime();
        scoreKeeping = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoreKeeping>();
        scoreTimer += Time.deltaTime;
	}
	
	void Update ()
	{
		if(alive && !inPosition)
		{
			transform.position += direction * Time.deltaTime;
			timeMoved += Time.deltaTime;

			if(timeMoved >= moveTime)
			{
				inPosition = true;
			}
		}

        if(score != minScore)
        {
            if(timePassed + 1 <= Time.time)
            {
                score -= 1;
                timePassed += 1;
            }
        }
	}

	// Sets the direction the alien will travel based on its location
	// Effectively, the direction vector is the distance the alien will be moved every second
	// The mod value of .1f and speed 1 means that every second, the alien will be moved 10% of 
	// the player view distance.
	void SetDirection()
	{
		direction = -transform.position;

		float mod = .1f * speed;
		direction.x *= mod;
		direction.y *= mod;
	}

	void SetMovementTime()
	{
		moveTime = Random.Range(minMovePercent / 10, maxMovePercent / 10);
	}

    void TakeDamage(float damage)
    {
        health -= damage;

        CheckHealth();
    }

    void CheckHealth()
	{
		if(health <= 0)
		{
            scoreKeeping.AddScore(score);
			DestroyEnemy();
		}
	}

	void OnTriggerEnter(Collider collider)
    {
        //checks if it is colliding with a shot from the player
        if (collider.tag.Equals("Shot"))
        {
			Destroy(Instantiate(hitSplash, collider.transform.position, collider.transform.rotation).gameObject, 2);
            TakeDamage(collider.GetComponent<ProjectileInfo>().GetDamage());

            if(collider.GetComponent<AudioSource>().isPlaying)
            {
                collider.GetComponent<MeshRenderer>().enabled = false;
                collider.enabled = false;

                Destroy(collider.gameObject, 3); 
            }
            else
            {
                Destroy(collider.gameObject);
            }                       
        }
    }

	void DestroyEnemy()
	{
		Destroy(Instantiate(explosion, transform.position, transform.rotation).gameObject, 2);
        DropBoost();
		Destroy(gameObject);
	}

    void DropBoost()
    {
        float result = Random.Range(1, 100);

        if(result <= BOOST_DROP_RATE)
        {
            Transform drop = null;

            result = Random.Range(1,100);
            
            if(result > 66)
            {
                drop = fireRateBoost;
            }
            else if(result > 33)
            {
                drop = shieldBoost;
            }
            else
            {
                drop = speedBoost;
            }

            Instantiate(drop, transform.position, transform.rotation);
        }
    }

    public void BuffHealth(float buff)
    {
        health += buff;
    }

    public void BuffScore(float buff)
    {
        minScore += buff;
        score += buff;
    }
}
