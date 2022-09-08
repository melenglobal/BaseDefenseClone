namespace Abstract
{
    public interface IEnemy
    {
        public int Health { get; set; }

        public int Damage { get; set; }

        public float AttackRange { get; set; }

        public float Speed { get; set; }

        public float ChaseSpeed { get; set; }
    }
}