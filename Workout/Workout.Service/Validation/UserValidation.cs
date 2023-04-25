using System.ComponentModel.DataAnnotations;

namespace ICS.Workout.Validation;

internal static class UserValidation
{
    public static void Validate(this User user)
    {
        if (user.UserId == Guid.Empty)
        {
            throw new MissingMemberException(nameof(User), nameof(User.UserId));
        }
    }
}
