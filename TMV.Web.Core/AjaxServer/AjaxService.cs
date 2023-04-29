using Furion.RemoteRequest.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TMV.Web.Core.AjaxServer
{
    // <summary>
    /// Ajax服务类
    /// </summary>
    public class AjaxService
    {
        [Inject]
        public NavigationManager Navigation { get; set; } = default!;
        /// <summary>
        /// 获得 回调委托缓存集合
        /// </summary>
        private List<(IComponent Key, Func<AjaxOption, Task<string>> Callback)> Cache { get; } = new();

        /// <summary>
        /// 获得 跳转其他页面的回调委托缓存集合
        /// </summary>
        private List<(IComponent Key, Func<string, Task> Callback)> GotoCache { get; } = new();

        /// <summary>
        /// 调用Ajax方法发送请求
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetMessageAsync(AjaxOption option)
        {
            var ulr = option.Url;
            switch (option.Method)
            {
                case "GET":
                    return await ulr.SetBody(option.Data).GetAsync();
                case "POST":
                    return await ulr.SetBody(option.Data).PostAsync();
                default:
                    return await ulr.SetBody(option.Data).PostAsync();
            }
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        internal void Register(IComponent key, Func<AjaxOption, Task<string>> callback) => Cache.Add((key, callback));

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        internal void RegisterGoto(IComponent key, Func<string, Task> callback) => GotoCache.Add((key, callback));

        /// <summary>
        /// 注销事件
        /// </summary>
        internal void UnRegister(IComponent key)
        {
            var item = Cache.FirstOrDefault(i => i.Key == key);
            if (item.Key != null)
            {
                Cache.Remove(item);
            }
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        internal void UnRegisterGoto(IComponent key)
        {
            var item = GotoCache.FirstOrDefault(i => i.Key == key);
            if (item.Key != null)
            {
                GotoCache.Remove(item);
            }
        }
    }
}
