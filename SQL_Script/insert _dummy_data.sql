USE [sigmasoftware]
GO
SET IDENTITY_INSERT [dbo].[Candidates] ON 
GO
INSERT [dbo].[Candidates] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [CallTimeInterval], [LinkedInUrl], [GitHubUrl], [Comment], [InterviewTime], [SentEmail ]) VALUES (1, N'John', N'Doe', N'johndoe@example.com', N'123-456-7890', N'10:00 AM - 11:00 AM', N'https://www.linkedin.com/in/johndoe', N'https://github.com/johndoe', N'A promising candidate with strong technical skills.', CAST(N'2024-12-10T10:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Candidates] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [CallTimeInterval], [LinkedInUrl], [GitHubUrl], [Comment], [InterviewTime], [SentEmail ]) VALUES (2, N'Jane', N'Smith', N'janesmith@example.com', N'987-654-3210', N'2:00 PM - 3:00 PM', N'https://www.linkedin.com/in/janesmith', N'https://github.com/janesmith', N'Experienced candidate with a focus on frontend development.', CAST(N'2024-12-12T14:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Candidates] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [CallTimeInterval], [LinkedInUrl], [GitHubUrl], [Comment], [InterviewTime], [SentEmail ]) VALUES (3, N'Michael', N'Johnson', N'michaeljohnson@example.com', N'555-555-5555', N'11:00 AM - 12:00 PM', N'https://www.linkedin.com/in/michaeljohnson', N'https://github.com/michaeljohnson', N'Recent graduate with potential.', CAST(N'2024-12-11T11:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Candidates] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [CallTimeInterval], [LinkedInUrl], [GitHubUrl], [Comment], [InterviewTime], [SentEmail ]) VALUES (4, N'Emily', N'Brown', N'emilybrown@example.com', N'123-456-7890', N'9:00 AM - 10:00 AM', N'https://www.linkedin.com/in/emilybrown', N'https://github.com/emilybrown', N'Strong communication and teamwork skills.', CAST(N'2024-12-13T09:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Candidates] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [CallTimeInterval], [LinkedInUrl], [GitHubUrl], [Comment], [InterviewTime], [SentEmail ]) VALUES (5, N'David', N'Lee', N'davidlee@example.com', N'987-654-3210', N'3:00 PM - 4:00 PM', N'https://www.linkedin.com/in/davidlee', N'https://github.com/davidlee', N'Experienced backend developer.', CAST(N'2024-12-14T15:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Candidates] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [CallTimeInterval], [LinkedInUrl], [GitHubUrl], [Comment], [InterviewTime], [SentEmail ]) VALUES (6, N'Sarah', N'Kim', N'sarahkim@example.com', N'555-555-5555', N'10:00 AM - 11:00 AM', N'https://www.linkedin.com/in/sarahkim', N'https://github.com/sarahkim', N'Passionate about full-stack development.', CAST(N'2024-12-15T10:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Candidates] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [CallTimeInterval], [LinkedInUrl], [GitHubUrl], [Comment], [InterviewTime], [SentEmail ]) VALUES (7, N'Thomas', N'Miller', N'thomasmiller@example.com', N'123-456-7890', N'2:00 PM - 3:00 PM', N'https://www.linkedin.com/in/thomasmiller', N'https://github.com/thomasmiller', N'Strong problem-solving and debugging skills.', CAST(N'2024-12-16T14:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Candidates] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [CallTimeInterval], [LinkedInUrl], [GitHubUrl], [Comment], [InterviewTime], [SentEmail ]) VALUES (8, N'Olivia', N'Davis', N'oliviadavis@example.com', N'987-654-3210', N'11:00 AM - 12:00 PM', N'https://www.linkedin.com/in/oliviadavis', N'https://github.com/oliviadavis', N'Creative and innovative thinker.', CAST(N'2024-12-17T11:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Candidates] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [CallTimeInterval], [LinkedInUrl], [GitHubUrl], [Comment], [InterviewTime], [SentEmail ]) VALUES (9, N'James', N'Wilson', N'jameswilson@example.com', N'555-555-5555', N'9:00 AM - 10:00 AM', N'https://www.linkedin.com/in/jameswilson', N'https://github.com/jameswilson', N'Strong leadership and team management skills.', CAST(N'2024-12-18T09:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Candidates] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [CallTimeInterval], [LinkedInUrl], [GitHubUrl], [Comment], [InterviewTime], [SentEmail ]) VALUES (10, N'Sophia', N'Moore', N'sophiamoore@example.com', N'123-456-7890', N'3:00 PM - 4:00 PM', N'https://www.linkedin.com/in/sophiamoore', N'https://github.com/sophiamoore', N'Quick learner and adaptable.', CAST(N'2024-12-19T15:00:00.000' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[Candidates] OFF
GO
