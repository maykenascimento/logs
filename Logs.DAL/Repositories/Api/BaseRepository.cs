using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Logs.BLL.Interfaces;
using Logs.BLL.Entities;
using System.Linq;
using Logs.Shared;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Web;

namespace Logs.DAL.Repositories.Api
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly string _baseUrl;
        private readonly string _pathUrl;

        public BaseRepository(AppDbContext context, string path)
        {
            _context = context;
            _baseUrl = "https://exampleservice.com/";
            _pathUrl = path;
        }

        public async Task<T> GetByIdAsync(int id) => await Get<T>(id);

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes) => await Get<T>(id);

        public virtual async Task<List<T>> GetAllAsync() => await Get<List<T>>();

        public virtual async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes) => await Get<List<T>>();

        public virtual async Task<T> AddAsync(T entity) => await Add<T>(entity);

        public virtual async Task UpdateAsync(T entity) => await Update<T>(entity, entity.Id);

        public virtual async Task DeleteAsync(T entity) => await Delete(entity.Id);

        #region Pagination
        public virtual async Task<QueryResult<T>> GetPageAsync(QueryParameters queryParams)
        {
            return await GetOrderedPageQueryResultAsync(queryParams, null);
        }

        public virtual async Task<QueryResult<T>> GetPageAsync(QueryParameters queryParams, Expression<Func<T, bool>> predicate)
        {
            return await GetOrderedPageQueryResultAsync(queryParams, null);
        }

        public virtual async Task<QueryResult<T>> GetPageAsync(QueryParameters queryParams, List<Expression<Func<T, object>>> includes)
        {
            return await GetOrderedPageQueryResultAsync(queryParams, null);
        }

        public virtual async Task<QueryResult<T>> GetPageAsync<TProperty>(QueryParameters queryParams, Expression<Func<T, bool>> predicate, List<Expression<Func<T, TProperty>>> includes = null)
        {
            return await GetOrderedPageQueryResultAsync(queryParams, null);
        }

        public virtual async Task<QueryResult<T>> GetOrderedPageQueryResultAsync(QueryParameters queryParams, IQueryable<T> query)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (queryParams.SortingParams != null && queryParams.SortingParams.Count > 0)
            {
                var p = queryParams.SortingParams.FirstOrDefault();
                parameters.Add(new KeyValuePair<string, string>(p.OrderProperty, p.OrderDescending?.ToString() ?? "false"));
                parameters.Add(new KeyValuePair<string, string>("pageIndex", queryParams.PageIndex.ToString()));
                parameters.Add(new KeyValuePair<string, string>("pageSize", queryParams.PageSize.ToString()));
            }
            return await Get<QueryResult<T>>(parameters.ToArray());
        }
        #endregion

        #region Private methods, External requests
        private async Task<Tsingle> Get<Tsingle>(int? id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{_baseUrl}/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Tsingle>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error try to get the data, ID {0}, error: {1}", id, ex.Message);
            }

            return default;
        }

        private async Task<Tlist> Get<Tlist>(params KeyValuePair<string, string>[] p)
        {
            var builder = new UriBuilder(_baseUrl);
            var query = HttpUtility.ParseQueryString(builder.Query);

            foreach (var item in p)
            {
                query[item.Key] = item.Value;
            }

            builder.Query = query.ToString();
            string url = builder.ToString();

            try
            {
                using var client = GetHttpClient();

                var response = await client.GetAsync(url);

                return await HandleResponse<Tlist>(response);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error try to send data, error: {0}", ex.Message);
            }

            return default;
        }

        private async Task<Tsingle> Add<Tsingle>(object data)
        {
            var payload = SerializeData(data);

            try
            {
                using var client = GetHttpClient();

                var response = await client.PostAsync(_pathUrl, payload);

                return await HandleResponse<Tsingle>(response);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error try to send data, error: {0}", ex.Message);
            }

            return default;
        }

        private async Task<Tsingle> Update<Tsingle>(object data, int id)
        {
            var payload = SerializeData(data);

            try
            {
                using var client = GetHttpClient();

                var response = await client.PutAsync($"{_pathUrl}/{id}", payload);

                return await HandleResponse<Tsingle>(response);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error try to send data, error: {0}", ex.Message);
            }

            return default;
        }

        private async Task Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.DeleteAsync($"{_baseUrl}/{id}");

                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error try delete, ID {0}, error: {1}", id, ex.Message);
            }
        }

        private HttpClient GetHttpClient(bool includeToken = false, params KeyValuePair<string, string>[] headers)
        {
            var client = new HttpClient();

            foreach (var item in headers)
            {
                client.DefaultRequestHeaders.Add(item.Key, item.Value);
            }

            client.BaseAddress = new Uri(_baseUrl);

            if (includeToken)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456789");
            }

            return client;
        }

        private async Task<tr> HandleResponse<tr>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<tr>(content);
            }

            return default;
        }

        private StringContent SerializeData(object data)
        {
            var stringContent = JsonConvert.SerializeObject(data);

            return new StringContent(stringContent, Encoding.UTF8, "application/json");
        }

        #endregion
    }
}
