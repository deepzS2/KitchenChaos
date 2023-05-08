using UnityEngine;

namespace KitchenChaos.Scripts.ScriptableObjects
{
    [CreateAssetMenu()]
    public class BurningRecipeSO : ScriptableObject
    {
        public KitchenObjectSO Input;
        public KitchenObjectSO Output;
        public float burningTimerMax;
    }
}