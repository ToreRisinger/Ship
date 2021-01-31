using Ship.Shared.Utilities;
using System.Collections.Generic;
using UnityEngine;


namespace Ship.Input
{
    public class InputManager : MonoBehaviour
    {

        private static List<KeyCode> keyList = new List<KeyCode> {
        KeyCode.Alpha0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,

        KeyCode.Q,
        KeyCode.W,
        KeyCode.E,
        KeyCode.R,
        KeyCode.T,
        KeyCode.Y,
        KeyCode.U,
        KeyCode.I,
        KeyCode.O,
        KeyCode.P,

        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,

        KeyCode.Z,
        KeyCode.X,
        KeyCode.C,
        KeyCode.V,
        KeyCode.B,
        KeyCode.N,
        KeyCode.M,

        KeyCode.LeftControl,
        KeyCode.RightControl,
        KeyCode.Space,
        KeyCode.KeypadEnter,
        KeyCode.LeftShift,
        KeyCode.RightShift,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
    };

        private static HashSet<KeyCode> holdKeys;
        private static HashSet<KeyCode> pressedKeys;

        private static HashSet<KeyCode> tmpHoldKeys;
        private static HashSet<KeyCode> tmpPressedKeys;

        private bool updateRanBetweenFixed = false;

        private void Awake()
        {
            Log.debug("InputManager.Awake");
            holdKeys = new HashSet<KeyCode>();
            pressedKeys = new HashSet<KeyCode>();

            tmpHoldKeys = new HashSet<KeyCode>();
            tmpPressedKeys = new HashSet<KeyCode>();
        }

        private void FixedUpdate()
        {
            if (!updateRanBetweenFixed)
            {
                pressedKeys = new HashSet<KeyCode>();
            }
            else
            {
                holdKeys = tmpHoldKeys;
                pressedKeys = tmpPressedKeys;
            }

            //Reset tmpMaps
            tmpHoldKeys = new HashSet<KeyCode>();
            tmpPressedKeys = new HashSet<KeyCode>();

            updateRanBetweenFixed = false;
        }

        private void Update()
        {
            updateRanBetweenFixed = true;

            for (int i = 0; i < keyList.Count; i++)
            {
                KeyCode key = keyList[i];
                if (UnityEngine.Input.GetKey(key))
                {
                    if (!holdKeys.Contains(key))
                    {
                        tmpPressedKeys.Add(key);
                    }

                    tmpHoldKeys.Add(key);
                }
            }
        }

        public static bool isKeyHold(KeyCode keyCode)
        {
            return holdKeys.Contains(keyCode);
        }

        public static bool isKeyPressed(KeyCode keyCode)
        {
            return pressedKeys.Contains(keyCode);
        }
    }
}
