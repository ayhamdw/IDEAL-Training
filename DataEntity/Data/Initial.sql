CREATE TABLE IF NOT EXISTS ScriptStatus (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    IsExecuted BOOLEAN NOT NULL
);
INSERT INTO ScriptStatus VALUES (default, 1);

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory`(
	`MigrationId` varchar(150) NOT NULL,
	`ProductVersion` varchar(32) NOT NULL,
 CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY 
(
	`MigrationId` ASC
) 
);

CREATE TABLE IF NOT EXISTS `AspNetRoleClaims`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`RoleId` varchar(450) NOT NULL,
	`ClaimType` Longtext NULL,
	`ClaimValue` Longtext NULL,
 CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY 
(
	`Id` ASC
) 
);

CREATE TABLE IF NOT EXISTS `AspNetRoles`(
	`Id` varchar(450) NOT NULL,
	`Name` varchar(256) NULL,
	`NormalizedName` varchar(256) NULL,
	`ConcurrencyStamp` Longtext NULL,
 CONSTRAINT `PK_AspNetRoles` PRIMARY KEY 
(
	`Id` ASC
) 
);

CREATE TABLE IF NOT EXISTS `AspNetUserClaims`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`UserId` varchar(450) NOT NULL,
	`ClaimType` Longtext NULL,
	`ClaimValue` Longtext NULL,
 CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY 
(
	`Id` ASC
) 
);

CREATE TABLE IF NOT EXISTS `AspNetUserLogins`(
	`LoginProvider` varchar(128) NOT NULL,
	`ProviderKey` varchar(128) NOT NULL,
	`ProviderDisplayName` Longtext NULL,
	`UserId` varchar(450) NOT NULL,
 CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY 
(
	`LoginProvider` ASC,
	`ProviderKey` ASC
) 
);

CREATE TABLE IF NOT EXISTS `AspNetUserRoles`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`UserId` varchar(450) NOT NULL,
	`RoleId` varchar(450) NOT NULL,
 CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY 
(
	`Id` ASC
) 
);

CREATE TABLE IF NOT EXISTS `AspNetUsers`(
	`Id` varchar(450) NOT NULL,
	`UserName` varchar(256) NULL,
	`NormalizedUserName` varchar(256) NULL,
	`Email` varchar(256) NULL,
	`NormalizedEmail` varchar(256) NULL,
	`EmailConfirmed` BOOLEAN NOT NULL,
	`PasswordHash` Longtext NULL,
	`SecurityStamp` Longtext NULL,
	`ConcurrencyStamp` Longtext NULL,
	`PhoneNumber` Longtext NULL,
	`PhoneNumberConfirmed` BOOLEAN NOT NULL,
	`TwoFactorEnabled` BOOLEAN NOT NULL,
	`LockoutEnd` Datetime(6) NULL,
	`LockoutEnabled` BOOLEAN NOT NULL,
	`AccessFailedCount` int NOT NULL,
 CONSTRAINT `PK_AspNetUsers` PRIMARY KEY 
(
	`Id` ASC
) 
);

CREATE TABLE IF NOT EXISTS `AspNetUserTokens`(
	`UserId` varchar(450) NOT NULL,
	`LoginProvider` varchar(128) NOT NULL,
	`Name` varchar(128) NOT NULL,
	`Value` Longtext NULL,
 CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY 
(
	`UserId` ASC,
	`LoginProvider` ASC,
	`Name` ASC
) 
);

CREATE TABLE IF NOT EXISTS `AuditLogs`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`Action` varchar(200) NOT NULL,
	`Controller` varchar(200) NOT NULL,
	`IpAddress` varchar(200) NOT NULL,
	`Description` Longtext NULL,
	`CreatedOn` datetime(3) NOT NULL,
	`CreatedBy` varchar(256) NOT NULL,
	`Status` int NOT NULL,
	`RequestDetails` Longtext NULL,
 CONSTRAINT `PK_AuditLogs` PRIMARY KEY 
(
	`Id` ASC
) 
);


CREATE TABLE IF NOT EXISTS `DataBaseScripts`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`Name` Longtext NOT NULL,
	`CreatedOn` datetime(3) NOT NULL,
	`CreatedBy` varchar(50) NOT NULL,
	`Status` int NOT NULL,
 CONSTRAINT `PK_DataBaseScripts` PRIMARY KEY 
(
	`Id` ASC
) 
);

CREATE TABLE IF NOT EXISTS `DetailsLookup`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`MasterId` int NOT NULL,
	`CreatedOn` datetime(3) NOT NULL,
	`CreatedBy` varchar(256) NOT NULL,
	`Status` int NOT NULL,
	`DeletedOn` datetime(3) NULL,
	`Code` varchar(300) NULL,
	`Value` varchar(200) NULL,
 CONSTRAINT `PK_DetailsLookup` PRIMARY KEY 
(
	`Id` ASC
) 
);

CREATE TABLE IF NOT EXISTS `DetailsLookupTranslation`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`DetailsLookupId` int NOT NULL,
	`Value` varchar(200) NOT NULL,
	`LanguageId` int NOT NULL,
	`IsDefault` BOOLEAN NOT NULL,
 CONSTRAINT `PK_DetailsLookupTranslation` PRIMARY KEY 
(
	`Id` ASC
) 
);


CREATE TABLE IF NOT EXISTS `MasterLookup`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`CreatedOn` datetime(3) NOT NULL,
	`CreatedBy` varchar(256) NOT NULL,
	`Status` int NOT NULL,
	`DeletedOn` datetime(3) NULL,
	`Name` varchar(200) NULL,
	`Code` varchar(200) NULL,
 CONSTRAINT `PK_MasterLookup` PRIMARY KEY 
(
	`Id` ASC
) 
);

CREATE TABLE IF NOT EXISTS `MasterLookupTranslation`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`MasterLookupId` int NOT NULL,
	`Name` varchar(200) NOT NULL,
	`LanguageId` int NOT NULL,
	`IsDefault` BOOLEAN NOT NULL,
 CONSTRAINT `PK_MasterLookupTranslation` PRIMARY KEY 
(
	`Id` ASC
) 
);

CREATE TABLE IF NOT EXISTS `Permission`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`PageUrl` Longtext NOT NULL,
	`PageName` varchar(300) NULL,
	`PermissionKey` varchar(200) NOT NULL,
	`Description` Longtext NULL,
	`CreatedOn` datetime(3) NOT NULL,
	`CreatedBy` varchar(256) NOT NULL,
	`Status` int NOT NULL,
	`DeletedOn` datetime(3) NULL,
 CONSTRAINT `PK_Permission` PRIMARY KEY 
(
	`Id` ASC
) 
);

CREATE TABLE IF NOT EXISTS `RolePermissions`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`RoleId` varchar(450) NOT NULL,
	`PermissionId` int NOT NULL,
 CONSTRAINT `PK_RolePermissions` PRIMARY KEY 
(
	`Id` ASC
) 
);


CREATE TABLE IF NOT EXISTS `SystemLog`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`Name` varchar(200) NOT NULL,
	`Component` varchar(200) NOT NULL,
	`StackTrace` Longtext NULL,
	`CreatedOn` datetime(3) NOT NULL,
	`CreatedBy` varchar(256) NOT NULL,
	`Status` int NOT NULL,
	`DeletedOn` datetime(3) NULL,
 CONSTRAINT `PK_SystemLog` PRIMARY KEY 
(
	`Id` ASC
) 
);

CREATE TABLE IF NOT EXISTS `SystemSetting`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`CreatedOn` datetime(3) NOT NULL,
	`CreatedBy` varchar(256) NOT NULL,
	`Status` int NOT NULL,
	`DeletedOn` datetime(3) NULL,
	`Name` Longtext NULL,
	`Value` Longtext NULL,
	`ShowInTheDashboard` BOOLEAN NULL,
 CONSTRAINT `PK_SystemSetting` PRIMARY KEY 
(
	`Id` ASC
) 
);



  
CREATE TABLE IF NOT EXISTS `SystemSettingTranslation`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`SettingId` int NOT NULL,
	`LanguageId` int NOT NULL,
	`IsDefault` BOOLEAN NOT NULL,
	`Name` Longtext NOT NULL,
	`Value` Longtext NOT NULL,
 CONSTRAINT `PK_SystemSettingTranslation` PRIMARY KEY 
(
	`Id` ASC
) 
);

CREATE TABLE IF NOT EXISTS `UserProfile`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`ContactId` int NULL,
	`Username` varchar(256) NOT NULL,
	`Email` varchar(256) NULL,
	`CreatedOn` datetime(3) NOT NULL,
	`Status` int NOT NULL,
	`DeletedOn` datetime(3) NULL,
	`LastLogin` datetime(3) NULL,
 CONSTRAINT `PK_UserProfile` PRIMARY KEY 
(
	`Id` ASC
) 
);
