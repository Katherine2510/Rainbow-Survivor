using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
     private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayRun()
    {
        _animator.SetBool("isCatch", false);
    }

    public void PlayCatch()
    {
        _animator.SetBool("isCatch", true);
    }
}
