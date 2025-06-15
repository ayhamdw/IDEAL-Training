namespace DataEntity.ViewModels;

public class MetaViewModel
{
    public int Total { get; set; }
    public int CurrentPage { get; set; }
    public int PerPage { get; set; }

    public MetaViewModel(int total, int currentPage, int perPage)
    {
        Total = total;
        CurrentPage = currentPage;
        PerPage = perPage;
    }
}