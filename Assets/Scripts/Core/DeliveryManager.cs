using KitchenChaos.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Scripts.Core
{
    public class DeliveryManager : MonoBehaviour
    {
        public static DeliveryManager Instance { get; private set; }

        public event EventHandler OnRecipeSpawned;
        public event EventHandler OnRecipeCompleted;
        public event EventHandler OnRecipeSuccess;
        public event EventHandler OnRecipeFailed;

        [SerializeField] private RecipeListSO recipeListSO;

        private List<RecipeSO> waitingRecipeSOList;
        private float spawnRecipeTimer;
        private float spawnRecipeTimerMax = 4f;
        private int waitingRecipesMax = 4;
        private int successfulRecipesAmount;

        private void Awake()
        {
            Instance = this;

            waitingRecipeSOList = new List<RecipeSO>();
        }

        private void Update()
        {
            spawnRecipeTimer -= Time.deltaTime;

            if (spawnRecipeTimer < 0f)
            {
                spawnRecipeTimer = spawnRecipeTimerMax;

                if (GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipesMax)
                {
                    RecipeSO waitingRecipeSO = recipeListSO.RecipeSOList[UnityEngine.Random.Range(0, recipeListSO.RecipeSOList.Count)];

                    waitingRecipeSOList.Add(waitingRecipeSO);

                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
        {
            for (int i = 0; i < waitingRecipeSOList.Count; i++)
            {
                RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

                if (waitingRecipeSO.KitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
                {
                    // Has the same number of ingredients
                    bool doesPlateContentsMatchesRecipe = true;

                    foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.KitchenObjectSOList)
                    {
                        // Cycling through all ingredients in the Recipe
                        bool hasIngredientBeenFound = false;

                        foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                        {
                            // Cycling through all ingredients in the Plate
                            if (plateKitchenObjectSO == recipeKitchenObjectSO)
                            {
                                // Ingredient matches!
                                hasIngredientBeenFound = true;
                                break;
                            }
                        }

                        if (!hasIngredientBeenFound)
                        {
                            // This Recipe ingredient was not found on the Plate
                            doesPlateContentsMatchesRecipe = false;
                        }
                    }

                    if (doesPlateContentsMatchesRecipe)
                    {
                        successfulRecipesAmount++;
                        waitingRecipeSOList.RemoveAt(i);

                        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                        OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                        return;
                    }
                }
            }

            // No matches found!
            OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        }

        public List<RecipeSO> GetWaitingRecipeSOList() => waitingRecipeSOList;
        public int GetSuccessfulRecipesAmount() => successfulRecipesAmount;
    }
}
