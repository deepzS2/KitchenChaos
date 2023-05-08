using KitchenChaos.Scripts.Core.Interfaces;
using KitchenChaos.Scripts.ScriptableObjects;
using System;
using UnityEngine;

namespace KitchenChaos.Scripts.Core.Counters
{
    public class StoveCounter : BaseCounter, IHasProgress
    {
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public class OnStateChangedEventArgs : EventArgs
        {
            public State state;
        }

        public enum State
        {
            Idle,
            Frying,
            Fried,
            Burned,
        }

        [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
        [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

        private State state;
        private float fryingTimer;
        private float burningTimer;
        private FryingRecipeSO fryingRecipeSO;
        private BurningRecipeSO burningRecipeSO;

        private void Start()
        {
            state = State.Idle;
        }

        private void Update()
        {

            if (HasKitchenObject())
            {

                switch (state)
                {
                    case State.Idle:
                        break;

                    case State.Frying:
                        fryingTimer += Time.deltaTime;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                        });

                        if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                        {
                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(fryingRecipeSO.Output, this);

                            state = State.Fried;
                            burningTimer = 0f;
                            burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = state
                            });
                        }

                        break;

                    case State.Fried:
                        burningTimer += Time.deltaTime;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                        });

                        if (burningTimer > burningRecipeSO.burningTimerMax)
                        {
                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(burningRecipeSO.Output, this);

                            state = State.Burned;

                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = state
                            });

                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                            {
                                progressNormalized = 0f
                            });
                        }

                        break;

                    case State.Burned:
                        break;
                }
            }
        }

        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                // There is NO KitchenObject here
                if (player.HasKitchenObject())
                {
                    // Player is carrying something
                    if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().SetKitchenObjectParent(this);

                        fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        state = State.Frying;
                        fryingTimer = 0f;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                        });
                    }
                }
            }
            else
            {
                // There is a KitchenObject here
                if (player.HasKitchenObject())
                {
                    // Player is carrying something
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        // Player is holding a Plate

                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();

                            state = State.Idle;

                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = state
                            });

                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                            {
                                progressNormalized = 0f
                            });
                        }
                    }
                }
                else
                {
                    // Player is NOT carrying something
                    GetKitchenObject().SetKitchenObjectParent(player);

                    state = State.Idle;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                }
            }
        }

        private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

            return fryingRecipeSO != null;
        }

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
        {
            FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

            return fryingRecipeSO?.Output;
        }

        private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
            {
                if (fryingRecipeSO.Input == inputKitchenObjectSO)
                {
                    return fryingRecipeSO;
                }
            }

            return null;
        }

        private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
            {
                if (burningRecipeSO.Input == inputKitchenObjectSO)
                {
                    return burningRecipeSO;
                }
            }

            return null;
        }

        public bool IsFried() => state == State.Fried;
    }
}