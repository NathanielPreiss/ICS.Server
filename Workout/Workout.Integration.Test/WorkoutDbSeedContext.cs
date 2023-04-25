using Bogus.Extensions;

namespace ICS.Workout.Test;

public class WorkoutDbSeedContext : WorkoutDbContext
{
    private readonly bool _skipDataSeed;
    private readonly int _userCount;
    private readonly int _workoutUpperLimit;
    private readonly int _routineUpperLimit;
    private readonly int _setUpperLimit;

    public WorkoutDbSeedContext(
        DbContextOptions<WorkoutDbContext> options) :
        base(options)
    {
        _skipDataSeed = true;
        _userCount = 0;
        _workoutUpperLimit = 0;
        _routineUpperLimit = 0;
        _setUpperLimit = 0;
    }

    public WorkoutDbSeedContext(
        int userCount,
        int workoutUpperLimit,
        int routineUpperLimit,
        int setUpperLimit,
        DbContextOptions<WorkoutDbContext> options) :
        base(options)
    {
        _skipDataSeed = false;
        _userCount = userCount;
        _workoutUpperLimit = workoutUpperLimit;
        _routineUpperLimit = routineUpperLimit;
        _setUpperLimit = setUpperLimit;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (_skipDataSeed)
            return;

        Faker.GlobalUniqueIndex = 0;

        // Create Users
        var users = Fakers.UserFaker.Generate(_userCount);
        modelBuilder.Entity<User>().HasData(users);

        // Create Workouts
        var workouts = users.SelectMany(user =>
            Fakers.WorkoutFaker
                .RuleFor(x => x.UserId, user.UserId)
                .GenerateBetween(0, _workoutUpperLimit))
            .ToList();
        modelBuilder.Entity<Workout>().HasData(workouts);

        // Create Routines
        var routines = workouts.SelectMany(workout =>
            Fakers.RoutineFaker
                .RuleFor(x => x.WorkoutId, workout.WorkoutId)
                .GenerateBetween(0, _routineUpperLimit))
            .ToList();
        modelBuilder.Entity<Routine>().HasData(routines);

        // Create Sets
        var sets = routines.SelectMany(routine =>
            Fakers.SetFaker
                .RuleFor(x => x.RoutineId, routine.RoutineId)
                .GenerateBetween(0, _setUpperLimit));
        modelBuilder.Entity<Set>().HasData(sets);
    }
}
