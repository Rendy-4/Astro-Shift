using UnityEngine;

namespace AstroShift.Core
{
    public interface IDamageable
    {
        void Die(GameObject killer = null);
    }
}
