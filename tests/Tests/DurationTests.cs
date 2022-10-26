namespace Tests;

public class DurationTests
{
    [Fact]
    public void Duration_creation_is_correct()
    {
        Duration duration = new(new DateTime(2022,5,5), new DateTime(2022, 12, 20));

        duration.StartDate.Month.ShouldBe(5);
        duration.StartDate.Day.ShouldBe(5);
        duration.EndDate.Month.ShouldBe(12);
        duration.EndDate.Day.ShouldBe(20);
    }

    [Fact]
    public void Duration_endDate_before_startDate_is_invalid()
    {
        Should.Throw<ArgumentException>(() => new Duration(new DateTime(2023, 5, 5), new DateTime(2022, 12, 20)));
    }
}
