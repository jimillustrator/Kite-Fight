using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] GameObject[] fighterParts;

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        TurnOffParts();
        crashVFX.Play();
        GetComponent<PlayerController>().enabled = false;
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
