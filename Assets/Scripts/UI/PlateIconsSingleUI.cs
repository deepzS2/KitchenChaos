using KitchenChaos.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos.Scripts.UI
{
    public class PlateIconsSingleUI : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
        {
            image.sprite = kitchenObjectSO.Sprite;
        }
    }
}