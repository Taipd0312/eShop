namespace Mask.Application.UnitOfWorks
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int Commit();
    }
}
