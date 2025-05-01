using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    
    public void SetBoolean(string animationType, bool value)
    {
        animator.SetBool(animationType,value);
       
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}