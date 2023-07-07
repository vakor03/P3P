namespace _Scripts.Helpers
{
    public static class MathHelper
    {
        public static float GetParabolaHeight(float startY, float maxHeight, float t)
        {
            float a = -4;
            float b = 4;

            float curY = (a * t * t + b * t) * maxHeight + startY;

            return curY;
        }
    }
}