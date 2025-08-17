using HomeFridgeDotNet.Models;
using HomeFridgeDotNet.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace HomeFridgeDotNet.Controllers
{
    /// <summary>
    /// 處理冰箱食品相關的 Web 請求。
    /// </summary>
    public class FoodController : Controller
    {
        private readonly FoodService _foodService;

        /// <summary>
        /// 初始化 FoodController 類別的新實例。
        /// </summary>
        /// <param name="foodService">食品服務實例。</param>
        public FoodController(FoodService foodService)
        {
            _foodService = foodService;
        }

        /// <summary>
        /// 顯示所有食品的列表。
        /// </summary>
        /// <param name="searchQuery">查詢字串，用於篩選食品。</param>
        /// <returns>食品列表視圖。</returns>
        public IActionResult Index(string searchQuery)
        {
            var foods = _foodService.SearchFoods(searchQuery);
            ViewBag.SearchQuery = searchQuery; // 將查詢字串傳遞給視圖
            return View(foods);
        }

        /// <summary>
        /// 顯示新增食品的表單。
        /// </summary>
        /// <returns>新增食品視圖。</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 處理新增食品的表單提交。
        /// </summary>
        /// <param name="foodItem">要新增的食品項目。</param>
        /// <returns>重新導向到食品列表或返回新增表單。</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                _foodService.AddFood(foodItem);
                return RedirectToAction(nameof(Index));
            }
            return View(foodItem);
        }

        /// <summary>
        /// 顯示編輯食品的表單。
        /// </summary>
        /// <param name="id">要編輯食品的唯一識別碼。</param>
        /// <returns>編輯食品視圖或找不到頁面。</returns>
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = _foodService.GetFoodById(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            return View(foodItem);
        }

        /// <summary>
        /// 處理編輯食品的表單提交。
        /// </summary>
        /// <param name="id">要編輯食品的唯一識別碼。</param>
        /// <param name="foodItem">更新後的食品項目。</param>
        /// <returns>重新導向到食品列表或返回編輯表單。</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, FoodItem foodItem)
        {
            if (id != foodItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _foodService.UpdateFood(foodItem);
                }
                catch (Exception)
                {
                    // 可以在這裡記錄錯誤
                    if (_foodService.GetFoodById(foodItem.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(foodItem);
        }

        /// <summary>
        /// 處理刪除食品的請求。
        /// </summary>
        /// <param name="id">要刪除食品的唯一識別碼。</param>
        /// <returns>重新導向到食品列表。</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _foodService.DeleteFood(id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 顯示錯誤頁面。
        /// </summary>
        /// <returns>錯誤視圖。</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}