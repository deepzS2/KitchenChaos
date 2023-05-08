using KitchenChaos.Scripts.Core;
using KitchenChaos.Scripts.Core.Counters;
using UnityEngine;

namespace KitchenChaos.Scripts.Visuals.Counters
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private BaseCounter baseCounter;
        [SerializeField] private GameObject[] visualGameObjectArray;

        private void Start()
        {
            Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }

        private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
        {
            if (e.selectedCounter == baseCounter)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            foreach (GameObject visualGameObject in visualGameObjectArray)
            {
                visualGameObject.SetActive(true);
            }
        }

        private void Hide()
        {
            foreach (GameObject visualGameObject in visualGameObjectArray)
            {
                visualGameObject.SetActive(false);
            }
        }
    }
}