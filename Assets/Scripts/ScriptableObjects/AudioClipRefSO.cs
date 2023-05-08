using UnityEngine;

namespace KitchenChaos.Scripts.ScriptableObjects
{
    [CreateAssetMenu()]
    public class AudioClipRefSO : ScriptableObject
    {
        public AudioClip[] chop;
        public AudioClip[] deliveryFail;
        public AudioClip[] deliverySuccess;
        public AudioClip[] footsteps;
        public AudioClip[] objectDrop;
        public AudioClip[] objectPickup;
        public AudioClip stoveSizzle;
        public AudioClip[] trash;
        public AudioClip[] warning;
    }
}
