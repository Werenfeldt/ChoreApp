namespace Web.Model;

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
        var choreRepository = new ChoreRepository(context);

        var family = await familyRepository.CreateFamilyAsync(new CreateFamilyDTO
        {
            Name = "Nielsen"
        });

        var user = await userRepository.CreateUserAsync(new CreateUserDTO
        {
            Id = Guid.NewGuid(),
            Name = "Marie Nielsen",
            Age = 30,
            Family = family
        });

        await choreRepository.CreateChoreAsync(new CreateChoreDTO
        {
            Name = "Støvsug",
            Duration = Duration.OneHour,
            Interval = Interval.OneWeek,
            Description = "Støvsug hele huset også kælder",
            Created = DateTime.UtcNow,
            CreatedByUserId = user.Id,
            FamilyId = family.Id,
            OneTimer = false
        });
    }
}