# 冰箱食品管理Web系統 - 任務清單

## 階段一：專案初始化與基礎架構 (Architect Mode) - 已完成

- [x] 分析專案需求並提供框架分析建議。
- [x] 設計系統UML圖 (流程圖、循序圖、關聯圖)。
- [x] 產生 `spec.md` 規格文件。
- [x] 產生 `todolist.md` 任務清單。

## 階段二：專案建立與核心功能開發 (Code Mode)

### 2.1 專案建立與基本設定

- [x] 建立 .NET Core 8 MVC 專案 `HomeFridgeDotNet`。
- [x] 配置 `appsettings.json` 以指定 INI 檔案的儲存路徑。
- [x] 建立基礎的 MVC 結構 (Controllers, Models, Views)。

### 2.2 資料模型與 INI 檔案處理

- [x] 建立 `FoodItem.cs` 模型類別，定義食品屬性。
- [x] 實作 `IniFileManager.cs` (或 `FoodIniRepository.cs`) 類別：
    - [x] 讀取 INI 檔案並反序列化為 `List<FoodItem>`。
    - [x] 將 `FoodItem` 物件序列化並寫入 INI 檔案。
    - [x] 處理 INI 檔案的增、刪、改操作。
    - [x] 處理 INI 檔案讀寫的錯誤。

### 2.3 服務層實作

- [x] 建立 `FoodService.cs` 類別：
    - [x] 實作 `AddFood(FoodItem item)` 方法。
    - [x] 實作 `GetAllFoods()` 方法。
    - [x] 實作 `GetFoodById(string id)` 方法。
    - [x] 實作 `UpdateFood(FoodItem item)` 方法。
    - [x] 實作 `DeleteFood(string id)` 方法。
    - [x] 實作 `SearchFoods(string query)` 方法 (依名稱、到期日、儲存位置等)。

### 2.4 控制器與視圖開發

- [x] 建立 `FoodController.cs`：
    - [x] 實作 `Index` Action (GET): 顯示食品列表。
    - [x] 實作 `Create` Action (GET): 顯示新增食品表單。
    - [x] 實作 `Create` Action (POST): 處理新增食品提交，呼叫 `FoodService`。
    - [x] 實作 `Edit` Action (GET): 顯示編輯食品表單，預填現有資訊。
    - [x] 實作 `Edit` Action (POST): 處理編輯食品提交，呼叫 `FoodService`。
    - [x] 實作 `Delete` Action (POST): 處理刪除食品請求，呼叫 `FoodService`。
    - [x] 實作 `Search` Action (GET/POST): 處理食品查詢。

### 2.5 前端互動與驗證

- [x] 在視圖中加入前端驗證邏輯 (例如使用 JavaScript)。
- [x] 提升使用者體驗 (例如使用 AJAX 提交表單)。
- [x] 提升使用者體驗 (例如使用 AJAX 提交表單)。

### 2.6 新增 "已使用%" 欄位

- [x] 在編輯食品頁面新增"已使用%"欄位，可以輸入0到100的整數字，並且在查詢頁面顯示這個欄位。

## 階段三：專案完成與文件撰寫 (Orchestrator Mode)

- [x] 更新任務進度 (`todolist.md`)。
- [x] 撰寫 `readme.md` 檔案，包含專案描述、安裝及執行方式。
- [x] 產生任務完成報告 (`report.md`)。