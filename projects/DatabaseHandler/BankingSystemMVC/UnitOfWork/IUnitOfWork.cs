namespace BankingSystemMVC.UnitOfWork
{
    /// <summary>
    /// Interface representing the unit of work pattern.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        public void Save();
    }
}
