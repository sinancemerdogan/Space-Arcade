using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    private bool isFiring;
    Coroutine firingCoroutine;

    AudioPlayer audioPlayer;

    private void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
     if(useAI) {
            isFiring = true;
        }   
    }

    void Update()
    {
        Fire();
    }

    private void Fire() {
        if(isFiring && firingCoroutine == null) {
            firingCoroutine = StartCoroutine(FireCoroutine());
        }
        else if (!isFiring && firingCoroutine != null) {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
        
    }

    IEnumerator FireCoroutine() {
        while(true) {
            GameObject projectileInstance = Instantiate(projectilePrefab, 
                                                        transform.position, 
                                                        Quaternion.identity);

            Rigidbody2D projectileRigidBody2D = projectileInstance.GetComponent<Rigidbody2D>();
            if(projectileRigidBody2D != null) {
                projectileRigidBody2D.velocity = transform.up * projectileSpeed;
            }

            Destroy(projectileInstance, projectileLifetime);
            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }

    public bool GetIsFiring() {
        return isFiring;
    }

    public void SetIsFiring(bool isFiring) {
        this.isFiring = isFiring;
    }
}
