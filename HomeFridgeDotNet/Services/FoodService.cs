using HomeFridgeDotNet.Data;
using HomeFridgeDotNet.Models;
using System.Collections.Generic;
using System.Linq;

namespace HomeFridgeDotNet.Services
{
    /// <summary>
    /// 提供冰箱食品相關的業務邏輯服務。
    /// </summary>
    public class FoodService
    {
        private readonly IniFileManager _iniFileManager;

        /// <summary>
        /// 初始化 FoodService 類別的新實例。
        /// </summary>
        /// <param name="iniFileManager">INI 檔案管理器實例。</param>
        public FoodService(IniFileManager iniFileManager)
        {
            _iniFileManager = iniFileManager;
        }

        /// <summary>
        /// 新增一個食品項目。
        /// </summary>
        /// <param name="food">要新增的食品項目。</param>
        public void AddFood(FoodItem food)
        {
            _iniFileManager.SaveFood(food);
        }

        /// <summary>
        /// 取得所有食品項目。
        /// </summary>
        /// <returns>所有食品項目的列表。</returns>
        public List<FoodItem> GetAllFoods()
        {
            return _iniFileManager.GetAllFoods();
        }

        /// <summary>
        /// 根據 ID 取得單個食品項目。
        /// </summary>
        /// <param name="id">食品的唯一識別碼。</param>
        /// <returns>匹配的食品項目，如果找不到則為 null。</returns>
        /// <summary>
        /// 根據 ID 取得單個食品項目。
        /// </summary>
        /// <param name="id">食品的唯一識別碼。</param>
        /// <returns>匹配的食品項目，如果找不到則為 null。</returns>
        public FoodItem? GetFoodById(string id)
        {
            return _iniFileManager.GetAllFoods().FirstOrDefault(f => f.Id == id); // FirstOrDefault 可能返回 null
        }

        /// <summary>
        /// 更新一個食品項目。
        /// </summary>
        /// <param name="food">要更新的食品項目。</param>
        public void UpdateFood(FoodItem food)
        {
            _iniFileManager.SaveFood(food);
        }

        /// <summary>
        /// 刪除一個食品項目。
        /// </summary>
        /// <param name="id">要刪除食品的唯一識別碼。</param>
        public void DeleteFood(string id)
        {
            _iniFileManager.DeleteFood(id);
        }

        /// <summary>
        /// 根據查詢條件篩選食品項目。
        /// </summary>
        /// <param name="query">查詢字串，可匹配名稱、儲存位置或到期日。</param>
        /// <returns>匹配查詢條件的食品項目列表。</returns>
        public List<FoodItem> SearchFoods(string query)
        {
            var foods = GetAllFoods();

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.ToLower();
                foods = foods.Where(f =>
                    f.Name.ToLower().Contains(query) ||
                    f.StorageLocation.ToLower().Contains(query) ||
                    f.ExpiryDate.ToString("yyyy-MM-dd").Contains(query)
                ).ToList();
            }

            // 預設用過期日排序，時間由近至遠，已過期食品在最上方，接近過期的食品次之，其他依過期日由近至遠排序
            var today = DateTime.Today;
            foods = foods.OrderBy(f => f.ExpiryDate >= today ? (f.ExpiryDate - today).TotalDays : -1 * (today - f.ExpiryDate).TotalDays).ToList();

            return foods;
        }

        public List<FoodItem> SearchFoods(string query, string sortField, string sortOrder)
        {
            var foods = SearchFoods(query);

            if (!string.IsNullOrEmpty(sortField))
            {
                foods = SortFoods(foods, sortField, sortOrder);
            }

            return foods;
        }

        private List<FoodItem> SortFoods(List<FoodItem> foods, string sortField, string sortOrder)
{
    if (string.IsNullOrEmpty(sortField))
    {
        return foods;
    }

    Func<FoodItem, object> keySelector = null;

    switch (sortField.ToLower())
    {
        case "name":
            keySelector = f => f.Name;
            break;
        case "quantity":
            keySelector = f => f.Quantity;
            break;
        case "expirydate":
            keySelector = f => f.ExpiryDate;
            break;
        case "storagelocation":
            keySelector = f => f.StorageLocation;
            break;
        case "usedpercentage":
            keySelector = f => f.UsedPercentage;
            break;
        default:
            return foods;
    }

    if (sortOrder.ToLower() == "desc")
    {
        return foods.OrderByDescending(keySelector).ToList();
    }
    else
    {
        return foods.OrderBy(keySelector).ToList();
    }
}

    }
}