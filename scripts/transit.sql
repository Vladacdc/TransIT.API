USE [MASTER];
GO
CREATE DATABASE TransITDB;
GO
USE TransITDB;
GO

CREATE TABLE [USER]
(
  ID           INT                  NOT NULL IDENTITY PRIMARY KEY,
  FIRST_NAME   NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS,
  MIDDLE_NAME  NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS,
  LAST_NAME    NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS,
  EMAIL        NVARCHAR(50),
  PHONE_NUMBER NVARCHAR(15),
  LOGIN        NVARCHAR(100) UNIQUE NOT NULL,
  PASSWORD     NVARCHAR(100)        NOT NULL,
  ROLE_ID      INT                  NOT NULL,
  IS_ACTIVE    BIT      DEFAULT (1),

  CREATE_DATE  DATETIME DEFAULT (GETDATE()),
  MOD_DATE     DATETIME DEFAULT (GETDATE()),
  CREATE_ID    INT,
  MOD_ID       INT,
  CONSTRAINT FK_CREATE_USER_ROLE
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_USER_ROLE
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION
);
GO

CREATE TABLE [ROLE]
(
  ID          INT                                            NOT NULL IDENTITY PRIMARY KEY,
  NAME        NVARCHAR(50)                                   NOT NULL UNIQUE,
  TRANS_NAME  NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS NOT NULL UNIQUE,

  CREATE_DATE DATETIME DEFAULT (GETDATE()),
  MOD_DATE    DATETIME DEFAULT (GETDATE()),
  CREATE_ID   INT,
  MOD_ID      INT,
  CONSTRAINT FK_CREATE_ROLE_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_ROLE_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION
);
GO

ALTER TABLE [USER]
  ADD CONSTRAINT FK_USER_ROLE
    FOREIGN KEY (ROLE_ID)
      REFERENCES [ROLE] (ID)
      ON DELETE NO ACTION;
GO

CREATE TABLE TOKEN
(
  ID            INT NOT NULL IDENTITY PRIMARY KEY,
  REFRESH_TOKEN NVARCHAR(MAX),

  CREATE_DATE   DATETIME DEFAULT (GETDATE()),
  MOD_DATE      DATETIME DEFAULT (GETDATE()),
  CREATE_ID     INT,
  MOD_ID        INT,
  CONSTRAINT FK_CREATE_TOKEN_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_TOKEN_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION
);
GO

CREATE TABLE VEHICLE_TYPE
(
  ID          INT                                            NOT NULL IDENTITY PRIMARY KEY,
  NAME        NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS NOT NULL UNIQUE,

  CREATE_DATE DATETIME DEFAULT (GETDATE()),
  MOD_DATE    DATETIME DEFAULT (GETDATE()),
  CREATE_ID   INT,
  MOD_ID      INT,
  CONSTRAINT FK_MOD_VEHICLE_TYPE_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_VEHICLE_TYPE_ROLE
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION
);
GO

CREATE TABLE VEHICLE
(
  ID              INT NOT NULL IDENTITY PRIMARY KEY,
  VEHICLE_TYPE_ID INT,
  VINCODE         NVARCHAR(20),
  INVENTORY_ID    NVARCHAR(40),
  REG_NUM         NVARCHAR(8),
  BRAND           NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS,
  MODEL           NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS,
  WARRANTY_END_DATE DATETIME,
  COMMISSIONING_DATE DATETIME,

  CREATE_DATE     DATETIME DEFAULT (GETDATE()),
  MOD_DATE        DATETIME DEFAULT (GETDATE()),

  CREATE_ID       INT,
  MOD_ID          INT,
  CONSTRAINT FK_VEHICLE_VEHICLE_TYPE
    FOREIGN KEY (VEHICLE_TYPE_ID)
      REFERENCES VEHICLE_TYPE (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_VEHICLE_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_VEHICLE_ROLE
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT CK_VEHICLE_REG_NUM
    CHECK (REG_NUM LIKE '[A-Z][A-Z][0-9][0-9][0-9][0-9][A-Z][A-Z]')
);
GO

CREATE TABLE MALFUNCTION_GROUP
(
  ID          INT                                             NOT NULL IDENTITY PRIMARY KEY,
  NAME        NVARCHAR(MAX) COLLATE Cyrillic_General_CI_AS_KS NOT NULL,

  CREATE_DATE DATETIME DEFAULT (GETDATE()),
  MOD_DATE    DATETIME DEFAULT (GETDATE()),
  CREATE_ID   INT FOREIGN KEY (CREATE_ID)
    REFERENCES [USER] (ID)
    ON DELETE NO ACTION,
  MOD_ID      INT FOREIGN KEY (MOD_ID)
    REFERENCES [USER] (ID)
    ON DELETE NO ACTION
);
GO

CREATE TABLE MALFUNCTION_SUBGROUP
(
  ID                   INT                                             NOT NULL IDENTITY PRIMARY KEY,
  NAME                 NVARCHAR(MAX) COLLATE Cyrillic_General_CI_AS_KS NOT NULL,
  MALFUNCTION_GROUP_ID INT                                             NOT NULL,

  CREATE_DATE          DATETIME DEFAULT (GETDATE()),
  MOD_DATE             DATETIME DEFAULT (GETDATE()),
  CREATE_ID            INT,
  MOD_ID               INT,
  CONSTRAINT FK_MALFUNCTION_SUBGROUP_MALFUNCTION_GROUP
    FOREIGN KEY (MALFUNCTION_GROUP_ID)
      REFERENCES MALFUNCTION_GROUP (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_CREATE_MALFUNCTION_SUBGROUP_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_MALFUNCTION_SUBGROUP_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION
);
GO

CREATE TABLE MALFUNCTION
(
  ID                      INT                                             NOT NULL IDENTITY PRIMARY KEY,
  NAME                    NVARCHAR(MAX) COLLATE Cyrillic_General_CI_AS_KS NOT NULL,
  MALFUNCTION_SUBGROUP_ID INT                                             NOT NULL,

  CREATE_DATE             DATETIME DEFAULT (GETDATE()),
  MOD_DATE                DATETIME DEFAULT (GETDATE()),
  CREATE_ID               INT,
  MOD_ID                  INT,
  CONSTRAINT FK_MALFUNCTION_SUBGROUP_MALFUNCTION_SUBGROUP
    FOREIGN KEY (MALFUNCTION_SUBGROUP_ID)
      REFERENCES [MALFUNCTION_SUBGROUP] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_CREATE_MALFUNCTION_ROLE
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_MALFUNCTION_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION
);
GO

CREATE TABLE STATE
(
  ID          INT NOT NULL IDENTITY PRIMARY KEY,
  NAME        NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS,
  TRANS_NAME  NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS UNIQUE,
  IS_FIXED    BIT NOT NULL      DEFAULT (0),

  CREATE_DATE DATETIME DEFAULT (GETDATE()),
  MOD_DATE    DATETIME DEFAULT (GETDATE()),
  CREATE_ID   INT,
  MOD_ID      INT
);
GO

CREATE TABLE POST (
	ID INT PRIMARY KEY NOT NULL IDENTITY,
	NAME NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS UNIQUE,

	CREATE_DATE DATETIME DEFAULT (GETDATE()),
	MOD_DATE    DATETIME DEFAULT (GETDATE()),
	CREATE_ID   INT,
	MOD_ID      INT,
	
	CONSTRAINT FK_MOD_POST_USER
		FOREIGN KEY (CREATE_ID)
			REFERENCES [USER] (ID)
			ON DELETE NO ACTION,
	CONSTRAINT FK_MOD_POST_ROLE
		FOREIGN KEY (MOD_ID)
			REFERENCES [USER] (ID)
			ON DELETE NO ACTION,
);
GO

CREATE TABLE EMPLOYEE(
	ID INT NOT NULL IDENTITY PRIMARY KEY,
	FIRST_NAME NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS,
	MIDDLE_NAME NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS,
	LAST_NAME NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS,
	SHORT_NAME NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS NOT NULL,
	BOARD_NUMBER INT NOT NULL,
	POST_ID INT NOT NULL,
	
	CREATE_DATE DATETIME DEFAULT (GETDATE()),
	MOD_DATE    DATETIME DEFAULT (GETDATE()),
	CREATE_ID   INT,
	MOD_ID      INT,
	
	CONSTRAINT FK_MOD_EMPLOYEE_USER
		FOREIGN KEY (CREATE_ID)
			REFERENCES [USER] (ID)
			ON DELETE NO ACTION,
	CONSTRAINT FK_MOD_EMPLOYEE_ROLE
		FOREIGN KEY (MOD_ID)
			REFERENCES [USER] (ID)
			ON DELETE NO ACTION,
	CONSTRAINT FK_EMPLOYEE_POST
		FOREIGN KEY (POST_ID)
			REFERENCES [POST] (ID)
			ON DELETE NO ACTION,
	CONSTRAINT CK_EMPLOYEE_BOARD_NUMBER
      CHECK (BOARD_NUMBER>=0 and BOARD_NUMBER<=1000000000),
	CONSTRAINT UQ_EMPLOYEE_BOARD_NUMBER_UNIQUE UNIQUE (BOARD_NUMBER)
);
GO

CREATE TABLE ISSUE
(
  ID             INT NOT NULL IDENTITY PRIMARY KEY,
  SUMMARY        NVARCHAR(MAX) COLLATE Cyrillic_General_CI_AS_KS,
  WARRANTY       INT,
  DEADLINE       DATETIME,
  NUMBER         INT NOT NULL,
  PRIORITY       INT NOT NULL  DEFAULT(0),

  STATE_ID       INT NOT NULL,
  ASSIGNED_TO    INT,
  VEHICLE_ID     INT NOT NULL,
  MALFUNCTION_ID INT,

  CREATE_DATE    DATETIME DEFAULT (GETDATE()),
  MOD_DATE       DATETIME DEFAULT (GETDATE()),
  CREATE_ID      INT,
  MOD_ID         INT,

  CONSTRAINT FK_ISSUE_STATE
    FOREIGN KEY (STATE_ID)
      REFERENCES STATE (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_ISSUE_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_CREATE_ISSUE_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_ISSUE_EMPLOYEE_ASSIGNED_TO
    FOREIGN KEY (ASSIGNED_TO)
      REFERENCES [EMPLOYEE] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_ISSUE_VEHICLE
    FOREIGN KEY (VEHICLE_ID)
      REFERENCES [VEHICLE] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_ISSUE_MALFUNCTION
    FOREIGN KEY (MALFUNCTION_ID)
      REFERENCES [MALFUNCTION] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT CK_ISSUE_PRIORITY
      CHECK (PRIORITY>=0 and PRIORITY<=2)
);
GO

CREATE TABLE ACTION_TYPE
(
  ID          INT                                            NOT NULL IDENTITY PRIMARY KEY,
  NAME        NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS NOT NULL UNIQUE,

  CREATE_DATE DATETIME DEFAULT (GETDATE()),
  MOD_DATE    DATETIME DEFAULT (GETDATE()),
  CREATE_ID   INT,
  MOD_ID      INT,
  CONSTRAINT FK_MOD_ACTION_TYPE_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_CREATE_ACTION_TYPE_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION
);
GO

CREATE TABLE SUPPLIER
(
  ID          INT                                            NOT NULL IDENTITY PRIMARY KEY,
  NAME        NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS NOT NULL UNIQUE,

  CREATE_DATE DATETIME DEFAULT (GETDATE()),
  MOD_DATE    DATETIME DEFAULT (GETDATE()),
  CREATE_ID   INT,
  MOD_ID      INT,
  CODE VARCHAR(10),
  FULL_NAME VARCHAR(100),
  COUNTRY INT,
  CURRENCY INT,
  EDRPOU VARCHAR(50),
  CONSTRAINT FK_CREATE_SUPPLIER_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_SUPPLIER_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION
);
GO

CREATE TABLE ISSUE_LOG
(
  ID             INT NOT NULL IDENTITY PRIMARY KEY,
  DESCRIPTION    NVARCHAR(MAX) COLLATE Cyrillic_General_CI_AS_KS,
  EXPENSES       DECIMAL(10, 2),

  OLD_STATE_ID   INT NOT NULL,
  NEW_STATE_ID   INT NOT NULL,

  SUPPLIER_ID    INT,
  ACTION_TYPE_ID INT NOT NULL,
  ISSUE_ID       INT NOT NULL,

  CREATE_DATE    DATETIME DEFAULT (GETDATE()),
  MOD_DATE       DATETIME DEFAULT (GETDATE()),
  CREATE_ID      INT,
  MOD_ID         INT,
  CONSTRAINT FK_ISSUE_LOG_SUPPLIER
    FOREIGN KEY (SUPPLIER_ID)
      REFERENCES SUPPLIER (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_ISSUE_LOG_ACTION_TYPE
    FOREIGN KEY (ACTION_TYPE_ID)
      REFERENCES ACTION_TYPE (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_ISSUE_LOG_ISSUE
    FOREIGN KEY (ISSUE_ID)
      REFERENCES ISSUE (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_CREATE_ISSUE_LOG_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_ISSUE_LOG_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_OLD_ISSUE_LOG_STATE
    FOREIGN KEY (OLD_STATE_ID)
      REFERENCES STATE (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_NEW_ISSUE_LOG_STATE
    FOREIGN KEY (NEW_STATE_ID)
      REFERENCES STATE (ID)
      ON DELETE NO ACTION
);
GO

CREATE TABLE DOCUMENT
(
  ID           INT NOT NULL IDENTITY PRIMARY KEY,
  NAME         NVARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS,
  DESCRIPTION  NVARCHAR(MAX) COLLATE Cyrillic_General_CI_AS_KS,

  ISSUE_LOG_ID INT,
  CREATE_DATE  DATETIME DEFAULT (GETDATE()),
  MOD_DATE     DATETIME DEFAULT (GETDATE()),
  CREATE_ID    INT,
  MOD_ID       INT,
  CONSTRAINT FK_DOCUMENT_ISSUE_LOG
    FOREIGN KEY (ISSUE_LOG_ID)
      REFERENCES [ISSUE_LOG] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_CREATE_DOCUMENT_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_DOCUMENT_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION
);
GO

CREATE TABLE BILL
(
  ID          INT NOT NULL IDENTITY PRIMARY KEY,
  SUM         DECIMAL(20, 2),
  DOCUMENT_ID INT,
  ISSUE_ID    INT NOT NULL,

  CREATE_DATE DATETIME DEFAULT (GETDATE()),
  MOD_DATE    DATETIME DEFAULT (GETDATE()),
  CREATE_ID   INT,
  MOD_ID      INT,
  CONSTRAINT FK_BILL_ISSUE
    FOREIGN KEY (ISSUE_ID)
      REFERENCES ISSUE (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_CREATE_BILL_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_BILL_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_BILL_DOCUMENT
    FOREIGN KEY (DOCUMENT_ID)
      REFERENCES DOCUMENT (ID)
      ON DELETE NO ACTION
);
GO

CREATE TABLE CURRENCY
(
	ID INT NOT NULL IDENTITY PRIMARY KEY,
	SHORT_NAME VARCHAR(5) COLLATE Cyrillic_General_CI_AS_KS NOT NULL UNIQUE,
	FULL_NAME VARCHAR(25) COLLATE Cyrillic_General_CI_AS_KS NOT NULL,

  CREATE_DATE  DATETIME DEFAULT (GETDATE()),
  MOD_DATE     DATETIME DEFAULT (GETDATE()),
  CREATE_ID    INT,
  MOD_ID       INT,
  CONSTRAINT FK_CREATE_CURRENCY_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_CURRENCY_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION
);
GO

CREATE TABLE COUNTRY
(
	ID INT NOT NULL IDENTITY PRIMARY KEY,
	NAME VARCHAR(50) COLLATE Cyrillic_General_CI_AS_KS NOT NULL UNIQUE,

  CREATE_DATE  DATETIME DEFAULT (GETDATE()),
  MOD_DATE     DATETIME DEFAULT (GETDATE()),
  CREATE_ID    INT,
  MOD_ID       INT,
  CONSTRAINT FK_CREATE_COUNTRY_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_COUNTRY_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION
);
GO

CREATE TABLE TRANSITION (
  ID INT PRIMARY KEY NOT NULL IDENTITY,
  FROM_STATE_ID INT NOT NULL,
  TO_STATE_ID INT NOT NULL,
  ACTION_TYPE_ID INT NOT NULL,

  CREATE_DATE DATETIME DEFAULT (GETDATE()),
  MOD_DATE    DATETIME DEFAULT (GETDATE()),
  CREATE_ID   INT,
  MOD_ID      INT,
  
  CONSTRAINT FK_CREATE_ISSUE_TRANSITION_USER
    FOREIGN KEY (CREATE_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_MOD_ISSUE_TRANSITION_USER
    FOREIGN KEY (MOD_ID)
      REFERENCES [USER] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_FROM_STATE
    FOREIGN KEY (FROM_STATE_ID)
      REFERENCES [STATE](ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_TO_STATE
    FOREIGN KEY (TO_STATE_ID)
      REFERENCES [STATE] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT FK_ACTION_TYPE_ISSUE
    FOREIGN KEY (ACTION_TYPE_ID)
      REFERENCES [ACTION_TYPE] (ID)
      ON DELETE NO ACTION,
  CONSTRAINT CK_ISSUE_TRANSITION_UNIQUE UNIQUE (FROM_STATE_ID, ACTION_TYPE_ID,TO_STATE_ID)
);
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_ISSUE_UPDATE]
  ON [dbo].[ISSUE]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[ISSUE]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [ISSUE].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_ROLE_UPDATE]
  ON [dbo].[ROLE]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[ROLE]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [ROLE].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_USER_UPDATE]
  ON [dbo].[USER]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[USER]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [USER].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_VEHICLE_TYPE_UPDATE]
  ON [dbo].[VEHICLE_TYPE]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[VEHICLE_TYPE]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [VEHICLE_TYPE].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_VEHICLE_UPDATE]
  ON [dbo].[VEHICLE]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[VEHICLE]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [VEHICLE].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_MALFUNCTION_GROUP_UPDATE]
  ON [dbo].[MALFUNCTION_GROUP]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[MALFUNCTION_GROUP]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [MALFUNCTION_GROUP].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_MALFUNCTION_SUBGROUP_UPDATE]
  ON [dbo].[MALFUNCTION_SUBGROUP]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[MALFUNCTION_SUBGROUP]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [MALFUNCTION_SUBGROUP].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_MALFUNCTION_UPDATE]
  ON [dbo].[MALFUNCTION]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[MALFUNCTION]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [MALFUNCTION].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_DOCUMENT_UPDATE]
  ON [dbo].[DOCUMENT]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[DOCUMENT]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [DOCUMENT].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_BILL_UPDATE]
  ON [dbo].[BILL]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[BILL]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [BILL].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_SUPPLIER_UPDATE]
  ON [dbo].[SUPPLIER]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[SUPPLIER]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [SUPPLIER].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_ACTION_TYPE_UPDATE]
  ON [dbo].[ACTION_TYPE]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[ACTION_TYPE]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [ACTION_TYPE].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_TOKEN_UPDATE]
  ON [dbo].[TOKEN]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[TOKEN]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [TOKEN].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_ISSUE_LOG_UPDATE]
  ON [dbo].[ISSUE_LOG]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[ISSUE_LOG]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [ISSUE_LOG].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_STATE_UPDATE]
  ON [dbo].[STATE]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[STATE]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [STATE].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_CURRENCY_UPDATE]
  ON [dbo].[CURRENCY]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[CURRENCY]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [CURRENCY].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_COUNTRY_UPDATE]
  ON [dbo].[COUNTRY]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[COUNTRY]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [COUNTRY].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_EMPLOYEE_UPDATE]
  ON [dbo].[EMPLOYEE]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[EMPLOYEE]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [EMPLOYEE].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_POST_UPDATE]
  ON [dbo].[POST]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[POST]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [POST].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER [dbo].[TR_ASSIGN_DATE_TRANSITION_UPDATE]
  ON [dbo].[TRANSITION]
  AFTER UPDATE
  AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;
  UPDATE [dbo].[TRANSITION]
    SET MOD_DATE = GETDATE(),
        CREATE_DATE = OLD.CREATE_DATE,
        CREATE_ID = OLD.CREATE_ID
  FROM DELETED OLD
  WHERE [TRANSITION].ID IN (SELECT ID FROM INSERTED);
END
GO

CREATE TRIGGER TR_ISSUE_NUMBER_UPDATE
ON ISSUE
AFTER UPDATE
AS
BEGIN
  IF NOT EXISTS(SELECT * FROM INSERTED) OR (TRIGGER_NESTLEVEL() > 1)
    RETURN;

  UPDATE [dbo].[ISSUE]
  SET NUMBER = OLD.NUMBER
  FROM DELETED OLD
  INNER JOIN INSERTED ON INSERTED.ID = OLD.ID
  WHERE ISSUE.ID = INSERTED.ID;
END
GO

DBCC CHECKIDENT (ROLE, RESEED, 0);
DBCC CHECKIDENT (STATE, RESEED, 0);
GO

INSERT INTO [ROLE] (NAME, TRANS_NAME)
VALUES ('ADMIN', N'Адмін'),
       ('ENGINEER', N'Інженер'),
       ('CUSTOMER', N'Реєстратор'),
       ('ANALYST', N'Аналітик');
GO

INSERT INTO [STATE] (NAME, TRANS_NAME)
VALUES ('NEW', N'Нова'),
       ('VERIFIED', N'Верифіковано'),
       ('REJECTED', N'Відхилено'),
       ('TODO', N'До виконання'),
       ('EXECUTING', N'В роботі'),
       ('DONE', N'Готово'),
       ('CONFIRMED', N'Підтверджено'),
       ('UNCONFIRMED', N'Не підтверджено');
GO

-- admin helloworld
INSERT INTO [USER] (LOGIN, EMAIL, PASSWORD, ROLE_ID)
SELECT 'admin', 'admin@admin', 'VXcrydRLS7cIYPVpBOGFxg==:hR1FzwJv0k8YPOHWpJqRqQ==', ROLE_ID
FROM ROLE
WHERE NAME = 'ADMIN'
GO

ALTER TABLE ISSUE
  ADD CONSTRAINT DF_ISSUE_STATE
    DEFAULT (1) FOR STATE_ID;
GO

ALTER TABLE SUPPLIER
	ADD CONSTRAINT FK_Currency
	FOREIGN KEY (CURRENCY) REFERENCES CURRENCY(ID);
GO

ALTER TABLE SUPPLIER
	ADD CONSTRAINT FK_Country
	FOREIGN KEY (COUNTRY) REFERENCES COUNTRY(ID);
GO
