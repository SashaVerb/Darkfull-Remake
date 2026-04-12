using UnityEngine;

namespace LayerMaskExtensions
{
    public static class LayerMaskExtensions
    {
        public static bool Contains(this LayerMask layerMask, GameObject gameObject)
        {
            return (layerMask.value & (1 << gameObject.layer)) != 0;
        }
    }
}