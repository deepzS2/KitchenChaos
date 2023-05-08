using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Scripts.ScriptableObjects
{
    // [CreateAssetMenu()]
    public class RecipeListSO : ScriptableObject
    {
        public List<RecipeSO> RecipeSOList;
    }
}