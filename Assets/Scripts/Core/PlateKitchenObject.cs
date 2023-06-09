using KitchenChaos.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Scripts.Core
{
    public class PlateKitchenObject : KitchenObject
    {
        public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

        public class OnIngredientAddedEventArgs : EventArgs
        {
            public KitchenObjectSO kitchenObjectSO;
        }

        [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

        private List<KitchenObjectSO> kitchenObjectSOList;

        private void Awake()
        {
            kitchenObjectSOList = new List<KitchenObjectSO>();
        }

        public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
        {
            if (kitchenObjectSOList.Contains(kitchenObjectSO) || !validKitchenObjectSOList.Contains(kitchenObjectSO))
            {
                return false;
            }

            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }

        public List<KitchenObjectSO> GetKitchenObjectSOList()
        {
            return kitchenObjectSOList;
        }
    }
}