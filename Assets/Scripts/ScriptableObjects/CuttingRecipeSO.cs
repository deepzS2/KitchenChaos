using UnityEngine;

namespace KitchenChaos.Scripts.ScriptableObjects
{
    [CreateAssetMenu()]
    public class CuttingRecipeSO : ScriptableObject
    {
        public KitchenObjectSO Input;
        public KitchenObjectSO Output;
        public int CuttingProgressMax;
    }
}