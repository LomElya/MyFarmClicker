public enum IndustrySubjects
{
    Empty = 1 << 0,
    Wheat = 1 << 1,
    Potato= 1 << 2,
    Tomato= 1 << 3,
    Grape= 1 << 4,
    Chicken= 1 << 5,
    Pig= 1 << 6,
    Cow= 1 << 7,
    Sheep= 1 << 8,
    Chip= 1 << 9,
    Telephone= 1 << 10,
    Computer= 1 << 11,
    Rocket= 1 << 12,

    Farm = Wheat | Potato | Tomato | Grape,
    Animal = Chicken | Pig | Cow | Sheep,
    Technology = Chip | Telephone | Computer | Rocket,
}
