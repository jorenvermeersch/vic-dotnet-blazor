namespace Tests;

public class DurationTests
{
    [Fact]
    public void Duration_creation_is_correct()
    {
        Domain.Domain.TimeSpan timeSpan = new(new DateTime(2022, 5, 5), new DateTime(2022, 12, 20));

        timeSpan.StartDate.Month.ShouldBe(5);
        timeSpan.StartDate.Day.ShouldBe(5);
        timeSpan.EndDate.Month.ShouldBe(12);
        timeSpan.EndDate.Day.ShouldBe(20);
    }

    [Fact]
    public void Duration_endDate_before_startDate_is_invalid()
    {
        Should.Throw<ArgumentException>(() => new Domain.Domain.TimeSpan(new DateTime(2023, 5, 5), new DateTime(2022, 12, 20)));
    }
}
