CREATE DATABASE ProEventosDB;

USE ProEventosDB;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `AspNetRoles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;


CREATE TABLE `AspNetUsers` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `PrimeiroNome` longtext CHARACTER SET utf8mb4 NULL,
    `UltimoNome` longtext CHARACTER SET utf8mb4 NULL,
    `Titulo` int NOT NULL,
    `Descricao` longtext CHARACTER SET utf8mb4 NULL,
    `Funcao` int NOT NULL,
    `ImagemURL` longtext CHARACTER SET utf8mb4 NULL,
    `UserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `Email` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 NULL,
    `EmailConfirmed` tinyint(1) NOT NULL,
    `PasswordHash` longtext CHARACTER SET utf8mb4 NULL,
    `SecurityStamp` longtext CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
    `PhoneNumberConfirmed` tinyint(1) NOT NULL,
    `TwoFactorEnabled` tinyint(1) NOT NULL,
    `LockoutEnd` datetime(6) NULL,
    `LockoutEnabled` tinyint(1) NOT NULL,
    `AccessFailedCount` int NOT NULL,
    CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `AspNetRoleClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RoleId` int NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `AspNetUserClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderKey` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderDisplayName` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `AspNetUserRoles` (
    `UserId` int NOT NULL,
    `RoleId` int NOT NULL,
    CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`RoleId`, `UserId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `AspNetUserTokens` (
    `UserId` int NOT NULL,
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Value` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `Eventos` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Local` longtext CHARACTER SET utf8mb4 NULL,
    `DataEvento` datetime(6) NULL,
    `Tema` longtext CHARACTER SET utf8mb4 NULL,
    `QtdPessoas` int NOT NULL,
    `ImagemURL` longtext CHARACTER SET utf8mb4 NULL,
    `Telefone` longtext CHARACTER SET utf8mb4 NULL,
    `Email` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_Eventos` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Eventos_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `Palestrantes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `MiniCurriculo` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_Palestrantes` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Palestrantes_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `Lotes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nome` longtext CHARACTER SET utf8mb4 NULL,
    `Preco` decimal(65,30) NOT NULL,
    `DataInicio` datetime(6) NULL,
    `DataFim` datetime(6) NULL,
    `Quantidade` int NOT NULL,
    `EventoId` int NOT NULL,
    CONSTRAINT `PK_Lotes` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Lotes_Eventos_EventoId` FOREIGN KEY (`EventoId`) REFERENCES `Eventos` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `PalestrantesEventos` (
    `PalestranteId` int NOT NULL,
    `EventoId` int NOT NULL,
    CONSTRAINT `PK_PalestrantesEventos` PRIMARY KEY (`EventoId`, `PalestranteId`),
    CONSTRAINT `FK_PalestrantesEventos_Eventos_EventoId` FOREIGN KEY (`EventoId`) REFERENCES `Eventos` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_PalestrantesEventos_Palestrantes_PalestranteId` FOREIGN KEY (`PalestranteId`) REFERENCES `Palestrantes` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `RedesSociais` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nome` longtext CHARACTER SET utf8mb4 NULL,
    `URL` longtext CHARACTER SET utf8mb4 NULL,
    `EventoId` int NULL,
    `PalestranteId` int NULL,
    CONSTRAINT `PK_RedesSociais` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_RedesSociais_Eventos_EventoId` FOREIGN KEY (`EventoId`) REFERENCES `Eventos` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_RedesSociais_Palestrantes_PalestranteId` FOREIGN KEY (`PalestranteId`) REFERENCES `Palestrantes` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_UserId` ON `AspNetUserRoles` (`UserId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

CREATE INDEX `IX_Eventos_UserId` ON `Eventos` (`UserId`);

CREATE INDEX `IX_Lotes_EventoId` ON `Lotes` (`EventoId`);

CREATE INDEX `IX_Palestrantes_UserId` ON `Palestrantes` (`UserId`);

CREATE INDEX `IX_PalestrantesEventos_PalestranteId` ON `PalestrantesEventos` (`PalestranteId`);

CREATE INDEX `IX_RedesSociais_EventoId` ON `RedesSociais` (`EventoId`);

CREATE INDEX `IX_RedesSociais_PalestranteId` ON `RedesSociais` (`PalestranteId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220516192819_MigrandoMySql', '5.0.10');

COMMIT;

