using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int health = 100;
    [SerializeField] float shootCounter;
    [SerializeField] float minTimeBetweenShots = 0.5f;
    [SerializeField] float maxTimeBetweenShots = 2f;
    [SerializeField] int points = 100;

    [SerializeField] GameObject enemyLaser;
    [SerializeField] GameObject explosionParticles;
    float explosionDuration = 1f;

    float projectileSpeed = 20f;

    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float soundVolume = 1f;

    private void Start() {

        shootCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update() {

        countDownAndShoot();
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



    private void countDownAndShoot() {

        shootCounter -= Time.deltaTime;

        if (shootCounter <= 0) {

            Shoot();
            shootCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Shoot() {

        var newLaser = Instantiate(enemyLaser, transform.position, Quaternion.identity);

        newLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

        PlayShootSound();
    }

    private void Die() {

        FindObjectOfType<GameSession>().AddToScore(points);

        var explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);

        Destroy(gameObject);

        PlayDeathSound();
    }

    private void PlayShootSound() {

        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, soundVolume);
    }

    private void PlayDeathSound() {

        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, soundVolume);
    }
}
