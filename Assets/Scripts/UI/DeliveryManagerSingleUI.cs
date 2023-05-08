using UnityEngine;
using TMPro;
using UnityEngine.UI;
using KitchenChaos.Scripts.ScriptableObjects;

namespace KitchenChaos.Scripts.UI
{
    public class DeliveryManagerSingleUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipeNameText;
        [SerializeField] private Transform iconContainer;
        [SerializeField] private Transform iconTemplate;

        private void Awake()
        {
            iconTemplate.gameObject.SetActive(false);
        }

        public void SetRecipeSO(RecipeSO recipeSO)
        {
            recipeNameText.text = recipeSO.RecipeName;

            foreach (Transform child in iconContainer)
            {
                if (child == iconTemplate) continue;

                Destroy(child.gameObject);
            }

            foreach (KitchenObjectSO kitchenObjectSO in recipeSO.KitchenObjectSOList)
            {
                Transform iconTransform = Instantiate(iconTemplate, iconContainer);

                iconTransform.gameObject.SetActive(true);
                iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.Sprite;
            }
        }
    }
}