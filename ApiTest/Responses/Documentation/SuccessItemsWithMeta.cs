namespace Api.Responses.Documentation;

public class SuccessItemsWithMeta<TResult, TMeta>
{
    public required TResult[] Items { get; set; }

    public required TMeta Meta { get; set; }
}
