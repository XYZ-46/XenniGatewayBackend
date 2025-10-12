
SET IDENTITY_INSERT Tenant ON

INSERT INTO dbo.Tenant(Id,TenantName,IsActive,CreatedBy)
SELECT 0,'NONE',0,1 union all
SELECT 1,'Internal',1,1

SET IDENTITY_INSERT Tenant OFF