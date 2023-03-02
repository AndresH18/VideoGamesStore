namespace GameStore.Data.ViewModels;

public class PageInfo
{
    public required int TotalItems { get; set; }
    public required int ItemsPerPage { get; set; }
    public required int CurrentPage { get; set; }

    public int TotalPages => (int) Math.Ceiling((decimal) TotalItems / ItemsPerPage);
}