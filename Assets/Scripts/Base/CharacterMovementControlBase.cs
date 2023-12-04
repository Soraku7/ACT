using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Base
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class CharacterMovementControlBase : MonoBehaviour
    {
        protected CharacterController Controller;
        protected Animator Anim;

        //地面检测
        protected bool CharacterIsOnGround;
        [SerializeField , Header("地面检测")]
        protected float GroundDetectionPositionOffset;
        [SerializeField]
        protected float DetectionRange;
        [SerializeField]
        protected LayerMask GroundLayer;

        //重力
        protected readonly float CharacterGravity = -9.8f;
        protected float CharacterVerticalVelocity;
        protected float FailOutDeltaTime;
        protected float FailOutTime = 0.15f;
        protected readonly float CharacterVerticalMaxVelocity = 54f;
        protected Vector3 CharacterVerticalDirection;
        protected virtual void Awake()
        {
            Controller = GetComponent<CharacterController>();
            Anim = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            FailOutDeltaTime = FailOutTime;
        }

        protected virtual void Update()
        {
            SetCharacterGravity();
            UpdateCharacterGravity();
        }

        /// <summary>
        /// 地面检测
        /// </summary>
        private bool GroundDetection()
        {
            var position = transform.position;
            var detectPosition = new Vector3(position.x
                , position.y - GroundDetectionPositionOffset, position.z);

            return Physics.CheckSphere(detectPosition, DetectionRange, GroundLayer, QueryTriggerInteraction.Ignore);
        }

        /// <summary>
        /// 重力
        /// </summary>
        private void SetCharacterGravity()
        {
            CharacterIsOnGround = GroundDetection();
            if (CharacterIsOnGround)
            {
                FailOutDeltaTime = FailOutTime;

                if (CharacterVerticalVelocity < 0)
                {
                    //放置第二次下落速度仍然叠加
                    CharacterVerticalVelocity = -2f;
                }
            }
            else
            {
                if (FailOutDeltaTime > 0)
                {
                    FailOutDeltaTime -= Time.deltaTime;
                }
                else
                {
                    
                }

                if (CharacterVerticalVelocity < CharacterVerticalMaxVelocity)
                {
                    CharacterVerticalVelocity += CharacterGravity * Time.deltaTime;
                }
            }
        }

        private void UpdateCharacterGravity()
        {
            CharacterVerticalDirection.Set(0 , CharacterVerticalVelocity , 0);
            Controller.Move(CharacterVerticalDirection * Time.deltaTime);
        }

        /// <summary>
        /// 坡道检测
        /// </summary>
        private Vector3 SlopResetDirection(Vector3 moveDirection)
        {
            if (Physics.Raycast(transform.position + (transform.up * 5.0f),
                    Vector3.down, out var hit , Controller.height * 0.85f 
                    , GroundLayer , QueryTriggerInteraction.Ignore))
            {
                if (Vector3.Dot(Vector3.up, hit.normal) != 0)
                {
                    return Vector3.ProjectOnPlane(moveDirection, hit.normal);
                }
            }

            return moveDirection;
        }

        private void OnDrawGizmos()
        {
            var position = transform.position;
            var detectPosition = new Vector3(position.x
                , position.y - GroundDetectionPositionOffset, position.z);
            
            Gizmos.DrawWireSphere(detectPosition , DetectionRange);
        }
    }
}
