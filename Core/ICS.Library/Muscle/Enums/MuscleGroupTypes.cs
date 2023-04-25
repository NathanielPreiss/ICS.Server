namespace ICS.Muscle;

public enum MuscleGroupTypes
{
    [Description("Invalid")]
    Invalid = 0,

    // Upper
    [Description("Neck")]
    Neck = 110000,
    [Description("Shoulder")]
    Shoulder = 120000,
    [Description("Chest")]
    Chest = 130000,
    [Description("Upper Back")]
    UpperBack = 140000,
    [Description("UpperArm")]
    UpperArm = 150000,
    [Description("Forearm")]
    Forearm = 160000,

    // Core
    [Description("Lower Back")]
    LowerBack = 210000,
    [Description("Abdomen")]
    Abdomen = 220000,

    // Lower
    [Description("Hip")]
    Hip = 310000,
    [Description("Thigh")]
    Thigh = 320000,
    [Description("Calve")]
    Calve = 330000
}
