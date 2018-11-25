using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject laser;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float laserFireRate = 0.5f;

    Coroutine shootCoroutine;

    float xMin, xMax;
    float yMin, yMax;
    float padding = 0.5f;

    [Header("Sound Effects")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float soundVolume = 1f;

    // Unity Methods

    private void Start() {

        SetMoveBoundaries();
    }

    private void Update() {

        Move();
        Shoot();
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

        FindObjectOfType<LevelManager>().LoadGameOver();
        PlayDeathSound();
        Destroy(gameObject);
    }

    // Player Private Methods

    private void Move() {

        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed * 0.5f;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);

    }

    private void Shoot() {

        if (Input.GetButtonDown("Fire1")) {

            shootCoroutine = StartCoroutine(ShootContinuously());

        }

        if (Input.GetButtonUp("Fire1")) {

            StopCoroutine(shootCoroutine);
        }
    }

    IEnumerator ShootContinuously() {

        while (true)
        {
            GameObject newLaser = Instantiate(laser, transform.position, Quaternion.identity);
            newLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            PlayShootSound();
            yield return new WaitForSeconds(laserFireRate);
        }
    }

    private void SetMoveBoundaries() {

        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void PlayShootSound() {

        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, soundVolume);
    }

    private void PlayDeathSound() {

        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, soundVolume);
    }

    public int GetHealth() {

        return health;
    }
}
