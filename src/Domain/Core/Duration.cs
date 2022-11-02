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
        if (endDate < startDate)
        {
            throw new ArgumentException("End Date must be later than Start Date");
        }
        StartDate = startDate;
        EndDate = endDate;
    }
    #endregion
}
