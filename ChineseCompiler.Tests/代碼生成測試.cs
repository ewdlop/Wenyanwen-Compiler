using System;
using Xunit;
using 語法分析;
using 代碼生成;

namespace ChineseCompiler.Tests
{
    /// <summary>
    /// 代碼生成器測試類
    /// </summary>
    public class 代碼生成測試
    {
        [Fact]
        public void 測試變量聲明代碼生成()
        {
            // Arrange
            var 生成器 = new 代碼生成器();
            var 程序 = new 程序節點();
            var 變量聲明 = new 變量聲明節點(1, 1)
            {
                變量類型 = "一數",
                變量名稱 = "甲",
                初始值 = new 字面量表達式節點(1, 5)
                {
                    類型 = 詞法分析.詞法單元類型.數字,
                    值 = 3.0
                }
            };
            程序.語句列表.Add(變量聲明);
            
            // Act
            var 代碼 = 生成器.生成(程序);
            
            // Assert
            Assert.Contains("using System;", 代碼);
            Assert.Contains("namespace 文言文程序", 代碼);
            Assert.Contains("public class 主類", 代碼);
            Assert.Contains("public static void Main(string[] args)", 代碼);
            Assert.Contains("double", 代碼);
            Assert.Contains("= 3", 代碼);
        }
        
        [Fact]
        public void 測試無初始值變量聲明()
        {
            // Arrange
            var 生成器 = new 代碼生成器();
            var 程序 = new 程序節點();
            var 變量聲明 = new 變量聲明節點(1, 1)
            {
                變量類型 = "一言",
                變量名稱 = "乙"
            };
            程序.語句列表.Add(變量聲明);
            
            // Act
            var 代碼 = 生成器.生成(程序);
            
            // Assert
            Assert.Contains("string", 代碼);
            Assert.Contains("= \"\"", 代碼);
        }
        
        [Fact]
        public void 測試輸出語句代碼生成()
        {
            // Arrange
            var 生成器 = new 代碼生成器();
            var 程序 = new 程序節點();
            var 輸出語句 = new 輸出語句節點(1, 1)
            {
                輸出表達式 = new 字面量表達式節點(1, 3)
                {
                    類型 = 詞法分析.詞法單元類型.字符串,
                    值 = "你好世界"
                }
            };
            程序.語句列表.Add(輸出語句);
            
            // Act
            var 代碼 = 生成器.生成(程序);
            
            // Assert
            Assert.Contains("Console.WriteLine(", 代碼);
            Assert.Contains("\"你好世界\"", 代碼);
        }
        
        [Fact]
        public void 測試條件語句代碼生成()
        {
            // Arrange
            var 生成器 = new 代碼生成器();
            var 程序 = new 程序節點();
            var 條件語句 = new 條件語句節點(1, 1)
            {
                條件表達式 = new 二元運算表達式節點(1, 3)
                {
                    左操作數 = new 字面量表達式節點(1, 3) { 類型 = 詞法分析.詞法單元類型.數字, 值 = 3.0 },
                    運算符 = "大於",
                    右操作數 = new 字面量表達式節點(1, 7) { 類型 = 詞法分析.詞法單元類型.數字, 值 = 2.0 }
                }
            };
            
            條件語句.真分支.Add(new 輸出語句節點(2, 1)
            {
                輸出表達式 = new 字面量表達式節點(2, 3)
                {
                    類型 = 詞法分析.詞法單元類型.字符串,
                    值 = "真"
                }
            });
            
            條件語句.假分支.Add(new 輸出語句節點(4, 1)
            {
                輸出表達式 = new 字面量表達式節點(4, 3)
                {
                    類型 = 詞法分析.詞法單元類型.字符串,
                    值 = "假"
                }
            });
            
            程序.語句列表.Add(條件語句);
            
            // Act
            var 代碼 = 生成器.生成(程序);
            
            // Assert
            Assert.Contains("if (", 代碼);
            Assert.Contains(">", 代碼);
            Assert.Contains("else", 代碼);
            Assert.Contains("\"真\"", 代碼);
            Assert.Contains("\"假\"", 代碼);
        }
        
        [Fact]
        public void 測試循環語句代碼生成()
        {
            // Arrange
            var 生成器 = new 代碼生成器();
            var 程序 = new 程序節點();
            var 循環語句 = new 循環語句節點(1, 1);
            循環語句.循環體.Add(new 輸出語句節點(2, 1)
            {
                輸出表達式 = new 字面量表達式節點(2, 3)
                {
                    類型 = 詞法分析.詞法單元類型.字符串,
                    值 = "循環中"
                }
            });
            程序.語句列表.Add(循環語句);
            
            // Act
            var 代碼 = 生成器.生成(程序);
            
            // Assert
            Assert.Contains("while (true)", 代碼);
            Assert.Contains("\"循環中\"", 代碼);
        }
        
        [Fact]
        public void 測試二元運算表達式代碼生成()
        {
            // Arrange
            var 生成器 = new 代碼生成器();
            var 程序 = new 程序節點();
            var 輸出語句 = new 輸出語句節點(1, 1)
            {
                輸出表達式 = new 二元運算表達式節點(1, 3)
                {
                    左操作數 = new 字面量表達式節點(1, 3) { 類型 = 詞法分析.詞法單元類型.數字, 值 = 5.0 },
                    運算符 = "加",
                    右操作數 = new 字面量表達式節點(1, 7) { 類型 = 詞法分析.詞法單元類型.數字, 值 = 3.0 }
                }
            };
            程序.語句列表.Add(輸出語句);
            
            // Act
            var 代碼 = 生成器.生成(程序);
            
            // Assert
            Assert.Contains("(5 + 3)", 代碼);
        }
        
        [Fact]
        public void 測試運算符轉換()
        {
            // Arrange
            var 生成器 = new 代碼生成器();
            var 程序 = new 程序節點();
            
            // 測試各種運算符
            var 運算符測試 = new[]
            {
                ("加", "+"),
                ("減", "-"),
                ("乘", "*"),
                ("除", "/"),
                ("大於", ">"),
                ("小於", "<"),
                ("等於", "==")
            };
            
            foreach (var (文言文運算符, 期望運算符) in 運算符測試)
            {
                var 輸出語句 = new 輸出語句節點(1, 1)
                {
                    輸出表達式 = new 二元運算表達式節點(1, 3)
                    {
                        左操作數 = new 字面量表達式節點(1, 3) { 類型 = 詞法分析.詞法單元類型.數字, 值 = 1.0 },
                        運算符 = 文言文運算符,
                        右操作數 = new 字面量表達式節點(1, 7) { 類型 = 詞法分析.詞法單元類型.數字, 值 = 2.0 }
                    }
                };
                程序.語句列表.Clear();
                程序.語句列表.Add(輸出語句);
                
                // Act
                var 代碼 = 生成器.生成(程序);
                
                // Assert
                Assert.Contains($"1 {期望運算符} 2", 代碼);
            }
        }
        
        [Fact]
        public void 測試類型轉換()
        {
            // Arrange
            var 生成器 = new 代碼生成器();
            var 程序 = new 程序節點();
            
            // 測試各種類型轉換
            var 類型測試 = new[]
            {
                ("一數", "double"),
                ("一言", "string"),
                ("一列", "List<object>")
            };
            
            foreach (var (文言文類型, 期望類型) in 類型測試)
            {
                var 變量聲明 = new 變量聲明節點(1, 1)
                {
                    變量類型 = 文言文類型,
                    變量名稱 = "測試變量"
                };
                程序.語句列表.Clear();
                程序.語句列表.Add(變量聲明);
                
                // Act
                var 代碼 = 生成器.生成(程序);
                
                // Assert
                Assert.Contains(期望類型, 代碼);
            }
        }
        
        [Fact]
        public void 測試字符串轉義()
        {
            // Arrange
            var 生成器 = new 代碼生成器();
            var 程序 = new 程序節點();
            var 輸出語句 = new 輸出語句節點(1, 1)
            {
                輸出表達式 = new 字面量表達式節點(1, 3)
                {
                    類型 = 詞法分析.詞法單元類型.字符串,
                    值 = "包含\"引號\"的字符串"
                }
            };
            程序.語句列表.Add(輸出語句);
            
            // Act
            var 代碼 = 生成器.生成(程序);
            
            // Assert
            Assert.Contains("\\\"", 代碼); // 轉義的引號
        }
        
        [Fact]
        public void 測試標識符表達式代碼生成()
        {
            // Arrange
            var 生成器 = new 代碼生成器();
            var 程序 = new 程序節點();
            var 輸出語句 = new 輸出語句節點(1, 1)
            {
                輸出表達式 = new 標識符表達式節點(1, 3)
                {
                    名稱 = "變量名"
                }
            };
            程序.語句列表.Add(輸出語句);
            
            // Act
            var 代碼 = 生成器.生成(程序);
            
            // Assert
            // 中文標識符應該被轉換為有效的C#標識符
            Assert.Contains("變量_", 代碼);
        }
        
        [Fact]
        public void 測試完整程序結構()
        {
            // Arrange
            var 生成器 = new 代碼生成器();
            var 程序 = new 程序節點();
            
            // Act
            var 代碼 = 生成器.生成(程序);
            
            // Assert
            Assert.Contains("using System;", 代碼);
            Assert.Contains("using System.Collections.Generic;", 代碼);
            Assert.Contains("namespace 文言文程序", 代碼);
            Assert.Contains("public class 主類", 代碼);
            Assert.Contains("public static void Main(string[] args)", 代碼);
            
            // 檢查大括號匹配
            int 左大括號數量 = 0;
            int 右大括號數量 = 0;
            foreach (char 字符 in 代碼)
            {
                if (字符 == '{') 左大括號數量++;
                if (字符 == '}') 右大括號數量++;
            }
            Assert.Equal(左大括號數量, 右大括號數量);
        }
    }
} 