using Game.Model;
using Ship.Constants;
using Ship.Network.Transport;
using System.Collections.Generic;
using UnityEngine;

namespace Ship.Game.GameObject
{
    public class Character : CharacterBase
    {

        private Queue<CharacterPositionUpdate> positions;
        private static int INTERPOLATION_MIDDLE_NUMBER = 5;

        private CharacterPositionUpdate nextPosition;

        private float currentInterpolationTime;

        protected override void AwakeInternal()
        {
            positions = new Queue<CharacterPositionUpdate>();
            currentInterpolationTime = 0.0f;
        }

        protected override void UpdateInternal()
        {
            calcInterpolatedPosition(Time.deltaTime);
            updateAnimator(isRunning, direction);
        }

        protected override void FixedUpdateInternal()
        {
            
        }
        

        public override void updateState(CharacterPositionUpdate characterUpdate)
        {
            positions.Enqueue(characterUpdate);
        }

        private void calcInterpolatedPosition(float delta)
        {
            if (positions.Count > 0 && nextPosition == null)
            {
                nextPosition = positions.Dequeue();
                direction = nextPosition.direction;
                isRunning = nextPosition.isRunning;
            }


            if (nextPosition != null)
            {

                float baseTimeBetweenPoints = 1.0f / 30.0f;
                float ratioToProcess = (delta / baseTimeBetweenPoints) * (positions.Count / INTERPOLATION_MIDDLE_NUMBER);

                float moveSpeed = Stats.CHARACTER_SPEED;

                currentInterpolationTime += ratioToProcess;

                while (true)
                {
                    Vector2 directionVector = getDirectionVector(direction);
                    Vector2 directionNormalized = directionVector.normalized;
                    Vector2 velocityVector = directionNormalized * moveSpeed * delta * ratioToProcess;

                    if (currentInterpolationTime >= 1)
                    {
                        setPosition(new Vector2(nextPosition.position.X, nextPosition.position.Y));

                        if (positions.Count > 0)
                        {
                            nextPosition = positions.Dequeue();
                            direction = nextPosition.direction;
                            isRunning = nextPosition.isRunning;
                            currentInterpolationTime -= 1;
                            ratioToProcess = currentInterpolationTime;
                            continue;
                        }
                        else
                        {
                            currentInterpolationTime = 0;
                            nextPosition = null;
                            break;
                        }
                    }
                    else
                    {
                        setPosition(transform.position + new Vector3(velocityVector.x, velocityVector.y, 0));
                        break;

                    }
                }
            }
        }

        private void setPosition(Vector2 position)
        {
            if (isRunning)
            {
                transform.position = new Vector3(position.x, position.y, 0);
            }
        }

        private Vector2 getDirectionVector(EDirection direction)
        {
            switch(direction)
            {
                case EDirection.DOWN: return new Vector2(0, -1);
                case EDirection.UP: return new Vector2(0, 1);
                case EDirection.LEFT: return new Vector2(-1, 0);
                case EDirection.RIGHT: return new Vector2(1, 0);
                case EDirection.DOWN_RIGHT: return new Vector2(1, -1);
                case EDirection.UP_RIGHT: return new Vector2(1, 1);
                case EDirection.DOWN_LEFT: return new Vector2(-1, -1);
                case EDirection.UP_LEFT: return new Vector2(-1, 1);
                default: return new Vector2(0, -1);
            }
        }
    }
}
