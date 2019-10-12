using System;
namespace CXEasyAsset
{
    class CXMathFunctions
    {
        public static bool CheckFloatInRange(float x, float Min,float Max){
            return (x >= Min && x <= Max);
        }
        public static float Map(float val, float in_min, float in_max, float out_min, float out_max)
        {
            //this method maps a value between in range to out range
            return ((val - in_min) * (out_max - out_min)) / (in_max - in_min) + out_min;
        }
        public static bool LineIntersection(float x1, float x2, float x3, float x4, float y1, float y2, float y3, float y4, out float t, out float u)
        {
                //write the line intersection
            float t_up = (x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4);
            float u_up = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3));
            float den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
                //calculate
            t = t_up / den;
            u = u_up / den;
                //make boolean and check
            bool t_Bool = CheckFloatInRange(t, 0f, 1f);
            bool u_Bool = CheckFloatInRange(u, 0f, 1f);
                //making the bool to the things
            return (t_Bool && u_Bool);
        }
    }
}