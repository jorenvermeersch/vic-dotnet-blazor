namespace Domain;
public class Duration {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Duration(DateTime startDate, DateTime endDate) {
        StartDate = startDate;
        EndDate = endDate;
    }
}