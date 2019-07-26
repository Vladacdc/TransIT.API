namespace TransIT.DAL.Models.DependencyInjection
{
    /// <summary>
    /// An abstraction which represents a current user in system.
    /// We need it get access to user id in <see cref="Microsoft.EntityFrameworkCore.TransITDBContext.SaveChangesAsync(System.Threading.CancellationToken)"/>.
    /// </summary>
    public interface IUser
    {
        string CurrentUserId { get; }
    }
}
