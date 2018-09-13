CREATE TABLE [dbo].[Search_Record] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Search]    VARCHAR (50) NOT NULL,
    [Result]    BIT          NOT NULL,
    [Timestamp] DATETIME     NOT NULL,
    CONSTRAINT [PK_Search_Record] PRIMARY KEY CLUSTERED ([Id] ASC)
);

