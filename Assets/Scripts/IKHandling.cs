using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IKHandling : MonoBehaviour
{
    private Animator _animator;

    public Transform leftHandTarget;
    public Transform rightHandTarget;

    public Vector3 leftPos,  rightPos;
    public Quaternion leftRot, rightRot;

    public Transform weapon;

    // Start is called before the first frame update
    void Start()
    {
         _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnAnimatorIK(int layerIndex)
    {
        leftPos = _animator.GetIKPosition(AvatarIKGoal.LeftHand);
        leftRot = _animator.GetIKRotation(AvatarIKGoal.LeftHand);

        rightPos = _animator.GetIKPosition(AvatarIKGoal.RightHand);
        rightRot = _animator.GetIKRotation(AvatarIKGoal.RightHand);

        weapon.position = rightPos + (leftPos - rightPos) / 2;
        weapon.rotation = Quaternion.Lerp(leftRot , rightRot, 0.1f);

        //Привязка рук к объекту
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        _animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
        _animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);


        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        _animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
        _animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);

    }
}
