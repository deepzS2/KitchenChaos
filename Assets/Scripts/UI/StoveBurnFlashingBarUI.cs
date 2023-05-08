using KitchenChaos.Scripts.Core.Counters;
using KitchenChaos.Scripts.Core.Interfaces;
using UnityEngine;

namespace KitchenChaos.Scripts.UI
{
    public class StoveBurnFlashingBarUI : MonoBehaviour
    {
        private const string IS_FLASHING = "IsFlashing";

        [SerializeField] private StoveCounter stoveCounter;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

            animator.SetBool(IS_FLASHING, false);
        }

        private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
        {
            float burnShowProgressAmount = .5f;

            bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

            animator.SetBool(IS_FLASHING, show);
        }
    }
}