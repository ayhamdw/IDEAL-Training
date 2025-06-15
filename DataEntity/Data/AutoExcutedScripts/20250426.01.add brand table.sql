CREATE TABLE IF NOT EXISTS `Brand` (
    `Id` int AUTO_INCREMENT NOT NULL,
    `Name` varchar(256) NOT NULL,
    `Logo` varchar(256) NOT NULL,
    `Status` int NOT NULL,
    `CreatedBy` varchar(256) NOT NUll,
    `CreatedAt` datetime(3) NOT NULL,
    CONSTRAINT `PK_Brand` PRIMARY KEY
(
    `Id` ASC
)
);
