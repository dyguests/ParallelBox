namespace Entities
{
    public interface ISplitCloneable<out T>
    {
        T SplitClone();
    }
}