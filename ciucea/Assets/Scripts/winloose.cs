using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winloose : MonoBehaviour
{
    public AudioSource win;
    public AudioSource lose;
    public void Playwin()
    {
        win.Play();
    }
    public void Playlose()
    {
        lose.Play();
    }
}
