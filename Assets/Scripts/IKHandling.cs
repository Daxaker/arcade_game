using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IKHandling : MonoBehaviour
{
    private Animator _animator;

    private Vector3 leftPos,  rightPos;

    public Transform weapon;

    private WeaponHandles _weaponHandles;
    void Start()
    {
         _animator = GetComponentInChildren<Animator>();

         _weaponHandles = weapon.gameObject.GetComponentInChildren<WeaponHandles>();
    }
    
    private void OnAnimatorIK(int layerIndex)
    {
        leftPos = _animator.GetIKPosition(AvatarIKGoal.LeftHand);
        rightPos = _animator.GetIKPosition(AvatarIKGoal.RightHand);

        //Set weapon origin between hands
        weapon.position = rightPos + (leftPos - rightPos) / 2;
        
        //Get direction along hand-to-hand Vector
        var aimPoint = (rightPos + (leftPos - rightPos).normalized );
        var rot = Quaternion.LookRotation(aimPoint - weapon.position);
       
        weapon.rotation = rot;
        
        SetIKParams(AvatarIKGoal.LeftHand, _weaponHandles.GetLeftHandle());
        SetIKParams(AvatarIKGoal.RightHand, _weaponHandles.GetRightHandle());
    }

    //Bind hands animation to handle object on weapon
    private void SetIKParams(AvatarIKGoal goal, Vector3 handlePosition)
    {
        _animator.SetIKPositionWeight(goal, 1);
        _animator.SetIKRotationWeight(goal, 1);
        _animator.SetIKPosition(goal, handlePosition);   
    }
}
