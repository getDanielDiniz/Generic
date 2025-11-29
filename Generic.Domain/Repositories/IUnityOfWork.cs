namespace Generic.Domain.Repositories
{
    public interface IUnityOfWork
    {        
        Task<int> Commit();
    }
}
