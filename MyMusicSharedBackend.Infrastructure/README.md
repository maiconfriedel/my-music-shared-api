# Infrastructure

### Add New Migration

    dotnet ef migrations add "MigrationName" -o "EntityFramework/Migrations" --project MyMusicSharedBackend.Infrastructure --startup-project MyMusicSharedBackend --context MyMusicSharedDbContext

### Remove the Last Migration

    dotnet ef migrations remove --project MyMusicSharedBackend.Infrastructure --startup-project MyMusicSharedBackend --context MyMusicSharedDbContext

### Update the Database

    dotnet ef database update --project MyMusicSharedBackend.Infrastructure --startup-project MyMusicSharedBackend --context MyMusicSharedDbContext
