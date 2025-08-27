CREATE TABLE [dbo].[GamesTable] (
    [Id]        INT  IDENTITY (1, 1) NOT NULL,
    [Length]    INT  NOT NULL,
    [StartDate] DATE NOT NULL,
    [PlayerId]  INT  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GamesTable_ToTable] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[PlayersTable] ([ID])
);

