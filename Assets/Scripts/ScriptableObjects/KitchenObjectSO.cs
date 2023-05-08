using UnityEngine;

namespace KitchenChaos.Scripts.ScriptableObjects
{
    [CreateAssetMenu()]
    public class KitchenObjectSO : ScriptableObject
    {
        public Transform Prefab;
        public Sprite Sprite;
        public string ObjectName;
    }
}