using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneswitch : MonoBehaviour
{
    [SerializeField] int sceneId;
    [SerializeField] int sceneId2;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
             SceneManager.LoadScene(sceneId);
        if (collision.gameObject.CompareTag("Untagged"))
             SceneManager.LoadScene(sceneId2);
    }
}
