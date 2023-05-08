using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Scripts.ScriptableObjects
{
    [CreateAssetMenu()]
    public class RecipeSO : ScriptableObject
    {
        public List<KitchenObjectSO> KitchenObjectSOList;
        public string RecipeName;
    }
}