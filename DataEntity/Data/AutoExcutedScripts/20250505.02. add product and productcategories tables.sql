CREATE TABLE IF NOT EXISTS `Product` (
    `Id` INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
    `Name` VARCHAR(255) NOT NULL,
    `BrandId` INT NOT NULL,
    `OriginalPrice` DECIMAL(10,2) NOT NULL, 
    `SalePercentage` INT NOT NULL,
    `Status` TINYINT(1) NOT NULL,
    `CreatedBy` VARCHAR(255) NOT NULL,
    `CreatedAt` DATETIME(3) NOT NULL,
    CONSTRAINT `FK_BrandProduct` FOREIGN KEY (`BrandId`) REFERENCES `Brand`(`Id`)
    );

CREATE TABLE IF NOT EXISTS `ProductCategory` ( 
    `ProductId` INT NOT NULL,
    `CategoryId` INT NOT NULL,
    PRIMARY KEY (`ProductId`, `CategoryId`), 
    CONSTRAINT `FK_ProductCategory_Product` FOREIGN KEY (`ProductId`) REFERENCES `Product`(`Id`),
    CONSTRAINT `FK_ProductCategory_Category` FOREIGN KEY (`CategoryId`) REFERENCES `Category` (`Id`) 
);
