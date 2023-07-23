using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] GameObject[] fighterParts;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Finish":
                Debug.Log("The game is over and you didn't screw it up");
                GetComponent<PlayerController>().enabled = false;
                break;
            default:
                StartCrashSequence();
                break;
        }
        
    }

    void StartCrashSequence()
    {
        TurnOffParts();
        crashVFX.Play();
        audioSource.PlayOneShot(explosionSFX);
        GetComponent<PlayerController>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void TurnOffParts()
    {
        int i = 0;
        for (i = 0; i < fighterParts.Length; ++i)
        {
            fighterParts[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
