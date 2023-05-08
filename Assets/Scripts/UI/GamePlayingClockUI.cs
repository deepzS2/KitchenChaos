using KitchenChaos.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos.Scripts.UI
{
    public class GamePlayingClockUI : MonoBehaviour
    {
        [SerializeField] private Image timerImage;

        private void Update()
        {
            timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
        }
    }
}