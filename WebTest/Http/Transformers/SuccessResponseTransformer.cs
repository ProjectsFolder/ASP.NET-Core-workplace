using WebTest.Http.Responses;
using WebTest.Services.Database.Dto;
using WebTest.Transformers;

namespace WebTest.Http.Transformers
{
    public static class SuccessResponseTransformer
    {
        public static SuccessDto<TResult> Build<TModel, TResult>(
            TModel data,
            ITransformer<TModel, TResult> transformer,
            object? meta = null)
            where TModel : class
            where TResult : class
        {
            var response = new SuccessDto<TResult>
            {
                Item = transformer.Transform(data),
            };

            if (meta != null)
            {
                response.Meta = meta;
            }

            return response;
        }

        public static SuccessDto<TModel> Build<TModel>(
            TModel data,
            object? meta = null)
            where TModel : class
        {
            var response = new SuccessDto<TModel>
            {
                Item = data,
            };

            if (meta != null)
            {
                response.Meta = meta;
            }

            return response;
        }

        public static SuccessDto<TResult> Build<TModel, TResult>(
            IEnumerable<TModel> data,
            ITransformer<TModel, TResult> transformer,
            object? meta = null)
            where TModel : class
            where TResult : class
        {
            var response = new SuccessDto<TResult>();
            var list = new List<TResult>();
            foreach (var item in data)
            {
                list.Add(transformer.Transform(item));
            }
            response.Items = list;

            if (meta != null)
            {
                response.Meta = meta;
            }

            return response;
        }

        public static SuccessDto<TModel> Build<TModel>(
            IEnumerable<TModel> data,
            object? meta = null)
            where TModel : class
        {
            var response = new SuccessDto<TModel>
            {
                Items = data
            };

            if (meta != null)
            {
                response.Meta = meta;
            }

            return response;
        }
    }
}
