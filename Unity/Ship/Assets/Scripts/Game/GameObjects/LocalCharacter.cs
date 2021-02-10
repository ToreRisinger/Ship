using Game.Model;
using Ship.Constants;
using Ship.Network.Transport;
using System.Collections.Generic;
using UnityEngine;

namespace Ship.Game.GameObject
{
    public class LocalCharacter : CharacterBase
    {
        private Rigidbody2D rb;
        private Vector2 velocityVector;

        protected override void AwakeInternal()
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb.freezeRotation = true;
            rb.gravityScale = 0;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;

            CapsuleCollider2D collider = gameObject.AddComponent<CapsuleCollider2D>();
            collider.offset = new Vector2(0, (float)0.3);
            collider.size = new Vector2(2, 1);

            animator = GetComponent<Animator>();

            velocityVector = Vector2.zero;
        }

        protected override void UpdateInternal()
        {
            HashSet<EPlayerAction> actions = getActions();

            direction = calculateDirection(actions);
            velocityVector = calculateVelocityVector(Time.deltaTime, actions);

            isRunning = velocityVector.magnitude > 0;

            updateAnimator(isRunning, direction);
        }

        protected override void FixedUpdateInternal()
        {
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = currentPosition + velocityVector;
            
            rb.MovePosition(newPosition);
        }

        private EDirection calculateDirection(HashSet<EPlayerAction> actions)
        {
            bool up = actions.Contains(EPlayerAction.UP);
            bool left = actions.Contains(EPlayerAction.LEFT);
            bool right = actions.Contains(EPlayerAction.RIGHT);
            bool down = actions.Contains(EPlayerAction.DOWN);

            if (up || down || left || right)
            {
                return getDirection(up, down, left, right);
            } else
            {
                return direction;
            }
        }

        protected Vector2 calculateVelocityVector(float delta, HashSet<EPlayerAction> actions)
        {
            Vector2 inputDirection = Vector2.zero;
            if (actions.Contains(EPlayerAction.UP))
            {
                inputDirection.y += 1;
            }
            if (actions.Contains(EPlayerAction.LEFT))
            {
                inputDirection.x -= 1;
            }
            if (actions.Contains(EPlayerAction.DOWN))
            {
                inputDirection.y -= 1;
            }
            if (actions.Contains(EPlayerAction.RIGHT))
            {
                inputDirection.x += 1;
            }

            Vector2 directionVector = new Vector2(inputDirection.x, inputDirection.y);

            float moveSpeed = Stats.CHARACTER_SPEED;

            if (directionVector.magnitude > 0)
            {
                Vector2 directionNormalized = directionVector.normalized;
                return directionNormalized * moveSpeed * delta;
            } 
            else
            {
                return Vector2.zero;
            }
        }

        private EDirection getDirection(bool up, bool down, bool left, bool right)
        {
            if (up && !left && !right && !down)
            {
                return EDirection.UP;
            }
            else if (up && !left && right && !down)
            {
                return EDirection.UP_RIGHT;
            }
            else if (!up && !left && right && !down)
            {
                return EDirection.RIGHT;
            }
            else if (!up && !left && right && down)
            {
                return EDirection.DOWN_RIGHT;
            }
            else if (!up && !left && !right && down)
            {
                return EDirection.DOWN;
            }
            else if (!up && left && !right && down)
            {
                return EDirection.DOWN_LEFT;
            }
            else if (!up && left && !right && !down)
            {
                return EDirection.LEFT;
            }
            else if (up && left && !right && !down)
            {
                return EDirection.UP_LEFT;
            }

            if(right && left)
            {
                return EDirection.LEFT;
            } else
            {
                return EDirection.DOWN;
            }

            
        }

        public override void updateState(CharacterPositionUpdate characterUpdate)
        {
            // Do nothing
        }
    }
}
