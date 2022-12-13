namespace Domain.VirtualMachines;

public class TimeSpan
{
    #region Properties
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    #endregion

    #region Constructors
    private TimeSpan() { }
    public TimeSpan(DateTime startDate, DateTime endDate)
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
