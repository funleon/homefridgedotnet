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
        public required string Name { get; set; }

        /// <summary>
        /// 取得或設定食品的數量。
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "數量必須大於0。")]
        public int Quantity { get; set; }

        /// <summary>
        /// 取得或設定食品的到期日。
        /// </summary>
        [Required(ErrorMessage = "到期日為必填。")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// 取得或設定食品的儲存位置 (例如: 冰箱, 冷凍庫)。
        /// </summary>
        [Required(ErrorMessage = "儲存位置為必填。")]
        [StringLength(50, ErrorMessage = "儲存位置長度不能超過50個字元。")]
        public required string StorageLocation { get; set; }

        /// <summary>
        /// 取得或設定食品的備註 (可選)。
        /// </summary>
        [StringLength(200, ErrorMessage = "備註長度不能超過200個字元。")]
        public required string Notes { get; set; }
    }
}