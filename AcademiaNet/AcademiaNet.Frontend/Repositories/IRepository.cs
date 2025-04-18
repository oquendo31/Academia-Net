namespace AcademiaNet.Frontend.Repositories;

public interface IRepository
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<HttpResponseWrapper<T>> GetAsync<T>(string url);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TActionResponse"></typeparam>
    /// <param name="url"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model);

    /// <summary>
    ///
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<HttpResponseWrapper<object>> DeleteAsync(string url);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TActionResponse"></typeparam>
    /// <param name="url"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<HttpResponseWrapper<TActionResponse>> PutAsync<T, TActionResponse>(string url, T model);

    /// <summary>
    ///
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<HttpResponseWrapper<object>> GetAsync(string url);
}