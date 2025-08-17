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
            if (string.IsNullOrWhiteSpace(query))
            {
                return GetAllFoods();
            }

            query = query.ToLower();
            return GetAllFoods().Where(f =>
                f.Name.ToLower().Contains(query) ||
                f.StorageLocation.ToLower().Contains(query) ||
                f.ExpiryDate.ToString("yyyy-MM-dd").Contains(query)
            ).ToList();
        }
    }
}