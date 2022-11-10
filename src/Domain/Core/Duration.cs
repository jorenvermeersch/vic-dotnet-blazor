namespace Domain.Core;

[ToString]
public class Duration
{
    #region Properties
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    #endregion

    #region Constructors
    public Duration(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;

        if (endDate < startDate)
        {
            throw new ArgumentException("End Date must be later than Start Date");
        }
    }
    #endregion
}
