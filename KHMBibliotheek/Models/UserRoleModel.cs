namespace KHMBibliotheek.Models;
public class UserRoleModel
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int RoleOrder { get; set; }
    public string? RoleName { get; set; }
    public string? RoleDescription { get; set; }
}