using System;
using System.IO;
using Xunit;
using 詞法分析;
using 語法分析;
using 語義分析;
using 代碼生成;

namespace ChineseCompiler.Tests
{
    /// <summary>
    /// 集成測試類 - 測試完整的編譯流程
    /// </summary>
    public class 集成測試
    {
        [Fact]
        public void 測試完整編譯流程_簡單變量聲明()
        {
            // Arrange
            string 源代碼 = "吾有一數曰「甲」其值三也";
            
            // Act
            var 編譯結果 = 執行完整編譯(源代碼);
            
            // Assert
            Assert.Contains("double", 編譯結果);
            Assert.Contains("= 3", 編譯結果);
            Assert.Contains("namespace 文言文程序", 編譯結果);
        }
        
        [Fact]
        public void 測試完整編譯流程_變量聲明和輸出()
        {
            // Arrange
            string 源代碼 = @"吾有一數曰「甲」其值三也
云「甲」";
            
            // Act
            var 編譯結果 = 執行完整編譯(源代碼);
            
            // Assert
            Assert.Contains("double", 編譯結果);
            Assert.Contains("Console.WriteLine", 編譯結果);
            Assert.Contains("\"甲\"", 編譯結果);
        }
        
        [Fact]
        public void 測試完整編譯流程_條件語句()
        {
            // Arrange
            string 源代碼 = @"若三大於二者
    云「真」
若非
    云「假」
云云";
            
            // Act
            var 編譯結果 = 執行完整編譯(源代碼);
            
            // Assert
            Assert.Contains("if (", 編譯結果);
            Assert.Contains("3 > 2", 編譯結果);
            Assert.Contains("else", 編譯結果);
            Assert.Contains("\"真\"", 編譯結果);
            Assert.Contains("\"假\"", 編譯結果);
        }
        
        [Fact]
        public void 測試完整編譯流程_算術運算()
        {
            // Arrange
            string 源代碼 = "云三加五";
            
            // Act
            var 編譯結果 = 執行完整編譯(源代碼);
            
            // Assert
            Assert.Contains("Console.WriteLine", 編譯結果);
            Assert.Contains("3 + 5", 編譯結果);
        }
        
        [Fact]
        public void 測試完整編譯流程_多種數據類型()
        {
            // Arrange
            string 源代碼 = @"吾有一數曰「甲」其值四十二也
吾有一言曰「乙」其值「你好世界」也
云「乙」
云「甲」";
            
            // Act
            var 編譯結果 = 執行完整編譯(源代碼);
            
            // Assert
            Assert.Contains("double", 編譯結果);
            Assert.Contains("string", 編譯結果);
            Assert.Contains("= 42", 編譯結果);
            Assert.Contains("\"你好世界\"", 編譯結果);
        }
        
        [Fact]
        public void 測試完整編譯流程_文言文數字()
        {
            // Arrange
            string 源代碼 = @"吾有一數曰「甲」其值五也
吾有一數曰「乙」其值十也
云「甲」加「乙」";
            
            // Act
            var 編譯結果 = 執行完整編譯(源代碼);
            
            // Assert
            Assert.Contains("= 5", 編譯結果);
            Assert.Contains("= 10", 編譯結果);
            Assert.Contains("+", 編譯結果);
        }
        
        [Fact]
        public void 測試完整編譯流程_複雜表達式()
        {
            // Arrange
            string 源代碼 = "云三加五乘二";
            
            // Act
            var 編譯結果 = 執行完整編譯(源代碼);
            
            // Assert
            Assert.Contains("Console.WriteLine", 編譯結果);
            // 應該包含正確的運算符優先級處理
            Assert.Contains("+", 編譯結果);
            Assert.Contains("*", 編譯結果);
        }
        
        [Fact]
        public void 測試完整編譯流程_循環語句()
        {
            // Arrange
            string 源代碼 = @"恆為是
    云「循環中」
云云";
            
            // Act
            var 編譯結果 = 執行完整編譯(源代碼);
            
            // Assert
            Assert.Contains("while (true)", 編譯結果);
            Assert.Contains("\"循環中\"", 編譯結果);
        }
        
        [Fact]
        public void 測試語法錯誤處理()
        {
            // Arrange
            string 錯誤源代碼 = "吾有數字"; // 缺少類型和其他必要部分
            
            // Act & Assert
            Assert.Throws<Exception>(() => 執行完整編譯(錯誤源代碼));
        }
        
        [Fact]
        public void 測試語義錯誤處理_未聲明變量()
        {
            // Arrange
            string 錯誤源代碼 = "云未聲明變量"; // 使用未聲明的變量（不是字符串）
            
            // Act & Assert
            Assert.Throws<Exception>(() => 執行完整編譯(錯誤源代碼));
        }
        
        [Fact]
        public void 測試語義錯誤處理_重複聲明()
        {
            // Arrange
            string 錯誤源代碼 = @"吾有一數曰「甲」也
吾有一言曰「甲」也"; // 重複聲明變量
            
            // Act & Assert
            Assert.Throws<Exception>(() => 執行完整編譯(錯誤源代碼));
        }
        
        [Fact]
        public void 測試空程序編譯()
        {
            // Arrange
            string 空源代碼 = "";
            
            // Act
            var 編譯結果 = 執行完整編譯(空源代碼);
            
            // Assert
            Assert.Contains("namespace 文言文程序", 編譯結果);
            Assert.Contains("public static void Main", 編譯結果);
        }
        
        [Fact]
        public void 測試註釋處理()
        {
            // Arrange
            string 源代碼 = @"// 這是註釋
吾有一數曰「甲」其值三也
// 另一個註釋
云「甲」";
            
            // Act
            var 編譯結果 = 執行完整編譯(源代碼);
            
            // Assert
            Assert.Contains("double", 編譯結果);
            Assert.Contains("Console.WriteLine", 編譯結果);
            Assert.DoesNotContain("//", 編譯結果); // 註釋不應出現在生成的代碼中
        }
        
        [Fact]
        public void 測試空白字符處理()
        {
            // Arrange
            string 源代碼 = @"吾有    一數
    曰「甲」
        其值三也
云「甲」";
            
            // Act
            var 編譯結果 = 執行完整編譯(源代碼);
            
            // Assert
            Assert.Contains("double", 編譯結果);
            Assert.Contains("= 3", 編譯結果);
            Assert.Contains("Console.WriteLine", 編譯結果);
        }
        
        [Fact]
        public void 測試生成代碼的語法正確性()
        {
            // Arrange
            string 源代碼 = @"吾有一數曰「甲」其值三也
吾有一言曰「乙」其值「測試」也
若「甲」大於二者
    云「乙」
云云";
            
            // Act
            var 編譯結果 = 執行完整編譯(源代碼);
            
            // Assert
            // 檢查生成的C#代碼語法結構
            Assert.Contains("using System;", 編譯結果);
            Assert.Contains("namespace 文言文程序", 編譯結果);
            Assert.Contains("public class 主類", 編譯結果);
            Assert.Contains("public static void Main(string[] args)", 編譯結果);
            
            // 檢查大括號匹配
            int 左大括號 = 0, 右大括號 = 0;
            foreach (char c in 編譯結果)
            {
                if (c == '{') 左大括號++;
                if (c == '}') 右大括號++;
            }
            Assert.Equal(左大括號, 右大括號);
            
            // 檢查分號
            Assert.Contains(";", 編譯結果);
        }
        
        /// <summary>
        /// 執行完整的編譯流程
        /// </summary>
        /// <param name="源代碼">文言文源代碼</param>
        /// <returns>生成的C#代碼</returns>
        private string 執行完整編譯(string 源代碼)
        {
            // 詞法分析
            var 詞法分析器 = new 詞法分析器();
            var 詞法單元列表 = 詞法分析器.分析(源代碼);
            
            // 語法分析
            var 語法分析器 = new 語法分析器();
            var 抽象語法樹 = 語法分析器.分析(詞法單元列表);
            
            // 語義分析
            var 語義分析器 = new 語義分析器();
            語義分析器.分析(抽象語法樹);
            
            // 代碼生成
            var 代碼生成器 = new 代碼生成器();
            return 代碼生成器.生成(抽象語法樹);
        }
    }
} 