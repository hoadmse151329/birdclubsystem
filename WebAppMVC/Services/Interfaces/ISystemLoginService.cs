namespace WebAppMVC.Services.Interfaces
{
    public interface ISystemLoginService
    {
        Task<string?> GetTokenAsync();
    }
}
