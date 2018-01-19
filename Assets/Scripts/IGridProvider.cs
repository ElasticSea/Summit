namespace Assets.Base.Scripts
{
    public interface IGridProvider<T>
    {
        T Provide(int x, int y);
    }
}