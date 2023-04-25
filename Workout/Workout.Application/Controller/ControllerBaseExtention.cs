namespace ICS.Workout;

public static class ControllerBaseExtension
{
    public static Workout WorkoutFactory(this ControllerBase controller, Guid userId, PostWorkoutRequest workoutRequest)
    {
        var workout = new Workout(userId, Guid.NewGuid(), default, workoutRequest.Name);

        workout.Routines = workoutRequest.Routines?.Select((routineRequest, position) =>
        {
            var routine = controller.RoutineFactory(workout.WorkoutId, routineRequest);
            routine.Workout = workout;
            routine.Position = position;
            return routine;
        }).ToList();

        return workout;
    }

    public static Routine RoutineFactory(this ControllerBase controller, Guid workoutId, PostRoutineRequest routineRequest)
    {
        var routine = new Routine(workoutId, Guid.NewGuid(), default, routineRequest.ExerciseId);

        routine.Sets = routineRequest.Sets?.Select((setRequest, position) =>
        {
            var set = controller.SetFactory(routine.RoutineId, setRequest);
            set.Routine = routine;
            set.Position = position;
            return set;
        }).ToList();

        return routine;
    }

    public static Set SetFactory(this ControllerBase controller, Guid routineId, PostSetRequest setRequest)
    {
        return new Set(routineId, Guid.NewGuid(), default, setRequest.Reps, setRequest.Weight);
    }

    public static ObjectResult ExceptionResult(this ControllerBase controller, Exception ex)
    {
        return ex switch
        {
            ForeignKeyViolationException icsEx => controller.StatusCode(StatusCodes.Status404NotFound, new ApiError(icsEx)),
            UpdateConcurrencyViolationException icsEx => controller.StatusCode(StatusCodes.Status404NotFound, new ApiError(icsEx)),
            UniqueConstraintViolationException icsEx => controller.StatusCode(StatusCodes.Status409Conflict, new ApiError(icsEx)),
            UniqueIndexViolationException icsEx => controller.StatusCode(StatusCodes.Status409Conflict, new ApiError(icsEx)),
            _ => controller.StatusCode(StatusCodes.Status500InternalServerError, new ApiError())
        };
    }
}
