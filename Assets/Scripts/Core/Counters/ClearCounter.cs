namespace KitchenChaos.Scripts.Core.Counters
{
    public class ClearCounter : BaseCounter
    {
        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                // There is NO KitchenObject here
                if (player.HasKitchenObject())
                {
                    // Player is carrying something
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
            else
            {
                // There is a KitchenObject here
                if (player.HasKitchenObject())
                {
                    PlateKitchenObject plateKitchenObject;

                    // Player is carrying something
                    if (player.GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // Player is holding a Plate

                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                    else
                    {
                        // Player is NOT holding a Plate
                        if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                        {
                            if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                            {
                                player.GetKitchenObject().DestroySelf();
                            }
                        }
                    }
                }
                else
                {
                    // Player is NOT carrying something
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }
    }
}