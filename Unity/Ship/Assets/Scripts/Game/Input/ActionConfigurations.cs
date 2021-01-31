using System.Collections.Generic;
using UnityEngine;

namespace Ship.Input
{
    class ActionConfigurations
    {
        private static List<ActionConfiguration> actionConfigurations = new List<ActionConfiguration> {
            new ActionConfiguration(EPlayerAction.ACTIVATE_BELT_1, KeyCode.Alpha1, false),
            new ActionConfiguration(EPlayerAction.ACTIVATE_BELT_2, KeyCode.Alpha2, false),
            new ActionConfiguration(EPlayerAction.ACTIVATE_BELT_3, KeyCode.Alpha3, false),
            new ActionConfiguration(EPlayerAction.ACTIVATE_BELT_4, KeyCode.Alpha4, false),
            new ActionConfiguration(EPlayerAction.ACTIVATE_BELT_5, KeyCode.Alpha5, false),
            new ActionConfiguration(EPlayerAction.ACTIVATE_BELT_6, KeyCode.Alpha6, false),
            new ActionConfiguration(EPlayerAction.ACTIVATE_BELT_7, KeyCode.Alpha7, false),
            new ActionConfiguration(EPlayerAction.ACTIVATE_BELT_8, KeyCode.Alpha8, false),
            new ActionConfiguration(EPlayerAction.ACTIVATE_BELT_9, KeyCode.Alpha9, false),


            new ActionConfiguration(EPlayerAction.UP, KeyCode.W, false),
            new ActionConfiguration(EPlayerAction.DOWN, KeyCode.S, false),
            new ActionConfiguration(EPlayerAction.RIGHT, KeyCode.D, false),
            new ActionConfiguration(EPlayerAction.LEFT, KeyCode.A, false),
            new ActionConfiguration(EPlayerAction.HOLD_DIRECTION, KeyCode.LeftControl, false),
            new ActionConfiguration(EPlayerAction.HOLD_POSITION, KeyCode.LeftShift, false)
        };

        public static List<ActionConfiguration> getActionConfigurations()
        {
            return actionConfigurations;
        }
    }
}
