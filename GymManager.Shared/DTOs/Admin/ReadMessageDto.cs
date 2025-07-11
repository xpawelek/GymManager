﻿namespace GymManager.Shared.DTOs.Admin;

public class ReadMessageDto
{
    public int Id { get; set; }

    public string MessageContent { get; set; }
    
    public DateTime Date { get; set; }

    public int TrainerId { get; set; }

    public int MemberId { get; set; }

    public bool SentByMember { get; set; }
}