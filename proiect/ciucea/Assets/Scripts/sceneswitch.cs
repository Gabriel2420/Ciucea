using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneswitch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
             SceneManager.LoadScene(3);
        if (collision.gameObject.CompareTag("Untagged"))
             SceneManager.LoadScene(2);
    }
}
