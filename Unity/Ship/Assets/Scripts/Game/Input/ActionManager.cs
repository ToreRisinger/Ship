using Ship.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Ship.Game.Input
{
    public class ActionManager : MonoBehaviour
    {

        private static HashSet<EPlayerAction> actionActivations;
        private void Awake()
        {
            Log.debug("ActionManager.Awake");
            actionActivations = new HashSet<EPlayerAction>();
        }

        private void FixedUpdate()
        {
            actionActivations = new HashSet<EPlayerAction>();

            foreach (ActionConfiguration actionConfiguration in ActionConfigurations.getActionConfigurations())
            {
                if ((actionConfiguration.isActivation && InputManager.isKeyPressed(actionConfiguration.key)) || (!actionConfiguration.isActivation && InputManager.isKeyHold(actionConfiguration.key)))
                {
                    Log.info(actionConfiguration.action.ToString());
                    actionActivations.Add(actionConfiguration.action);
                }
            }
        }
    }
}

