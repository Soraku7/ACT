using System;
using Input;
using Manager;
using UnityEngine;

namespace Character
{
    public class PlayerClimbControl : MonoBehaviour
    {
        [SerializeField, Header("检测")] private float detectionDistance;
        [SerializeField] private LayerMask detectionLayer;

        private RaycastHit _hit;

        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            CharacterClimbInput();
        }

        private bool CanClimb()
        {
            return Physics.Raycast(transform.position + (transform.up * 0.5f), transform.forward, out _hit ,detectionDistance,
                detectionLayer , QueryTriggerInteraction.Ignore);
        }

        private void CharacterClimbInput()
        {
            if (!CanClimb()) return;

            if (GameInputManager.MainInstance.Climb)
            {
                var position = Vector3.zero;
                var rotation = Quaternion.LookRotation(-_hit.normal);
                position.Set(_hit.point.x, _hit.collider.bounds.size.y - (_hit.point.y  * 2), _hit.point.z);
               
                Debug.Log(_hit.collider.bounds.size.y);
                switch (_hit.collider.tag)
                {
                    case "MiddleWall":
                        ToCallEvent(position , rotation);
                        _animator.CrossFade("Climb" ,0 , 0 , 0);
                        break;
                    case "HighWall":
                        ToCallEvent(position , rotation);
                        _animator.CrossFade("Climb" ,0 , 0 , 0);
                        break;
                }
            }
        }

        private void ToCallEvent(Vector3 position , Quaternion rotation)
        {
            GameEventManager.MainInstance.CallEvent("SetAnimationMatchInfo" , position , rotation);
            GameEventManager.MainInstance.CallEvent("EnableCharacterGravity" , false);
        }
    }
}