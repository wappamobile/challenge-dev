------------------------------------------------------------------------------
-- PROCEDURE SAVE Address
-------------------------------------------------------------------------------
GO
CREATE OR ALTER PROCEDURE dbo.pc_SaveAddress
( @AiAddress      INT         
, @AnCdDriver     INT        
, @AcPostalCode   VARCHAR(8)  
, @AcStreetName   VARCHAR(50) 
, @AAddressNumber VARCHAR(15) = NULL
, @AcNeighborhood VARCHAR(50) = NULL
, @AcCity         VARCHAR(50) = NULL
, @AcStateCode    VARCHAR(02) = NULL
, @AcCountry      VARCHAR(30) = NULL 
)
AS
BEGIN
  MERGE dbo.Address tTarget  
  USING (SELECT nCdDriver     = @AnCdDriver
              , iAddress      = @AiAddress              
              , cPostalCode   = @AcPostalCode
              , cStreetName   = @AcStreetName
              , AddressNumber = @AAddressNumber
              , cNeighborhood = @AcNeighborhood
              , cCity         = @AcCity
              , cStateCode    = @AcStateCode
              , cCountry      = @AcCountry
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
              , AddressNumber = tSource.AddressNumber
              , cNeighborhood = tSource.cNeighborhood
              , cCity         = tSource.cCity
              , cStateCode    = tSource.cStateCode
              , cCountry      = tSource.cCountry
    WHEN NOT MATCHED THEN  
    INSERT ( nCdDriver
           , iAddress
           , cPostalCode
           , cStreetName
           , cNeighborhood
           , cCity
           , cStateCode
           , cCountry)  
    VALUES ( tSource.nCdDriver
           , tSource.iAddressMax
           , tSource.cPostalCode
           , tSource.cStreetName
           , tSource.cNeighborhood
           , tSource.cCity
           , tSource.cStateCode
           , tSource.cCountry
           )  
    OUTPUT INSERTED.nCdDriver, INSERTED.iAddress; 
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
  SELECT tAddress.nCdDriver
       , tAddress.iAddress
       , tAddress.cPostalCode
       , tAddress.cStreetName
       , tAddress.cNeighborhood
       , tAddress.cCity
       , tAddress.cStateCode
       , tAddress.cCountry
    FROM dbo.Address tAddress
   WHERE tAddress.nCdDriver = @AnCdDriver
END

GO 
EXECUTE pc_SelectAdresses 1