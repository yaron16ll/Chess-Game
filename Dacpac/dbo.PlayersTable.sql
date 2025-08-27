CREATE TABLE [dbo].[PlayersTable] (
    [ID]          INT        NOT NULL,
    [FirstName]   NCHAR (10) NOT NULL,
    [PhoneNumber] NCHAR (10) NOT NULL,
    [Country]     NCHAR (10) NOT NULL,
    [LastName]    NCHAR (10) NULL,
    [Password]    NCHAR (6)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

