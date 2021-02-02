using UnityEngine;

namespace Ship.Game.Input
{
    class ActionConfiguration
    {
        public EPlayerAction action;
        public KeyCode key;
        public bool isActivation;

        public ActionConfiguration(EPlayerAction _action, KeyCode _key, bool _isActivation)
        {
            action = _action;
            key = _key;
            isActivation = _isActivation;
        }
    }
}
