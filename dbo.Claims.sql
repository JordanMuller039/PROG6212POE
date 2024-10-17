CREATE TABLE [dbo].[Claims] (
    [ClaimID]          INT             IDENTITY (1, 1) NOT NULL,
    [LecturerID]       INT             NOT NULL,
    [TotalHoursWorked] INT             NOT NULL,
    [AmountDue]        DECIMAL (18, 2) NOT NULL,
    [Status]           NVARCHAR (MAX)  DEFAULT 'Pending',
    [ModCode]          NVARCHAR (MAX)  DEFAULT (N'') NOT NULL,
    [desc]             NVARCHAR (MAX)  DEFAULT (N'') NOT NULL,
    CONSTRAINT [PK_Claims] PRIMARY KEY CLUSTERED ([ClaimID] ASC)
);

