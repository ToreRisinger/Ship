using Ship.Network.Transport;
using UnityEngine;

namespace Ship.Game.GameObject
{
    public class Character : CharacterBase
    {

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            float delta = Time.deltaTime;

            isRunning = velocityVector.magnitude > 0;

            updateAnimator();
        }

        void FixedUpdate()
        {

        }

        public override void updateState(CharacterUpdate characterUpdate)
        {
            //TODO should put new data in queue and handle it in update or fixed
            direction = characterUpdate.direction;
            transform.position = new Vector2(characterUpdate.position.X, characterUpdate.position.Y);
        }
    }
}
