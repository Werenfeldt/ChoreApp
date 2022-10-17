namespace ChoreApp.Web.Model;

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
        var workTimeSlotRepository = new WorkTimeSlotRepository(context);


        var family = await familyRepository.CreateFamilyAsync(new CreateFamilyDTO
        {
            Id = Guid.Parse("30bc356c-f2cf-42b6-961e-dfd178a50a66"),
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

        await choreRepository.CreateChoreAsync(new CreateChoreDTO
        {
            Name = "Tør støv af",
            Duration = Duration.ThirtyMinutes,
            Interval = Interval.OneWeek,
            Description = "Tør støv af i hele huset",
            Created = DateTime.UtcNow,
            CreatedByUserId = user.Id,
            FamilyId = family.Id,
            OneTimer = false
        });

        await choreRepository.CreateChoreAsync(new CreateChoreDTO
        {
            Name = "Klip Luna",
            Duration = Duration.TwoHour,
            Interval = Interval.OneMonth,
            Description = "Trim Lunas pels med 9mm",
            Created = DateTime.UtcNow,
            CreatedByUserId = user.Id,
            FamilyId = family.Id,
            OneTimer = false
        });
        await choreRepository.CreateChoreAsync(new CreateChoreDTO
        {
            Name = "Sæt ur op",
            Duration = Duration.TwentyMinutes,
            Interval = Interval.FiveDays,
            Description = "Sæt ur op i køkkenet",
            Created = DateTime.UtcNow,
            CreatedByUserId = user.Id,
            FamilyId = family.Id,
            OneTimer = true
        });

        await choreRepository.CreateChoreAsync(new CreateChoreDTO
        {
            Name = "Vand planter",
            Duration = Duration.TwentyMinutes,
            Interval = Interval.ThreeDays,
            Description = "Vand planer (husk dem på badeværelset)",
            Created = DateTime.UtcNow,
            CreatedByUserId = user.Id,
            FamilyId = family.Id,
            OneTimer = false
        });

        await workTimeSlotRepository.CreateWorkTimeSlotAsync(new CreateWorkTimeSlotDTO
        {
            Duration = Duration.OneHour,
            User = user,
            Weekday = DayOfWeek.Monday
        });
    }
}