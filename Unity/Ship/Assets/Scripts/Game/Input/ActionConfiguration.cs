using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Ship.Input
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
