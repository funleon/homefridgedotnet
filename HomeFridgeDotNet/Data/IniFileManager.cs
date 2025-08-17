using HomeFridgeDotNet.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HomeFridgeDotNet.Data
{
    /// <summary>
    /// 負責 INI 檔案的讀寫操作，用於儲存和管理食品項目。
    /// </summary>
    public class IniFileManager
    {
        private readonly string _iniFilePath;

        /// <summary>
        /// 初始化 IniFileManager 類別的新實例。
        /// </summary>
        /// <param name="configuration">應用程式的配置介面，用於取得 INI 檔案路徑。</param>
        public IniFileManager(IConfiguration configuration)
        {
            _iniFilePath = configuration["IniFilePath"] ?? "Data/foods.ini";
            // 確保 INI 檔案所在的目錄存在
            var directory = Path.GetDirectoryName(_iniFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // 如果檔案不存在，則建立一個空檔案
            if (!File.Exists(_iniFilePath))
            {
                File.WriteAllText(_iniFilePath, string.Empty);
            }
        }

        /// <summary>
        /// 從 INI 檔案中讀取所有食品項目。
        /// </summary>
        /// <returns>包含所有食品項目的列表。</returns>
        public List<FoodItem> GetAllFoods()
        {
            var foods = new List<FoodItem>();
            var lines = File.ReadAllLines(_iniFilePath);
            /// <summary>
            /// 當前正在解析的食品項目。
            /// </summary>
            FoodItem? currentFood = null;

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    // 新的 Section (食品項目)
                    if (currentFood != null)
                    {
                        foods.Add(currentFood);
                    }
                    // 由於 FoodItem 的屬性現在是 required，我們需要確保它們在建構時被賦值。
                    // 在這裡，我們將它們初始化為空字串或預設值，並在後續解析 INI 檔案時更新。
                    currentFood = new FoodItem
                    {
                        Name = string.Empty,
                        StorageLocation = string.Empty,
                        Notes = string.Empty,
                        ExpiryDate = DateTime.MinValue, // 或其他合適的預設日期
                        Quantity = 0 // 或其他合適的預設數量
                    };
                    currentFood.Id = trimmedLine.Substring(1, trimmedLine.Length - 2); // 取得 Section 名稱作為 ID
                }
                else if (currentFood != null)
                {
                    // Key-Value 對
                    var parts = trimmedLine.Split('=', 2);
                    if (parts.Length == 2)
                    {
                        var key = parts[0].Trim();
                        var value = parts[1].Trim();

                        switch (key)
                        {
                            case "Name":
                                currentFood.Name = value;
                                break;
                            case "Quantity":
                                if (int.TryParse(value, out int quantity))
                                {
                                    currentFood.Quantity = quantity;
                                }
                                break;
                            case "ExpiryDate":
                                if (DateTime.TryParse(value, out DateTime expiryDate))
                                {
                                    currentFood.ExpiryDate = expiryDate;
                                }
                                break;
                            case "StorageLocation":
                                currentFood.StorageLocation = value;
                                break;
                            case "Notes":
                                currentFood.Notes = value;
                                break;
                        }
                    }
                }
            }

            if (currentFood != null)
            {
                foods.Add(currentFood);
            }

            return foods;
        }

        /// <summary>
        /// 將單個食品項目儲存到 INI 檔案中。
        /// 如果食品項目已存在 (根據 ID)，則更新；否則新增。
        /// </summary>
        /// <param name="food">要儲存的食品項目。</param>
        public void SaveFood(FoodItem food)
        {
            var allFoods = GetAllFoods();
            var existingFood = allFoods.FirstOrDefault(f => f.Id == food.Id);

            if (existingFood != null)
            {
                // 更新現有食品
                allFoods.Remove(existingFood);
                allFoods.Add(food);
            }
            else
            {
                // 新增食品
                allFoods.Add(food);
            }

            WriteAllFoods(allFoods);
        }

        /// <summary>
        /// 從 INI 檔案中刪除指定的食品項目。
        /// </summary>
        /// <param name="id">要刪除食品的唯一識別碼。</param>
        public void DeleteFood(string id)
        {
            var allFoods = GetAllFoods();
            var foodToRemove = allFoods.FirstOrDefault(f => f.Id == id);
            if (foodToRemove != null)
            {
                allFoods.Remove(foodToRemove);
                WriteAllFoods(allFoods);
            }
        }

        /// <summary>
        /// 將所有食品項目寫入 INI 檔案。
        /// </summary>
        /// <param name="foods">要寫入的食品項目列表。</param>
        private void WriteAllFoods(List<FoodItem> foods)
        {
            var sb = new StringBuilder();
            foreach (var food in foods)
            {
                sb.AppendLine($"[{food.Id}]");
                sb.AppendLine($"Name={food.Name}");
                sb.AppendLine($"Quantity={food.Quantity}");
                sb.AppendLine($"ExpiryDate={food.ExpiryDate:yyyy-MM-dd}");
                sb.AppendLine($"StorageLocation={food.StorageLocation}");
                if (!string.IsNullOrEmpty(food.Notes))
                {
                    sb.AppendLine($"Notes={food.Notes}");
                }
                sb.AppendLine(); // 空行分隔不同食品項目
            }
            File.WriteAllText(_iniFilePath, sb.ToString());
        }
    }
}