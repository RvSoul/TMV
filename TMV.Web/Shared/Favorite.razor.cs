namespace TMV.Web.Shared
{
    public partial class Favorite
    {
        List<int> _favoriteMenus = FavoriteService.GetDefaultFavoriteMenuList();

        protected override void OnInitialized()
        {
            if (GlobalConfig.Favorite == "")
            {
                _favoriteMenus.Clear();
            }
            else if (GlobalConfig.Favorite is not null)
            {
                _favoriteMenus = GlobalConfig.Favorite.Split('|').Select(v => Convert.ToInt32(v)).ToList();
            }
        }

        bool _open;
        string? _search;

        void OnOpen(bool open)
        {
            _open = open;
            if (open is true)
            {
                _search = null;
            }
        }

        List<NavModel> GetNavs(string? search)
        {
            var output = new List<NavModel>();

            if (search is null || search == "") output.AddRange(NavHelper.SameLevelNavs.Where(n => _favoriteMenus.Contains(n.Id)));
            else
            {
                output.AddRange(NavHelper.SameLevelNavs.Where(n => n.Href is not null));
            }

            return output;
        }

        List<NavModel> GetFavoriteMenus() => GetNavs(null);

        void AddOrRemoveFavoriteMenu(int id)
        {
            if (_favoriteMenus.Contains(id)) _favoriteMenus.Remove(id);
            else _favoriteMenus.Add(id);
            GlobalConfig.Favorite = string.Join("|", _favoriteMenus);
        }
    }
}
