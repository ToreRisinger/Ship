using Game.Model;
using Ship.Constants;
using Ship.Game.Input;
using Ship.Game.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Ship.Game.GameObject
{
    public class Character : MonoBehaviour
    {

        private bool isThisPlayer = false;

        private Rigidbody2D rb;
        private Animator animator;
        private ActionManager actionManager;

        private EDirection direction;
        private Vector2 velocityVector;
        private bool isRunning;


        public void init(CharacterTp character, bool isThisPlayer)
        {
            actionManager = GameManager.instance.getActionManager();

            this.isThisPlayer = isThisPlayer;
            transform.position = new Vector2(character.position.X, character.position.Y);

            direction = EDirection.DOWN;
            velocityVector = Vector2.zero;
            isRunning = false;

            if (isThisPlayer)
            {
                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                rb.freezeRotation = true;
                rb.gravityScale = 0;

                CapsuleCollider2D collider = gameObject.AddComponent<CapsuleCollider2D>();
                collider.offset = new Vector2(0, (float)0.3);
                collider.size = new Vector2(2, 1);
            }
        }

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            float delta = Time.deltaTime;
            if (isThisPlayer)
            {
                HashSet<EPlayerAction> actions = actionManager.getActions();
                calculateDirection(actions);
                calculateVelocityVector(actions, delta);
            }

            isRunning = velocityVector.magnitude > 0;

            updateAnimator();
        }

        void FixedUpdate()
        {
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = currentPosition + velocityVector;
            if (isThisPlayer)
            {
                rb.MovePosition(newPosition);
            }
            else
            {
                transform.position = newPosition;
            }

            
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

            Vector2 newPosition = new Vector2(transform.position.x, transform.position.y);
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

        private void updateAnimator()
        {
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("leftDirection", direction == EDirection.DOWN_LEFT || direction == EDirection.LEFT || direction == EDirection.UP_LEFT);
            animator.SetBool("rightDirection", direction == EDirection.DOWN_RIGHT || direction == EDirection.RIGHT || direction == EDirection.UP_RIGHT);
            animator.SetBool("upDirection", direction == EDirection.UP_RIGHT || direction == EDirection.UP || direction == EDirection.UP_LEFT);
            animator.SetBool("downDirection", direction == EDirection.DOWN_LEFT || direction == EDirection.DOWN || direction == EDirection.DOWN_RIGHT);

                /*
                animator.SetBool("leftDirection", velocityVector.x < 0);
                animator.SetBool("rightDirection", velocityVector.x > 0);
                animator.SetBool("upDirection", velocityVector.y > 0);
                animator.SetBool("downDirection", velocityVector.y < 0);
                */

            
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

        /*
        public void Move(HashSet<EPlayerAction> actions, float delta)
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

            Vector2 newPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 direction = new Vector2(inputDirection.x, inputDirection.y);

            float modifiedMoveSpeed = Stats.CHARACTER_SPEED;

            if (direction.magnitude > 0)
            {
                Vector2 directionNormalized = direction.normalized;
                newPosition += directionNormalized * modifiedMoveSpeed * delta;
            }

            if (isThisPlayer)
            {
                rb.MovePosition(newPosition);
            }
            else
            {
                transform.position = newPosition;
            }
        }
        */
    }
}
