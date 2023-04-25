namespace ICS.Workout.Validation;

internal static class RoutineValidation
{
    public static void Validate(this Routine routine)
    {
        routine.Sets.Validate();
        routine.Sets.ToList().Validate();
        routine.Sets.ToList().ForEach(x => x.Validate());
    }
}
