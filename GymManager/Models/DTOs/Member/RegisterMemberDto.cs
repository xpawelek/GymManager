﻿namespace GymManager.Models.DTOs.Member;

public class RegisterMemberDto
{
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public String PhoneNumber { get; set; }
    public String Password { get; set; }
}