using KitchenChaos.Scripts.Core;
using UnityEngine;

namespace KitchenChaos.Scripts.Visuals
{
    public class PlayerAnimator : MonoBehaviour
    {
        private const string IS_WALKING = "IsWalking";

        [SerializeField] private Player player;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            animator.SetBool(IS_WALKING, player.IsWalking());
        }
    }
}
