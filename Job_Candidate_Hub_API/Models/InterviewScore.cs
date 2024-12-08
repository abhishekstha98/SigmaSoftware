using System;
using System.Collections.Generic;

namespace CandidateHubAPI.Models;

public partial class InterviewScore
{
    public int Id { get; set; }

    public int CandidateId { get; set; }

    public int TechnicalScore { get; set; }

    public int CommunicationScore { get; set; }

    public int ProblemSolvingScore { get; set; }

    public int? TotalScore { get; set; }

    public string? Comments { get; set; }

    public DateTime ScoredOn { get; set; }
}
