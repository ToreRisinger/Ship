using Game.Model;
using Ship.Network.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Ship.Game.GameObject
{
    public abstract class CharacterBase : MonoBehaviour
    {
        public Animator animator;

        public EDirection direction;
        public Vector2 velocityVector;
        public bool isRunning;

        public void Awake()
        {
            direction = EDirection.DOWN;
            velocityVector = Vector2.zero;
            isRunning = false;
        }
        protected void updateAnimator()
        {
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("leftDirection", direction == EDirection.DOWN_LEFT || direction == EDirection.LEFT || direction == EDirection.UP_LEFT);
            animator.SetBool("rightDirection", direction == EDirection.DOWN_RIGHT || direction == EDirection.RIGHT || direction == EDirection.UP_RIGHT);
            animator.SetBool("upDirection", direction == EDirection.UP_RIGHT || direction == EDirection.UP || direction == EDirection.UP_LEFT);
            animator.SetBool("downDirection", direction == EDirection.DOWN_LEFT || direction == EDirection.DOWN || direction == EDirection.DOWN_RIGHT);
        }

        public abstract void updateState(CharacterUpdate characterUpdate);
    }
}
