namespace GameStore.Data.ViewModels;

public class ListViewModel<TModel>
{

    public IEnumerable<TModel> Items { get; set; } = Enumerable.Empty<TModel>();
    public PageInfo? PageInfo { get; set; }
}