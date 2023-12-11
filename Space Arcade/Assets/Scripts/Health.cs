using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer = false;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraAnimation;
    CameraAnimation cameraAnimation;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    private void Awake() {
        cameraAnimation = Camera.main.GetComponent<CameraAnimation>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if(damageDealer != null) {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    private void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            Die();
        }
    }

    private void Die() {
        if(!isPlayer) {
            scoreKeeper.SetScore(scoreKeeper.GetScore() + score);
        }
        else {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    private void PlayHitEffect() {
        if(hitEffect != null) {
            ParticleSystem hitEffectInstance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hitEffectInstance, hitEffectInstance.main.duration + hitEffectInstance.main.startLifetime.constantMax);

        }
    }

    private void ShakeCamera() {
        if(cameraAnimation != null && applyCameraAnimation) {
            cameraAnimation.PlayCameraAnimation();
        }
    }

    public int GetHealth() {
        return health;
    }
}
