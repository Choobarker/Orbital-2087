using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private const float BOOST_DROP_RATE = 20;

    public float health = 2f;
	public float speed;
	public float playerViewDistance;
	private float moveTime;
	private float timeMoved = 0f;	
	private float minMovePercent = 10;
	private float maxMovePercent = 50;

    private bool alive = false;
	private bool inPosition = false;

	public Transform bulletProjectile;
	public Transform explosion;
	public Transform hitSplash;
    public Transform fireRateBoost;
    public Transform shieldBoost;
    public Transform speedBoost;

	private Vector3 direction;
	
	void Start()
	{
		alive = true;

		SetDirection();
		SetMovementTime();
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
            ScoreKeeping.ScoreValue += 10;
			DestroyEnemy();
		}
	}

	void OnTriggerEnter(Collider other)
    {
        //checks if it is colliding with a shot from the player
        if (other.tag.Equals("Shot") == true)
        {
			Destroy(Instantiate(hitSplash, other.transform.position, other.transform.rotation).gameObject, 2);
            Destroy(other.gameObject);
            TakeDamage(1f);
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
}
