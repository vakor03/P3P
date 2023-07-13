using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Helpers
{
    public static class Helper
    {
        private static Dictionary<float, WaitForSeconds> _waitForSecondsCache = new();

        public static WaitForSeconds GetCachedWaitForSeconds(float time)
        {
            if (_waitForSecondsCache.TryGetValue(time, out var waitForSeconds))
            {
                return waitForSeconds;
            }

            _waitForSecondsCache.Add(time, new WaitForSeconds(time));
            return _waitForSecondsCache[time];
        }

        private static PointerEventData _eventDataCurrentPosition;
        private static List<RaycastResult> _results;

        public static bool IsOverUI()
        {
            _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            _results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
            return _results.Count > 0;
        }

        public static bool ContainsLayer(this LayerMask layerMask, int layer)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}