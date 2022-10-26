using Ardalis.GuardClauses;

namespace Domain.Domain;
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
        if (endDate > startDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
        else
        {
            throw new ArgumentException("end date should be after start date");
        }
        
    }
    #endregion
}