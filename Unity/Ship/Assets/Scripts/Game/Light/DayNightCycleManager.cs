using System;
using UnityEngine;

namespace Game.Graphics

{
    public class DayNightCycleManager : MonoBehaviour
    {
        [Range(0, 10000)]
        public int time = 0;
        public float dayDuration;
        public float nightDuration;
        public bool progressTime = true;

        private static DayNightCycleManager instance;

        private float shadowAlpha = 0;
        private float shadowYScale = 0;
        private float shadowAngle = 0;
        private float globalLightIntensity = 0;
        private Color globalLightColor = new Color(1, 1, 1);

        private int EAST = 90;
        private float SHADOW_MAX_ALPHA = 0.2f;
        private float SHADOW_MAX_Y_SCALE = 3.0f;
        private float GLOBAL_LIGHT_MIN = 0.2f;
        private float GLOBAL_LIGHT_MAX = 1.0f;

        public static DayNightCycleManager getInstance()
        {
            return instance;
        }

        void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }

        void Update()
        {
            if(progressTime)
            {
                time += 1;
            }
           
            if (time >= 10000)
            {
                time = 0;
            }

            calculateShadowAngle();
            calculateShadowAlpha();
            calculateShadowYScale();
            calculateGlobalLightIntensity();
            calculateGlobalLightColor();
        }

        private void calculateShadowAngle()
        {
            float sun = EAST + -180 * time / dayDuration;
            shadowAngle = sun + 360.0f;
        }

        private void calculateShadowAlpha()
        {
            if (time > dayDuration)
            {
                shadowAlpha = 0.0f;
            }
            else
            {
                float halfDay = dayDuration / 2.0f;
                float alphaScale;
                if (time <= halfDay)
                {
                    alphaScale = time / halfDay;
                }
                else
                {
                    alphaScale = (halfDay - (time - halfDay)) / halfDay;

                }
                shadowAlpha = SHADOW_MAX_ALPHA * alphaScale;
            }
        }

        private void calculateShadowYScale()
        {
            if (time > dayDuration)
            {
                shadowYScale = 1.0f;
            }
            else
            {
                float halfDay = dayDuration / 2.0f;
                double shadowLengthScale;
                if (time <= halfDay)
                {
                   
                    shadowLengthScale = Math.Pow(Math.Log(time / halfDay), 2);
                }
                else
                {
                    shadowLengthScale = Math.Pow(Math.Log((halfDay - (time - halfDay)) / halfDay), 2);

                }
                if (shadowLengthScale > SHADOW_MAX_Y_SCALE)
                {
                    shadowLengthScale = SHADOW_MAX_Y_SCALE;
                }

                shadowYScale = 1 + (float)shadowLengthScale;
            }
        }

        private void calculateGlobalLightIntensity()
        {
            if (time > dayDuration)
            {
                globalLightIntensity = GLOBAL_LIGHT_MIN;
            }
            else
            {
                float halfDay = dayDuration / 2.0f;
                double globalLightScale;
                if (time <= halfDay)
                {
                    globalLightScale = time / halfDay;
                }
                else
                {
                    globalLightScale = (halfDay - (time - halfDay)) / halfDay;

                }
                globalLightScale += GLOBAL_LIGHT_MIN;
                if(globalLightScale > GLOBAL_LIGHT_MAX)
                {
                    globalLightScale = GLOBAL_LIGHT_MAX;
                }

                if(globalLightScale < GLOBAL_LIGHT_MIN)
                {
                    globalLightScale = GLOBAL_LIGHT_MIN;
                }

                globalLightIntensity = (float)globalLightScale;
            }
        }

        public void calculateGlobalLightColor()
        {
            Color red = new Color(255, 130, 0);
            Color white = new Color(255, 255, 255);

            if(time <= dayDuration / 6)
            {
                globalLightColor = new Color(1, 1 - 0.41f * (time / (dayDuration / 6)), 1 - (time / (dayDuration / 6)));
                return;
            }
            else if(time <= dayDuration / 4)
            {
                float _time = time - (dayDuration / 6);
                float _compare = (dayDuration / 4) - (dayDuration / 6);
                globalLightColor = new Color(1, 0.59f + 0.41f * (_time / _compare), _time / _compare);
                return;
            }
            else if(time >= 3 * dayDuration / 4 && time <= 5 * dayDuration / 6)
            {
                float _time = time - (3 * dayDuration / 4);
                float _compare = (5 * dayDuration / 6) - (3 * dayDuration / 4);
                globalLightColor = new Color(1, 1 - 0.41f * (_time / _compare), (1 - _time / _compare));
                return;
            }
            else if (time >= 5 * dayDuration / 6 && time <= dayDuration)
            {
                float _time = time - (5 * dayDuration / 6);
                float _compare = dayDuration - (5 * dayDuration / 6);
                globalLightColor = new Color(1, 0.59f + 0.41f * (_time / _compare), _time / _compare);
                return;
            }
        }

        public float getShadowAlpha()
        {
            return shadowAlpha;
        }

        public float getShadowYScale()
        {
            return shadowYScale;
        }
        public float getShadowAngle()
        {
            return shadowAngle;
        }

        public float getGlobalLightIntensity()
        {
            return globalLightIntensity;
        }

        public Color getGlobalLightColor()
        {
            return globalLightColor;
        }

        public int getCurrentTime()
        {
            return time;
        }
    }
}
