
SET IDENTITY_INSERT Tenant ON

INSERT INTO dbo.Tenant(id,TenantName,CreatedBy)
SELECT 1,'Internal',1

SET IDENTITY_INSERT Tenant OFF