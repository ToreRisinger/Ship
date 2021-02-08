using Game.Model;
using Ship.Constants;
using Ship.Game.Input;
using Ship.Network.Transport;
using System.Collections.Generic;
using UnityEngine;

namespace Ship.Game.GameObject
{
    public class LocalCharacter : CharacterBase
    {
        private Rigidbody2D rb;
        private ActionManager actionManager;

        void Awake()
        {
            base.Awake();

            actionManager = GameManager.instance.getActionManager();

            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb.freezeRotation = true;
            rb.gravityScale = 0;

            CapsuleCollider2D collider = gameObject.AddComponent<CapsuleCollider2D>();
            collider.offset = new Vector2(0, (float)0.3);
            collider.size = new Vector2(2, 1);

            animator = GetComponent<Animator>();
        }

        void Update()
        {
            float delta = Time.deltaTime;

            HashSet<EPlayerAction> actions = actionManager.getActions();
            calculateDirection(actions);
            calculateVelocityVector(actions, delta);

            isRunning = velocityVector.magnitude > 0;

            updateAnimator();
        }

        void FixedUpdate()
        {
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = currentPosition + velocityVector;
            
            rb.MovePosition(newPosition);
        }

        public override void updateState(CharacterUpdate characterUpdate)
        {
            //Do nothing
        }


        private void calculateDirection(HashSet<EPlayerAction> actions)
        {
            bool up = actions.Contains(EPlayerAction.UP);
            bool left = actions.Contains(EPlayerAction.LEFT);
            bool right = actions.Contains(EPlayerAction.RIGHT);
            bool down = actions.Contains(EPlayerAction.DOWN);

            if (up || down || left || right)
            {
                direction = getDirection(up, down, left, right);
            }
        }

        private void calculateVelocityVector(HashSet<EPlayerAction> actions, float delta)
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

            Vector2 direction = new Vector2(inputDirection.x, inputDirection.y);

            float moveSpeed = Stats.CHARACTER_SPEED;

            if (direction.magnitude > 0)
            {
                Vector2 directionNormalized = direction.normalized;
                velocityVector = directionNormalized * moveSpeed * delta;
            } 
            else
            {
                velocityVector = Vector2.zero;
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

            return EDirection.UP;
        }
    }
}
