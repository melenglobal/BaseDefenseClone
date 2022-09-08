namespace Abstract
{
    public interface ISaveable
    {
        void Save(int uniqueId);

        void Load(int uniqueId);
    }
}