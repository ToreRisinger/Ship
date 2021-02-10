using Game.Model;
using Ship.Game.Input;
using Ship.Network.Transport;
using System.Collections.Generic;
using UnityEngine;

namespace Ship.Game.GameObject
{
    public abstract class CharacterBase : MonoBehaviour
    {
        public Animator animator;

        public EDirection direction;
        public bool isRunning;

        protected ActionManager actionManager;

        public void Awake()
        {
            animator = GetComponent<Animator>();
            actionManager = GameManager.instance.getActionManager();

            direction = EDirection.DOWN;
            isRunning = false;

            AwakeInternal();
        }

        public void Update()
        {

            UpdateInternal();
        }

        public void FixedUpdate()
        {

            FixedUpdateInternal();
        }

        protected abstract void AwakeInternal();

        protected abstract void UpdateInternal();

        protected abstract void FixedUpdateInternal();

        //protected abstract void calculateVelocityVectorAndDirection(float delta);

        public abstract void updateState(CharacterPositionUpdate characterUpdate);

        protected HashSet<EPlayerAction> getActions() {
            return actionManager.getActions();
        }

        protected void updateAnimator(bool isRunning, EDirection direction)
        {
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("leftDirection", direction == EDirection.DOWN_LEFT || direction == EDirection.LEFT || direction == EDirection.UP_LEFT);
            animator.SetBool("rightDirection", direction == EDirection.DOWN_RIGHT || direction == EDirection.RIGHT || direction == EDirection.UP_RIGHT);
            animator.SetBool("upDirection", direction == EDirection.UP_RIGHT || direction == EDirection.UP || direction == EDirection.UP_LEFT);
            animator.SetBool("downDirection", direction == EDirection.DOWN_LEFT || direction == EDirection.DOWN || direction == EDirection.DOWN_RIGHT);
        }

        
    }
}
