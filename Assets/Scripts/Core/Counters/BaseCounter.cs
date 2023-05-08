using KitchenChaos.Scripts.Core.Interfaces;
using System;
using UnityEngine;

namespace KitchenChaos.Scripts.Core.Counters
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        public static event EventHandler OnAnyObjectPlaced;

        [SerializeField] private Transform counterTopPoint;

        private KitchenObject kitchenObject;

        public static void ResetStaticData()
        {
            OnAnyObjectPlaced = null;
        }

        public virtual void Interact(Player player)
        {
            Debug.LogError("BaseCounter.Interact();");
        }

        public virtual void InteractAlternate(Player player)
        {
            // Debug.LogError("BaseCounter.InteractAlternate();");
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return counterTopPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            this.kitchenObject = kitchenObject;

            if (kitchenObject != null)
            {
                OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
            }
        }

        public KitchenObject GetKitchenObject()
        {
            return kitchenObject;
        }

        public void ClearKitchenObject()
        {
            kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return kitchenObject != null;
        }
    }
}