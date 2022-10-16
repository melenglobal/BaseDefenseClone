using Abstract.Interfaces;
using UnityEngine;

namespace Controllers.Bomb
{
    public class BombPhysicController : MonoBehaviour, IAttacker
    {
        private const int DAMAGE = 95;

        public int Damage()
        {
            return DAMAGE;
        }
    }
}
