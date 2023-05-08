using KitchenChaos.Scripts.ScriptableObjects;
using System;
using UnityEngine;

namespace KitchenChaos.Scripts.Core.Counters
{
    public class ContainerCounter : BaseCounter
    {
        public event EventHandler OnPlayerGrabbedObject;

        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        public override void Interact(Player player)
        {
            if (!player.HasKitchenObject())
            {
                // Player is NOT carrying anything
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}