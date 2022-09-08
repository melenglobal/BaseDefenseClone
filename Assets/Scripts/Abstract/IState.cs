namespace Abstract
{
    public interface IState
    {

        void Tick();

        void OnEnter();

        void OnExit();

    }
}