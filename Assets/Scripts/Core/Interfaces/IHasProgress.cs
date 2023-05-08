using System;

namespace KitchenChaos.Scripts.Core.Interfaces
{
    public interface IHasProgress
    {
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public class OnProgressChangedEventArgs : EventArgs
        {
            public float progressNormalized;
        }
    }
}