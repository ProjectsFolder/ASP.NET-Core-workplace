namespace WebTest.Transformers
{
    public interface ITransformer<TFrom, TTo>
    {
        TTo Transform(TFrom from);
    }
}
