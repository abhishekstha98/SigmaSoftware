using System;
using System.Collections.Generic;

namespace CandidateHubAPI.Models;

public partial class OnboardedCandidate
{
    public int TempEmployeeId { get; set; }

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateTime OnboardedDate { get; set; }
}
