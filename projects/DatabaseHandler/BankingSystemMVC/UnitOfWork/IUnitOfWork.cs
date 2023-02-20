namespace BankingSystemMVC.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public void Save();
    }
}
