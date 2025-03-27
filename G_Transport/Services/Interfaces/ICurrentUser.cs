namespace G_Transport.Services.Interfaces
{
    public interface ICurrentUser
    {
        string GetCurrentUser();
        Guid GetCurrentuserId();

    }
}
