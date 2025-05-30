// Simple wrapper for controlling player animation booleans through the Animator component.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    
    // Sets a boolean parameter on the Animator by name.
    public void SetBoolean(string animationType, bool value)
    {
        animator.SetBool(animationType,value);
       
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}