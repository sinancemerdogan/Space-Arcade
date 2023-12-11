using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = 1f;

    private void Awake() {
        ManageSingleton();
    }

    private void ManageSingleton() {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if(instanceCount > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip() {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip() {
        PlayClip(damageClip, damageVolume);
    }

    private void PlayClip(AudioClip audioClip, float volume) {
        if(audioClip != null) {
            Vector3 cameraPosition = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(audioClip, cameraPosition, volume);
        }
    }

}
