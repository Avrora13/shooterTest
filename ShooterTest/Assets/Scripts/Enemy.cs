using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void Hit()
    {
        if(animator.GetBool("isUp"))
        {
            animator.SetBool("isUp", false);
        }
    }

    public bool isDown()
    {
        return !animator.GetBool("isUp");
    }

    public void StartGame()
    {
        animator.SetBool("isUp", true);
    }
}
