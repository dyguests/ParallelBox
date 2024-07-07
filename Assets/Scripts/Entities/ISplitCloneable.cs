namespace Entities
{
    public interface ISplitCloneable<out T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count">要分裂成几份</param>
        /// <returns></returns>
        T SplitClone(int count);
    }
}