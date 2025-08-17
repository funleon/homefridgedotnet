using System;
using System.ComponentModel.DataAnnotations;

namespace HomeFridgeDotNet.Models
{
    /// <summary>
    /// 代表冰箱中的一個食品項目。
    /// </summary>
    public class FoodItem
    {
        /// <summary>
        /// 取得或設定食品的唯一識別碼。
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 取得或設定食品的名稱。
        /// </summary>
        [Required(ErrorMessage = "食品名稱為必填。")]
        [StringLength(100, ErrorMessage = "食品名稱長度不能超過100個字元。")]
        [Display(Name = "產品名稱")]
        public required string Name { get; set; }

        /// <summary>
        /// 取得或設定食品的數量。
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "數量必須大於0。")]
        [Display(Name = "數量")]
        public int Quantity { get; set; }

        /// <summary>
        /// 取得或設定食品的到期日。
        /// </summary>
        [Required(ErrorMessage = "到期日為必填。")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "過期日")]
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// 取得或設定食品的儲存位置 (例如: 冰箱, 冷凍庫)。
        /// </summary>
        [Required(ErrorMessage = "儲存位置為必填。")]
        [StringLength(50, ErrorMessage = "儲存位置長度不能超過50個字元。")]
        [Display(Name = "存放位置")]
        public required string StorageLocation { get; set; }

        /// <summary>
        /// 取得或設定食品的備註 (可選)。
        /// </summary>
        [StringLength(200, ErrorMessage = "備註長度不能超過200個字元。")]
        [Display(Name = "備註")]
        public string? Notes { get; set; }

        /// <summary>
        /// 取得或設定食品的使用百分比。
        /// </summary>
        [Range(0, 100, ErrorMessage = "使用百分比必須介於 0 到 100 之間。")]
        [Display(Name = "已使用%")]
        public int UsedPercentage { get; set; }
    }
}