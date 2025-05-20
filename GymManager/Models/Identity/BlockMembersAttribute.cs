using GymManager.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Models.Identity;

public class BlockMembersAttribute : TypeFilterAttribute
{
    public BlockMembersAttribute() : base(typeof(BlockAuthenticatedMembersFilter)) {}
}