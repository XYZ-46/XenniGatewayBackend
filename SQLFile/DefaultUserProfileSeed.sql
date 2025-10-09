SET IDENTITY_INSERT UserProfile ON

INSERT INTO dbo.UserProfile(Id,FullName,NickName,Email,TenantId,IsActive,CreatedBy)
SELECT 1,'SystemApp','SystemApp','admin@admin.com',1,0,1

SET IDENTITY_INSERT UserProfile OFF