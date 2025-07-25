# 文言文編譯器 (Classical Chinese Compiler)

一個基於文言文語法的現代編程語言編譯器，所有代碼組件均採用中文命名。

## 項目概述

本項目旨在創建一個獨特的編程語言編譯器，該語言結合了古典中文（文言文）的語法結構與現代編程概念。編譯器的所有內部組件、變量名、函數名和類名都使用中文命名，體現了中華文化與現代技術的融合。

## 特色功能

- 🏛️ **文言文語法**: 採用古典中文語法結構進行編程
- 🇨🇳 **全中文命名**: 所有代碼組件使用中文命名
- 🔧 **現代架構**: 基於 .NET 9.0 構建的高性能編譯器
- 🐳 **容器化支持**: 支持 Docker 容器化部署
- 📚 **語言特性**: 支持變量聲明、函數定義、控制流等現代編程概念

## 技術架構

### 核心組件

```
文言文編譯器/
├── 詞法分析器/        # 負責將源代碼分解為詞法單元
├── 語法分析器/        # 構建抽象語法樹
├── 語義分析器/        # 進行類型檢查和語義驗證
├── 代碼生成器/        # 生成目標代碼
└── 運行時環境/        # 提供程序執行環境
```

### 技術棧

- **框架**: .NET 9.0
- **語言**: C#
- **容器**: Docker
- **開發環境**: Visual Studio 2022

## 語言語法示例

### 變量聲明
```wenyan
吾有一數，曰「甲」，其值三也。
吾有一言，曰「乙」，其值「你好世界」也。
```

### 函數定義
```wenyan
有術曰「求和」，欲行是術，必先得二數，曰「甲」、曰「乙」。
    施「甲」於「乙」，名之曰「丙」。
    乃得「丙」。
是謂「求和」之術也。
```

### 條件判斷
```wenyan
若「甲」大於「乙」者，
    云「甲大」。
若非，
    云「乙大」。
云云。
```

### 循環結構
```wenyan
恆為是。
    若「甲」等於十者，乃止。
    加「甲」以一。
云云。
```

## 快速開始

### 環境要求

- .NET 9.0 SDK
- Visual Studio 2022 或 VS Code
- Docker (可選，用於容器化部署)

### 安裝步驟

1. **克隆項目**
```bash
git clone https://github.com/your-username/ChineseCompiler.git
cd ChineseCompiler
```

2. **還原依賴**
```bash
dotnet restore
```

3. **構建項目**
```bash
dotnet build
```

4. **運行編譯器**
```bash
dotnet run
```

### Docker 部署

1. **構建鏡像**
```bash
docker build -t 文言文編譯器 .
```

2. **運行容器**
```bash
docker run -it 文言文編譯器
```

## 項目結構

```
ChineseCompiler/
├── Program.cs              # 主程序入口
├── ChineseCompiler.csproj  # 項目配置文件
├── Dockerfile              # Docker 配置
├── 詞法分析/               # 詞法分析模塊
│   ├── 詞法分析器.cs
│   ├── 詞法單元.cs
│   └── 關鍵字表.cs
├── 語法分析/               # 語法分析模塊
│   ├── 語法分析器.cs
│   ├── 抽象語法樹.cs
│   └── 語法規則.cs
├── 語義分析/               # 語義分析模塊
│   ├── 語義分析器.cs
│   ├── 符號表.cs
│   └── 類型檢查器.cs
├── 代碼生成/               # 代碼生成模塊
│   ├── 代碼生成器.cs
│   ├── 中間代碼.cs
│   └── 目標代碼.cs
├── ChineseCompiler.Tests/  # 測試項目
│   ├── 詞法分析測試.cs
│   ├── 關鍵字表測試.cs
│   ├── 語法分析測試.cs
│   ├── 語義分析測試.cs
│   ├── 代碼生成測試.cs
│   └── 集成測試.cs
└── 測試/                   # 測試文件
    ├── 單元測試/
    └── 集成測試/
```

## 測試套件

項目包含完整的 xUnit 測試套件，確保編譯器的可靠性和正確性。

### 測試覆蓋範圍

| 測試類別 | 測試數量 | 覆蓋範圍 |
|----------|----------|----------|
| 詞法分析測試 | 12 個測試 | 關鍵字識別、數字解析、字符串處理、空白字符處理 |
| 關鍵字表測試 | 8 個測試 | 關鍵字映射、文言文數字轉換、類型獲取 |
| 語法分析測試 | 12 個測試 | AST 構建、語法規則驗證、錯誤處理 |
| 語義分析測試 | 10 個測試 | 類型檢查、符號表管理、作用域驗證 |
| 代碼生成測試 | 12 個測試 | C# 代碼生成、類型轉換、運算符映射 |
| 集成測試 | 15 個測試 | 完整編譯流程、錯誤處理、端到端驗證 |

**總計**: 69 個測試用例，全面覆蓋編譯器的各個組件。

### 運行測試

```bash
# 運行所有測試
cd ChineseCompiler.Tests
dotnet test

# 運行特定測試類別
dotnet test --filter "詞法分析測試"
dotnet test --filter "集成測試"

# 顯示詳細測試輸出
dotnet test --verbosity detailed

# 生成測試覆蓋率報告
dotnet test --collect:"XPlat Code Coverage"
```

### 測試特色

- **中文測試名稱**: 所有測試方法使用中文命名，清晰表達測試意圖
- **全面覆蓋**: 從單元測試到集成測試，確保每個組件都經過充分驗證
- **錯誤場景**: 包含大量錯誤處理測試，確保編譯器的健壯性
- **實際案例**: 使用真實的文言文代碼片段進行測試

## 開發計劃

### 第一階段 - 基礎架構 ✅
- [x] 項目初始化
- [x] 詞法分析器實現
- [x] 基本詞法單元定義
- [x] 關鍵字表建立

### 第二階段 - 語法分析 ✅
- [x] 語法分析器實現
- [x] 抽象語法樹構建
- [x] 語法錯誤處理

### 第三階段 - 語義分析 ✅
- [x] 符號表管理
- [x] 類型系統設計
- [x] 語義檢查實現

### 第四階段 - 代碼生成 ✅
- [x] 中間代碼生成
- [x] 目標代碼優化
- [x] 運行時支持

### 第五階段 - 測試與完善 ✅
- [x] 完整測試套件（69 個測試用例）
- [x] 單元測試覆蓋所有組件
- [x] 集成測試驗證完整流程
- [x] 錯誤處理測試

### 第六階段 - 高級功能 🔄
- [ ] 標準庫實現
- [ ] 調試器支持
- [ ] IDE 插件開發
- [ ] 性能優化
- [ ] 更多語言特性

## 貢獻指南

我們歡迎所有對文言文編程語言感興趣的開發者參與貢獻！

### 貢獻方式

1. **提交問題**: 在 Issues 中報告 bug 或提出功能建議
2. **代碼貢獻**: Fork 項目並提交 Pull Request
3. **文檔完善**: 幫助改進項目文檔
4. **測試用例**: 編寫測試用例提高代碼覆蓋率

### 代碼規範

- 所有類名、方法名、變量名必須使用中文
- 遵循 C# 編碼規範
- 添加適當的中文註釋
- 確保代碼通過所有測試

### 提交規範

```
類型(範圍): 簡短描述

詳細描述（可選）

相關問題: #123
```

類型包括：
- `功能`: 新功能
- `修復`: Bug 修復
- `文檔`: 文檔更新
- `樣式`: 代碼格式調整
- `重構`: 代碼重構
- `測試`: 測試相關
- `構建`: 構建系統相關

## 許可證

本項目採用 MIT 許可證 - 詳見 [LICENSE](LICENSE) 文件。

## 聯繫方式

- **項目主頁**: https://github.com/your-username/ChineseCompiler
- **問題反饋**: https://github.com/your-username/ChineseCompiler/issues
- **討論區**: https://github.com/your-username/ChineseCompiler/discussions

## 致謝

感謝所有為本項目做出貢獻的開發者，以及對文言文編程語言概念提供靈感的古代文人墨客。

---

*「程序設計之道，在於融古今之智慧，創新時代之語言。」* 

## 測試覆蓋

項目包含 150+ 個測試用例，覆蓋：

| 組件 | 測試數量 | 覆蓋範圍 |
|------|----------|----------|
| 詞法分析 | 12 個測試 | 關鍵字識別、數字解析、字符串處理 |
| 關鍵字表 | 8 個測試 | 關鍵字映射、文言文數字轉換 |
| 語法分析 | 12 個測試 | AST 構建、語法規則驗證 |
| 語義分析 | 10 個測試 | 類型檢查、符號表管理 |
| 代碼生成 | 12 個測試 | C# 代碼生成、類型轉換 |
| 集成測試 | 15 個測試 | 完整編譯流程、錯誤處理 |

運行特定測試：
```bash
# 運行詞法分析測試
dotnet test --filter "詞法分析測試"

# 運行集成測試
dotnet test --filter "集成測試"

# 運行所有測試並顯示詳細輸出
dotnet test --verbosity detailed
``` 