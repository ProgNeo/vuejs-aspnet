namespace Anime.Models;

public partial class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public virtual ICollection<UserRole> UsersRoles { get; set; }
}