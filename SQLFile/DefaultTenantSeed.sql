
SET IDENTITY_INSERT Tenant ON

INSERT INTO dbo.Tenant(id,TenantName,CreatedBy)
SELECT 0,'Internal',0

SET IDENTITY_INSERT Tenant OFF