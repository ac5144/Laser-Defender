using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour {

    [SerializeField] int health;
    [SerializeField] int points;

    [SerializeField] float movementSpeed;

    [SerializeField] float minTimeBetweenShots;
    [SerializeField] float maxTimeBetweenShots;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed;
    float currentTimeUntilNextShot;

    [SerializeField] GameObject bigLaserPrefab;
    [SerializeField] float bigLaserSpeed;

    int maxBigLaserCount = 3;
    int currentBigLaserCount;

	[SerializeField] List<Transform> allDestinations;

    Movement movement;
    Vector3 currentDestination;

    [SerializeField] List<Wave> sideWaves;

    [SerializeField] Transform startLocation;

    private void Start() {

        movement = gameObject.GetComponent<Movement>();
        movement.SetSpeed(movementSpeed);

        currentBigLaserCount = maxBigLaserCount;

        currentDestination = startLocation.position;

        ResetShootTimer();

        StartCoroutine(StartSpawningWaves());
    }

    private void Update() {

        Move();
        UpdateDestination();
        UpdateShootTimer();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        var other = collision.gameObject;

        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer) {

            return;
        }

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer) {

        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0) {

            Die();
        }
    }

    private void Die() {

        GameObject.FindObjectOfType<GameSession>().AddToScore(points);

        GameObject.FindObjectOfType<LevelManager>().LoadGameWin();

        Destroy(gameObject);
    }

    // Movement

    private void Move() {

        gameObject.GetComponent<Movement>().Move(currentDestination);
    }

    private void GetNewDestination() {

        int newDestinationIndex = Random.Range(0, allDestinations.Count);

        currentDestination = allDestinations[newDestinationIndex].position;
    }

    private void UpdateDestination() {

        if (currentDestination == transform.position) {

            GetNewDestination();
        }
    }

    //Shooting

    private void ResetShootTimer() {

        currentTimeUntilNextShot = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void UpdateShootTimer() {

        if (currentTimeUntilNextShot <= 0) {

            Shoot();
            ResetShootTimer();
        }
        else {

            currentTimeUntilNextShot -= Time.deltaTime;
        }
    }

    private void Shoot() {

        if (currentBigLaserCount <= 0) {

            var newLaser = Instantiate(bigLaserPrefab, transform.position, Quaternion.identity);
            newLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bigLaserSpeed);
        }
        else {

            var newLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            newLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
        }


        UpdateBigLaserCount();
    }

    void ResetBigLaserCounter() {

        currentBigLaserCount = maxBigLaserCount;
    }

    void UpdateBigLaserCount() {

        if (currentBigLaserCount <= 0) {

            ResetBigLaserCounter();
        }
        else {

            currentBigLaserCount--;
        }
    }

    // Spawning Waves

    private IEnumerator StartSpawningWaves() {

        EnemySpawner enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();

        while (health > 0) {

            int randomWaveIndex = Random.Range(0, sideWaves.Count);
            Wave randomWave = sideWaves[randomWaveIndex];

            int randomFormationIndex = Random.Range(0, randomWave.GetFormations().Count);
            var randomFormation = randomWave.GetFormations()[randomFormationIndex];

            yield return StartCoroutine(enemySpawner.SpawnEnemies(randomFormation));
        }
    }
}
