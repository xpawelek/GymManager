namespace GymManager.Models.Identity;

public static class RoleConstants
{
    public const string Admin = "Admin";
    public const string User = "Receptionist";
    public const string Member = "Member";
    public const string Trainer = "Trainer";
    
    public static readonly string[] AllRoles = { Admin, User, Member, Trainer };
}