using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class AnimationMatch : StateMachineBehaviour
{
    [SerializeField , Header("匹配信息")] private float startTime;
    [SerializeField] private float endTime;
    [SerializeField] private AvatarTarget avatarTarget;

    [SerializeField , Header("激活重力")] private bool isEnableGravity;
    [SerializeField] private float enableTime;

    private Vector3 _matchPosition;
    private Quaternion _matchQuaternion;

    private void OnEnable()
    {
        GameEventManager.MainInstance.AddEventListening<Vector3 , Quaternion>("SetAnimationMatchInfo" , GetMatchInfo);
    }

    private void OnDisable()
    {
        GameEventManager.MainInstance.RemoveEvent<Vector3 , Quaternion>("SetAnimationMatchInfo" , GetMatchInfo);
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.IsInTransition(layerIndex)) return;
        
        if (!animator.isMatchingTarget)
        {
            animator.MatchTarget(_matchPosition, _matchQuaternion, avatarTarget,
                new MatchTargetWeightMask(Vector3.one, 0f), startTime, endTime);
        }

        if (isEnableGravity)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > enableTime)
            {
                GameEventManager.MainInstance.CallEvent("EnableCharacterGravity" , true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    private void GetMatchInfo(Vector3 position, Quaternion rotation)
    {
        _matchPosition = position;
        _matchQuaternion = rotation;
    }
}
