namespace Abstract.AbstractClasses
{
    public abstract class Worker
    {
        public int Capacity;

        public float Speed;

        protected Worker(float speed, int capacity) // Constructor savelenirken abstractin kini mi newleycez,yoksa concrete classin mi 
        {
            Speed = speed;
            Capacity = capacity;
        }

    }
}