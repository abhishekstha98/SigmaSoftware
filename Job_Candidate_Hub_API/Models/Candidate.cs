﻿using System;
using System.Collections.Generic;

namespace CandidateHubAPI.Models;

public partial class Candidate
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? CallTimeInterval { get; set; }

    public string? LinkedInUrl { get; set; }

    public string? GitHubUrl { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime? InterviewTime { get; set; }

    public bool SentEmail { get; set; }
}
