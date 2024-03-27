namespace Api.Responses.Documentation;

public class SuccessItemWithMeta<TResult, TMeta>
{
    public required TResult Item { get; set; }

    public required TMeta Meta { get; set; }
}
