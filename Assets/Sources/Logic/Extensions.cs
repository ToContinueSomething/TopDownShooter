using UnityEngine;

namespace Sources.Logic
{
    public static class Extensions
    {
        public static bool Compare(this LayerMask layerMask, int layer)
        {
            int layerBit = 1 << layer;
            return (layerMask & layerBit) != 0;
        }
    }
}