/*
 * Author: Muhammad Farhan
 * Date: 29/7/2024
 * Description: Script related the scene changer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// scene index to change to
    /// </summary>
    public int targetSceneIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(targetSceneIndex);
        }
    }
}
