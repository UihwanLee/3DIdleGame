using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field:Header("Animation")]
    [field:SerializeField] public PlayerAnimationData AnimationData { get; set; }

    public Animator animator;

    private void Awake()
    {
        AnimationData.Initialize();
        animator = GetComponentInChildren<Animator>();
    }
}
