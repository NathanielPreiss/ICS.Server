using ICS.Workout.Validation;

namespace ICS.Workout;

internal static class WorkoutValidation
{
    public static void Validate(this Workout workout)
    {
        if (workout.UserId == Guid.Empty)
        {
            throw new ArgumentException();
        }

        if (workout.WorkoutId == Guid.Empty)
        {
            throw new ArgumentException();
        }

        if (workout.Name == string.Empty)
        {
            throw new ArgumentException();
        }

        workout.Routines?.ToList().ForEach(x => x.Validate());
    }

    public static void Validate(this Workout workout, int expectedPosition)
    {
        workout.Validate();

        if (workout.Position != expectedPosition)
        {
            throw new ArgumentException();
        }
    }
}
