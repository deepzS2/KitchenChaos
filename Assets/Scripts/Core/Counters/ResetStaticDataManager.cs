using UnityEngine;

namespace KitchenChaos.Scripts.Core.Counters
{
    public class ResetStaticDataManager : MonoBehaviour
    {
        private void Awake()
        {
            CuttingCounter.ResetStaticData();
            BaseCounter.ResetStaticData();
            TrashCounter.ResetStaticData();
        }
    }
}