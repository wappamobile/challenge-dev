USE master;  
GO  

-------------------------------------------------------------------------------
-- REMOVE DATABASE
-------------------------------------------------------------------------------
IF DB_ID (N'wappa') IS NOT NULL
BEGIN
  DROP DATABASE wappa;
END
GO
-------------------------------------------------------------------------------
-- CREATE DATABASE
-------------------------------------------------------------------------------
CREATE DATABASE wappa;  

GO 

USE wappa;
GO

-------------------------------------------------------------------------------
-- TABLE DRIVER
-------------------------------------------------------------------------------
CREATE TABLE dbo.DRIVER
( nCdDriver  INT IDENTITY(1,1) NOT NULL
, cDocument  VARCHAR(11)       NOT NULL
, cFirstName VARCHAR(50)       NOT NULL
, cLastName  VARCHAR(50)       NOT NULL
, dCreate    DATETIME          NOT NULL
  CONSTRAINT DF_DRIVER_dCREATE DEFAULT GETDATE()
, dUpdate    DATETIME          NOT NULL
  CONSTRAINT DF_DRIVER_dUpdate DEFAULT GETDATE()  
, CONSTRAINT PK_DRIVER PRIMARY KEY CLUSTERED (nCdDriver))

CREATE UNIQUE NONCLUSTERED INDEX IX_DRIVER_cDocument ON dbo.DRIVER (cDocument);

-------------------------------------------------------------------------------
-- TABLE VEHICLE
-------------------------------------------------------------------------------
CREATE TABLE dbo.VEHICLE
( iVehicle    INT         NOT NULL
, nCdDriver   INT         NOT NULL
, cPlate      VARCHAR(7)  NOT NULL
, cModel      VARCHAR(50) NOT NULL
, cFabricator VARCHAR(50) NOT NULL
, dCreate    DATETIME          NOT NULL
  CONSTRAINT DF_VEHICLE_dCREATE DEFAULT GETDATE()
, dUpdate    DATETIME          NOT NULL
  CONSTRAINT DF_VEHICLE_dUpdate DEFAULT GETDATE()  
, CONSTRAINT PK_VEHICLE PRIMARY KEY CLUSTERED (nCdDriver, iVehicle)
, CONSTRAINT FK_VEHICLE_DRIVER FOREIGN KEY (nCdDriver) REFERENCES dbo.DRIVER (nCdDriver)
)

-------------------------------------------------------------------------------
-- TABLE ADDRESS
-------------------------------------------------------------------------------
CREATE TABLE dbo.ADDRESS
( iAddress      INT            NOT NULL
, nCdDriver     INT            NOT NULL
, cPostalCode   VARCHAR(8)     NOT NULL
, cStreetName   VARCHAR(50)    NOT NULL
, cNumber       VARCHAR(15)    NULL
, cNeighborhood VARCHAR(50)    NULL
, cCity         VARCHAR(50)    NULL
, cStateCode    VARCHAR(02)    NULL
, cCountry      VARCHAR(30)    NULL
, nLongitude    NUMERIC(10, 6) NULL
, nLatitude     NUMERIC(10, 6) NULL
, dCreate    DATETIME          NOT NULL
  CONSTRAINT DF_ADDRESS_dCREATE DEFAULT GETDATE()
, dUpdate    DATETIME          NOT NULL
  CONSTRAINT DF_ADDRESS_dUpdate DEFAULT GETDATE()  
, CONSTRAINT PK_ADDRESS PRIMARY KEY CLUSTERED (nCdDriver, iAddress)
, CONSTRAINT FK_ADDRESS_DRIVER FOREIGN KEY (nCdDriver) REFERENCES dbo.DRIVER (nCdDriver))
------------------------------------------------------------------------------
-- PROCEDURE SAVE DRIVER
-------------------------------------------------------------------------------
GO
CREATE OR ALTER PROCEDURE dbo.pc_SaveDriver
( @AnCdDriver  INT 
, @AcDocument  VARCHAR(11)
, @AcFirstName VARCHAR(50)
, @AcLastName  VARCHAR(50)
)
AS
BEGIN
  MERGE dbo.DRIVER tTarget  
  USING (SELECT nCdDriver  = @AnCdDriver
              , cDocument  = @AcDocument
              , cFirstName = @AcFirstName
              , cLastName  = @AcLastName 
         ) tSource 
    ON (tTarget.nCdDriver = tSource.nCdDriver)  
    WHEN MATCHED THEN   
        UPDATE 
           SET cFirstName = tSource.cFirstName
             , cLastName  = tSource.cLastName 
             , dUpdate    = GETDATE()
    WHEN NOT MATCHED THEN  
    INSERT (cDocument, cFirstName, cLastName)  
    VALUES (tSource.cDocument, tSource.cFirstName, tSource.cLastName)  
    OUTPUT INSERTED.nCdDriver; 
END

------------------------------------------------------------------------------
-- PROCEDURE DELETE DRIVER
-------------------------------------------------------------------------------
GO
CREATE OR ALTER PROCEDURE dbo.pc_DeleteDriver
( @AnCdDriver  INT 
)
AS
BEGIN
  DELETE dbo.DRIVER 
  OUTPUT DELETED.nCdDriver
   WHERE nCdDriver = @AnCdDriver     
END

------------------------------------------------------------------------------
-- PROCEDURE SELECT DRIVER
-------------------------------------------------------------------------------
GO
CREATE OR ALTER PROCEDURE dbo.pc_SelectDriver
( @AnCdDriver  INT         = NULL
, @AcDocument  VARCHAR(11) = NULL 
, @AcFirstName VARCHAR(50) = NULL
, @AcLastName  VARCHAR(50) = NULL
)
AS
BEGIN
  DECLARE @cCommandText NVARCHAR(max) =
    N'SELECT Id        = tDriver.nCdDriver   
           , Document  = tDriver.cDocument 
           , FirstName = tDriver.cFirstName 
           , LastName  = tDriver.cLastName
        FROM dbo.DRIVER tDriver
       WHERE tDriver.nCdDriver IS NOT NULL
             {nCdDriver}
             {cDocument}
             {cFirstName}
             {cLastName}
        '
    SELECT @cCommandText = REPLACE(@cCommandText, '{nCdDriver}' , IIF(ISNULL(@AnCdDriver, 0) <> 0  , '', 'AND nCdDriver = @AnCdDriver'))
         , @cCommandText = REPLACE(@cCommandText, '{cDocument}' , IIF(ISNULL(@AcDocument, '') = '' , '', 'AND cDocument like @AcDocument'))
         , @cCommandText = REPLACE(@cCommandText, '{cFirstName}', IIF(ISNULL(@AcFirstName, '') = '', '', 'AND cFirstName like @AcFirstName'))
         , @cCommandText = REPLACE(@cCommandText, '{cLastName}' , IIF(ISNULL(@AcLastName, '') = '' , '', 'AND cLastName like @AcLastName'))

    EXECUTE sp_executesql @cCommandText
                      , N' @AnCdDriver  INT         = NULL
                         , @AcDocument  VARCHAR(11) = NULL 
                         , @AcFirstName VARCHAR(50) = NULL
                         , @AcLastName  VARCHAR(50) = NULL'
                      , @AnCdDriver  
                      , @AcDocument  
                      , @AcFirstName 
                      , @AcLastName  
END

------------------------------------------------------------------------------
-- PROCEDURE SAVE VEHICLE
-------------------------------------------------------------------------------
GO
CREATE OR ALTER PROCEDURE dbo.pc_SaveVehicle
( @AnCdDriver   INT 
, @AiVehicle    INT                
, @AcPlate      VARCHAR(7)  
, @AcModel      VARCHAR(50) 
, @AcFabricator VARCHAR(50) 
)
AS
BEGIN
  MERGE dbo.VEHICLE tTarget  
  USING (SELECT nCdDriver   = @AnCdDriver
              , iVehicle    = @AiVehicle              
              , cPlate      = @AcPlate
              , cModel      = @AcModel
              , cFabricator = @AcFabricator 
              , iVehicleMax = ISNULL( (SELECT MAX(iVehicle)
                                         FROM dbo.VEHICLE
                                         WHERE nCdDriver   = @AnCdDriver
                                      ), 0) + 1
         ) tSource 
    ON (   tTarget.nCdDriver = tSource.nCdDriver
       AND tTarget.iVehicle = tSource.iVehicle)  
    WHEN MATCHED THEN   
        UPDATE 
           SET cPlate      = @AcPlate
             , cModel      = @AcModel
             , cFabricator = @AcFabricator 
             , dUpdate     = GETDATE()
    WHEN NOT MATCHED THEN  
    INSERT (nCdDriver, iVehicle, cPlate, cModel, cFabricator)  
    VALUES (tSource.nCdDriver, tSource.iVehicleMax, tSource.cPlate, tSource.cModel, tSource.cFabricator)  
    OUTPUT INSERTED.nCdDriver as DriverId
         , INSERTED.iVehicle  as Id; 
END

------------------------------------------------------------------------------
-- PROCEDURE DELETE DRIVER
-------------------------------------------------------------------------------
GO
CREATE OR ALTER PROCEDURE dbo.pc_DeleteVehicle
( @AnCdDriver INT 
, @AiVehicle  INT 
)
AS
BEGIN
  DELETE dbo.VEHICLE 
  OUTPUT DELETED.nCdDriver, DELETED.iVehicle
   WHERE nCdDriver = @AnCdDriver     
     AND iVehicle  = @AiVehicle     
END

------------------------------------------------------------------------------
-- PROCEDURE SELECT VEHICLE
-------------------------------------------------------------------------------
GO
CREATE OR ALTER PROCEDURE dbo.pc_SelectVehicles
( @AnCdDriver INT 
)
AS
BEGIN
  SELECT DriverId = tVehicle.nCdDriver
       , Id = tVehicle.iVehicle
       , Plate = tVehicle.cPlate
       , Model = tVehicle.cModel
       , Fabricator = tVehicle.cFabricator
    FROM dbo.VEHICLE tVehicle
   WHERE tVehicle.nCdDriver = @AnCdDriver
END
------------------------------------------------------------------------------
-- PROCEDURE SAVE Address
-------------------------------------------------------------------------------
GO
CREATE OR ALTER PROCEDURE dbo.pc_SaveAddress
( @AiAddress      INT         
, @AnCdDriver     INT        
, @AcPostalCode   VARCHAR(8)  
, @AcStreetName   VARCHAR(50) 
, @AcNumber       VARCHAR(15)     = NULL
, @AcNeighborhood VARCHAR(50)     = NULL
, @AcCity         VARCHAR(50)     = NULL
, @AcStateCode    VARCHAR(02)     = NULL
, @AcCountry      VARCHAR(30)     = NULL 
, @AnLongitude    NUMERIC(10, 6)  = NULL
, @AnLatitude     NUMERIC(10, 6)  = NULL
)
AS
BEGIN
  MERGE dbo.Address tTarget  
  USING (SELECT nCdDriver     = @AnCdDriver
              , iAddress      = @AiAddress              
              , cPostalCode   = @AcPostalCode
              , cStreetName   = @AcStreetName
              , cNumber         = @AcNumber
              , cNeighborhood = @AcNeighborhood
              , cCity         = @AcCity
              , cStateCode    = @AcStateCode
              , cCountry      = @AcCountry
              , nLongitude    = @AnLongitude    
              , nLatitude     = @AnLatitude
              , iAddressMax   = ISNULL( (SELECT MAX(iAddress)
                                           FROM dbo.Address
                                           WHERE nCdDriver   = @AnCdDriver
                                        ), 0) + 1
         ) tSource 
    ON (   tTarget.nCdDriver = tSource.nCdDriver
       AND tTarget.iAddress = tSource.iAddress)  
    WHEN MATCHED THEN   
        UPDATE 
           SET  cPostalCode   = tSource.cPostalCode
              , cStreetName   = tSource.cStreetName
              , cNumber       = tSource.cNumber
              , cNeighborhood = tSource.cNeighborhood
              , cCity         = tSource.cCity
              , cStateCode    = tSource.cStateCode
              , cCountry      = tSource.cCountry
              , nLongitude    = tSource.nLongitude
              , nLatitude     = tSource.nLatitude
              , dUpdate       = GETDATE()
    WHEN NOT MATCHED THEN  
    INSERT ( nCdDriver
           , iAddress
           , cPostalCode
           , cStreetName
           , cNumber
           , cNeighborhood
           , cCity
           , cStateCode
           , cCountry
           , nLongitude
           , nLatitude)  
    VALUES ( tSource.nCdDriver
           , tSource.iAddressMax
           , tSource.cPostalCode
           , tSource.cStreetName
           , tSource.cNumber
           , tSource.cNeighborhood
           , tSource.cCity
           , tSource.cStateCode
           , tSource.cCountry
           , tSource.nLongitude
           , tSource.nLatitude
           )  
    OUTPUT INSERTED.nCdDriver as DriverId
         , INSERTED.iAddress  as Id; 
END

------------------------------------------------------------------------------
-- PROCEDURE DELETE DRIVER
-------------------------------------------------------------------------------
GO
CREATE OR ALTER PROCEDURE dbo.pc_DeleteAddress
( @AnCdDriver INT 
, @AiAddress  INT 
)
AS
BEGIN
  DELETE dbo.Address 
  OUTPUT DELETED.nCdDriver, DELETED.iAddress
   WHERE nCdDriver = @AnCdDriver     
     AND iAddress  = @AiAddress     
END

------------------------------------------------------------------------------
-- PROCEDURE SELECT Address
-------------------------------------------------------------------------------
GO
CREATE OR ALTER PROCEDURE dbo.pc_SelectAdresses
( @AnCdDriver INT 
)
AS
BEGIN
  SELECT DriverId      = tAddress.nCdDriver
       , Id            = tAddress.iAddress
       , PostalCode    = tAddress.cPostalCode
       , StreetName    = tAddress.cStreetName
       , cNumber       = tAddress.cNumber
       , Neighborhood  = tAddress.cNeighborhood
       , City          = tAddress.cCity
       , StateCode     = tAddress.cStateCode
       , Country       = tAddress.cCountry
       , Longitude     = tAddress.nLongitude
       , Latitude      = tAddress.nLatitude
    FROM dbo.Address tAddress
   WHERE tAddress.nCdDriver = @AnCdDriver
END