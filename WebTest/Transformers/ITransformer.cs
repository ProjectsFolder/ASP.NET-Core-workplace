namespace WebTest.Transformers
{
    public interface ITransformer<From, To>
    {
        To Transform(From from);
    }
}
