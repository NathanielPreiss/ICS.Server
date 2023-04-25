namespace ICS.Workout.Test;

public static class Fakers
{
    public static Faker<User> UserFaker => new Faker<User>()
        .CustomInstantiator(_ => new User(default))
        .RuleFor(x => x.UserId, faker => faker.Random.Guid())
        .Ignore(x => x.Workouts);

    public static Faker<Workout> WorkoutFaker => new Faker<Workout>()
        .CustomInstantiator(_ => new Workout(default, default, default, string.Empty))
        .RuleFor(x => x.UserId, faker => faker.Random.Guid())
        .RuleFor(x => x.WorkoutId, faker => faker.Random.Guid())
        .RuleFor(x => x.Name, faker => faker.Name.JobTitle())
        .RuleFor(x => x.Position, faker => faker.IndexFaker)
        .Ignore(x => x.User)
        .Ignore(x => x.Routines);

    public static Faker<Routine> RoutineFaker => new Faker<Routine>()
        .CustomInstantiator(_ => new Routine(default, default, default, default))
        .RuleFor(x => x.WorkoutId, faker => faker.Random.Guid())
        .RuleFor(x => x.RoutineId, faker => faker.Random.Guid())
        .RuleFor(x => x.Position, faker => faker.IndexFaker)
        .RuleFor(x => x.ExerciseId, faker => faker.PickRandom<ExerciseTypes>())
        .Ignore(x => x.Workout)
        .Ignore(x => x.Sets);

    public static Faker<Set> SetFaker => new Faker<Set>()
        .CustomInstantiator(_ => new Set(default, default, default, default, default))
        .RuleFor(x => x.RoutineId, faker => faker.Random.Guid())
        .RuleFor(x => x.SetId, faker => faker.Random.Guid())
        .RuleFor(x => x.Position, faker => faker.IndexFaker)
        .RuleFor(x => x.Reps, faker => faker.Random.Int(1, 10))
        .RuleFor(x => x.Weight, faker => faker.Random.Int(5, 100))
        .Ignore(x => x.Routine);
}