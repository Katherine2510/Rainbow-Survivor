using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
     public Animator _animator;


    public void PlayRun()
    {
        _animator.SetBool("isCatch", false);
    }

    public void PlayCatch()
    {
        _animator.SetBool("isCatch", true);
    }
}
