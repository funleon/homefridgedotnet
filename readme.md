# 冰箱食品管理Web系統

## 專案描述

這是一個基於 .NET Core 8 MVC 開發的Web應用程式，旨在幫助使用者有效管理冰箱中的食品。系統允許使用者追蹤食品的名稱、數量、到期日和儲存位置。資料儲存採用簡單的 INI 文字檔案，方便輕量級部署和管理。

## 功能特色

*   **食品列表:** 顯示所有冰箱中的食品資訊。
*   **新增食品:** 允許使用者輸入食品名稱、數量、到期日、儲存位置等資訊，並新增至系統。
*   **編輯食品:** 允許使用者修改現有食品的資訊。
*   **刪除食品:** 允許使用者從系統中移除食品。
*   **查詢食品:** 允許使用者根據名稱、到期日或儲存位置等條件查詢食品。

## 技術棧

*   **後端框架:** .NET Core 8 MVC
*   **前端技術:** HTML5, CSS3, JavaScript (Razor Views), Bootstrap
*   **資料儲存:** INI 文字檔案

## 安裝與執行方式

### 前提條件

*   安裝 .NET 8 SDK: [https://dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0)

### 步驟

1.  **複製專案:**
    ```bash
    git clone [專案的 Git 儲存庫 URL]
    cd HomeFridgeDotNet
    ```
    (如果專案已經在本地，請跳過此步驟)

2.  **進入專案目錄:**
    ```bash
    cd HomeFridgeDotNet
    ```

3.  **還原 NuGet 套件:**
    ```bash
    dotnet restore
    ```

4.  **執行應用程式:**
    ```bash
    dotnet run
    ```

5.  **開啟瀏覽器:**
    應用程式啟動後，您將在終端機中看到類似以下的輸出，其中包含應用程式的本機 URL (通常是 `https://localhost:7xxx` 或 `http://localhost:5xxx`)：
    ```
    info: Microsoft.Hosting.Lifetime[14]
          Now listening on: https://localhost:7xxx
    info: Microsoft.Hosting.Lifetime[14]
          Now listening on: http://localhost:5xxx
    ```
    在您的瀏覽器中開啟這些 URL 之一，即可存取冰箱食品管理系統。

## INI 檔案說明

*   INI 檔案的路徑配置在 `appsettings.json` 中的 `IniFilePath` 鍵。預設路徑為 `Data/foods.ini`。
*   每個食品項目在 INI 檔案中以一個獨立的 Section 表示，Section 名稱是食品的唯一識別碼 (GUID)。
*   INI 檔案範例 (`Data/foods.ini`):
    ```ini
    [GUID_OF_FOOD_ITEM_1]
    Name=牛奶
    Quantity=1
    ExpiryDate=2025-06-01
    StorageLocation=冰箱
    Notes=低脂

    [GUID_OF_FOOD_ITEM_2]
    Name=雞蛋
    Quantity=12
    ExpiryDate=2025-06-15
    StorageLocation=冰箱
    Notes=土雞蛋