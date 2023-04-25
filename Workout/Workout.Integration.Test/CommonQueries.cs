namespace ICS.Workout.Test;

public static class CommonQueries
{
    public static Task<Workout> GetRandomWorkout(this WorkoutDbContext dbContext) => dbContext.Workout
        .OrderBy(x => Guid.NewGuid())
        .Include(x => x.Routines)
        .FirstAsync(CancellationToken.None);

    public static Task<Routine> GetRandomRoutine(this WorkoutDbContext dbContext) => dbContext.Routine
        .OrderBy(x => Guid.NewGuid())
        .Include(x => x.Sets)
        .FirstAsync(CancellationToken.None);

    public static Task<Set> GetRandomSet(this WorkoutDbContext dbContext) => dbContext.Set
        .OrderBy(x => Guid.NewGuid())
        .FirstAsync(CancellationToken.None);

    /*public static Task<User> GetRandomUser(this WidgetDbContext dbContext) => dbContext.User
        .OrderBy(x => Guid.NewGuid())
        .FirstAsync(CancellationToken.None);

    public static Task<User> GetRandomUserWithoutWidgets(this WidgetDbContext dbContext) => dbContext.User
        .FirstAsync(x => !dbContext.UserWidget
            .OrderBy(x1 => Guid.NewGuid())
            .Select(x1 => x1.UserId)
            .Contains(x.UserId), CancellationToken.None);

    public static Task<int> GetRandomUserIdWithWidgets(this WidgetDbContext dbContext) => dbContext.UserWidget
        .OrderBy(x => Guid.NewGuid())
        .Select(x => x.UserId)
        .FirstAsync(CancellationToken.None);

    public static Task<int> GetRandomUserIdWithAllWidgets(this WidgetDbContext dbContext) => dbContext.UserWidget
        .GroupBy(x => x.UserId)
        .Where(x => x.Count() == EnumUtilities<WidgetTypes>.Count)
        .OrderBy(x1 => Guid.NewGuid())
        .Select(x => x.Key)
        .FirstAsync(CancellationToken.None);

    public static Task<List<UserWidget>> GetUserWidgets(this WidgetDbContext dbContext, int userId) => dbContext.UserWidget
        .Where(x => x.UserId == userId)
        .OrderBy(x => x.Position)
        .ToListAsync(CancellationToken.None);

    public static Task<List<RecentWorkoutsProjection>> GetRecentWorkoutsProjections(this WidgetDbContext dbContext, int userId) => dbContext.RecentWorkoutsProjection
        .Where(x => x.UserId == userId)
        .OrderByDescending(x => x.WorkoutCompletedUtc)
        .ToListAsync(CancellationToken.None);

    public static Task<List<WorkoutsPerWeekProjection>> GetWorkoutsPerWeekProjections(this WidgetDbContext dbContext, int userId) => dbContext.WorkoutsPerWeekProjection
        .Where(x => x.UserId == userId)
        .OrderByDescending(x => x.WorkoutCompletedUtc)
        .ToListAsync(CancellationToken.None);

    public static Task<RecentWorkoutsProjection> GetRandomRecentWorkoutsProjection(this WidgetDbContext dbContext) => dbContext.RecentWorkoutsProjection
        .OrderBy(x => Guid.NewGuid())
        .FirstAsync(CancellationToken.None);

    public static Task<WorkoutsPerWeekProjection> GetRandomWorkoutsPerWeekProjection(this WidgetDbContext dbContext) => dbContext.WorkoutsPerWeekProjection
        .OrderBy(x => Guid.NewGuid())
        .FirstAsync(CancellationToken.None);

    public static Task<List<int>> GetRandomUserIds(this WidgetDbContext dbContext, int userCount) => dbContext.User
        .OrderBy(x => Guid.NewGuid())
        .Take(userCount)
        .Select(x => x.UserId)
        .ToListAsync(CancellationToken.None);

    public static Task<List<User>> GetRandomUsersWithAllWidgets(this WidgetDbContext dbContext, int userCount) => dbContext.User
        .Include(x => x.UserWidgets)
        .Where(x => x.UserWidgets!.Count == EnumUtilities<WidgetTypes>.Count)
        .OrderBy(x1 => Guid.NewGuid())
        .Take(userCount)
        .ToListAsync(CancellationToken.None);

    public static Task<List<User>> GetRandomUsersWithLessThanAllWidgets(this WidgetDbContext dbContext, int userCount) => dbContext.User
        .Include(x => x.UserWidgets)
        .Where(x => x.UserWidgets!.Count < EnumUtilities<WidgetTypes>.Count)
        .OrderBy(x1 => Guid.NewGuid())
        .Take(userCount)
        .ToListAsync(CancellationToken.None);

    public static Task<List<User>> GetRandomUsersWithAtLeastOneWidgets(this WidgetDbContext dbContext, int userCount) => dbContext.User
        .Include(x => x.UserWidgets)
        .Where(x => x.UserWidgets!.Count > 0)
        .OrderBy(x1 => Guid.NewGuid())
        .Take(userCount)
        .ToListAsync(CancellationToken.None);

    public static async Task RemoveUsers(this WidgetDbContext dbContext, IEnumerable<int> userIds)
    {
        var workoutsPerWeek = await dbContext.WorkoutsPerWeekProjection
            .Where(x => userIds.Contains(x.UserId))
            .ToListAsync(CancellationToken.None)
            .ConfigureAwait(false);

        dbContext.WorkoutsPerWeekProjection.RemoveRange(workoutsPerWeek);

        var recentWorkouts = await dbContext.RecentWorkoutsProjection
            .Where(x => userIds.Contains(x.UserId))
            .ToListAsync(CancellationToken.None)
            .ConfigureAwait(false);

        dbContext.RecentWorkoutsProjection.RemoveRange(recentWorkouts);

        var users = await dbContext.User
            .Where(x => userIds.Contains(x.UserId))
            .Include(x => x.UserWidgets)
            .ToListAsync(CancellationToken.None)
            .ConfigureAwait(false);

        dbContext.UserWidget.RemoveRange(users.SelectMany(x => x.UserWidgets ?? Array.Empty<UserWidget>()));

        dbContext.User.RemoveRange(users);

        await dbContext
            .SaveChangesAsync(CancellationToken.None)
            .ConfigureAwait(false);
    }*/
}
