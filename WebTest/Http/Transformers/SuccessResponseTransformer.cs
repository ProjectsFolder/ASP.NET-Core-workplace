using WebTest.Http.Responses;
using WebTest.Services.Database.Dto;
using WebTest.Transformers;

namespace WebTest.Http.Transformers
{
    public static class SuccessResponseTransformer
    {
        public static SuccessDto Build<TModel, TResult>(
            TModel data,
            ITransformer<TModel, TResult> transformer,
            object? meta = null)
            where TModel : class
            where TResult : class
        {
            var response = new SuccessDto
            {
                Item = transformer.Transform(data),
            };

            if (meta != null)
            {
                response.Meta = meta;
            }

            return response;
        }

        public static SuccessDto Build<TModel>(
            TModel data,
            object? meta = null)
            where TModel : class
        {
            var response = new SuccessDto
            {
                Item = data,
            };

            if (meta != null)
            {
                response.Meta = meta;
            }

            return response;
        }

        public static SuccessDto Build<TModel, TResult>(
            IEnumerable<TModel> data,
            ITransformer<TModel, TResult> transformer,
            object? meta = null)
            where TModel : class
            where TResult : class
        {
            var response = new SuccessDto();
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

        public static SuccessDto Build<TModel>(
            IEnumerable<TModel> data,
            object? meta = null)
            where TModel : class
        {
            var response = new SuccessDto
            {
                Items = data
            };

            if (meta != null)
            {
                response.Meta = meta;
            }

            return response;
        }

        public static SuccessDto Build<TModel, TResult>(
            Paginator<TModel> data,
            ITransformer<TModel, TResult> transformer,
            Dictionary<string, object>? meta = null)
            where TModel : class
            where TResult : class
        {
            var response = new SuccessDto();
            var list = new List<TResult>();
            foreach (var item in data.Data)
            {
                list.Add(transformer.Transform(item));
            }
            response.Items = list;
            var paginatorMeta = new Dictionary<string, int>
            {
                { "perPage", data.PerPage },
                { "currentPage", data.CurrentPage },
                { "totalPages", data.TotalPages },
                { "total", data.Total }
            };

            meta ??= [];
            meta.Add("paginator", paginatorMeta);
            response.Meta = meta;

            return response;
        }

        public static SuccessDto Build<TModel>(
            Paginator<TModel> data,
            Dictionary<string, object>? meta = null)
            where TModel : class
        {
            var response = new SuccessDto
            {
                Items = data.Data
            };

            var paginatorMeta = new Dictionary<string, int>
            {
                { "perPage", data.PerPage },
                { "currentPage", data.CurrentPage },
                { "totalPages", data.TotalPages },
                { "total", data.Total }
            };

            meta ??= [];
            meta.Add("paginator", paginatorMeta);
            response.Meta = meta;

            return response;
        }
    }
}
