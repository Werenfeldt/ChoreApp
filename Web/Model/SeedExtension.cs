namespace Web;

public static class SeedExtension
{
    public async static Task<IHost> Seed(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ChoreAppContext>();

            await SeedProjects(context);
        }
        return host;
    }

    private async static Task SeedProjects(ChoreAppContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.Migrate();
        await context.Database.MigrateAsync();

        var userRepository = new UserRepository(context);
        var familyRepository = new FamilyRepository(context);
        var ProjectRepository = new ChoreRepository(context);
    }
}