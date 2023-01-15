using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneswitch2 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
             SceneManager.LoadScene(5);
        if (collision.gameObject.CompareTag("Untagged"))
             SceneManager.LoadScene(4);
    }
}
