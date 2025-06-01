using System;
using System.Collections.Generic;
using Xunit;
using 詞法分析;

namespace ChineseCompiler.Tests
{
    /// <summary>
    /// 詞法分析器測試類
    /// </summary>
    public class 詞法分析測試
    {
        [Fact]
        public void 測試基本關鍵字識別()
        {
            // Arrange
            var 分析器 = new 詞法分析器();
            string 源代碼 = "吾有一數曰也";

            // Act
            var 結果 = 分析器.分析(源代碼);

            // Debug output
            foreach (var token in 結果)
            {
                System.Console.WriteLine($"Token: {token.類型} = '{token.值}'");
            }

            // Assert
            Assert.Equal(5, 結果.Count); // 修正期望值：吾有、一數、曰、也、文件結束
            Assert.Equal(詞法單元類型.吾有, 結果[0].類型);
            Assert.Equal(詞法單元類型.一數, 結果[1].類型);
            Assert.Equal(詞法單元類型.曰, 結果[2].類型);
            Assert.Equal(詞法單元類型.也, 結果[3].類型);
            Assert.Equal(詞法單元類型.文件結束, 結果[4].類型);
        }

        [Fact]
        public void 測試字符串字面量()
        {
            // Arrange
            var 分析器 = new 詞法分析器();
            string 源代碼 = "曰「變量名」";

            // Act
            var 結果 = 分析器.分析(源代碼);

            // Assert
            Assert.Equal(3, 結果.Count);
            Assert.Equal(詞法單元類型.曰, 結果[0].類型);
            Assert.Equal(詞法單元類型.字符串, 結果[1].類型);
            Assert.Equal("變量名", 結果[1].值);
        }

        [Fact]
        public void 測試數字字面量()
        {
            // Arrange
            var 分析器 = new 詞法分析器();
            string 源代碼 = "其值三也";

            // Act
            var 結果 = 分析器.分析(源代碼);

            // Assert
            Assert.Equal(4, 結果.Count);
            Assert.Equal(詞法單元類型.其值, 結果[0].類型);
            Assert.Equal(詞法單元類型.數字, 結果[1].類型);
            Assert.Equal("三", 結果[1].值);
            Assert.Equal(詞法單元類型.也, 結果[2].類型);
        }

        [Fact]
        public void 測試阿拉伯數字()
        {
            // Arrange
            var 分析器 = new 詞法分析器();
            string 源代碼 = "其值123也";

            // Act
            var 結果 = 分析器.分析(源代碼);

            // Assert
            Assert.Equal(4, 結果.Count);
            Assert.Equal(詞法單元類型.其值, 結果[0].類型);
            Assert.Equal(詞法單元類型.數字, 結果[1].類型);
            Assert.Equal("123", 結果[1].值);
        }

        [Fact]
        public void 測試運算符()
        {
            // Arrange
            var 分析器 = new 詞法分析器();
            string 源代碼 = "加減乘除";

            // Act
            var 結果 = 分析器.分析(源代碼);

            // Assert
            Assert.Equal(5, 結果.Count);
            Assert.Equal(詞法單元類型.加, 結果[0].類型);
            Assert.Equal(詞法單元類型.減, 結果[1].類型);
            Assert.Equal(詞法單元類型.乘, 結果[2].類型);
            Assert.Equal(詞法單元類型.除, 結果[3].類型);
        }

        [Fact]
        public void 測試比較運算符()
        {
            // Arrange
            var 分析器 = new 詞法分析器();
            string 源代碼 = "大於小於等於";

            // Act
            var 結果 = 分析器.分析(源代碼);

            // Assert
            Assert.Equal(4, 結果.Count);
            Assert.Equal(詞法單元類型.大於, 結果[0].類型);
            Assert.Equal(詞法單元類型.小於, 結果[1].類型);
            Assert.Equal(詞法單元類型.等於, 結果[2].類型);
        }

        [Fact]
        public void 測試完整變量聲明()
        {
            // Arrange
            var 分析器 = new 詞法分析器();
            string 源代碼 = "吾有一數曰「甲」其值三也";

            // Act
            var 結果 = 分析器.分析(源代碼);

            // Assert
            Assert.Equal(8, 結果.Count); // 包括文件結束標記
            Assert.Equal(詞法單元類型.吾有, 結果[0].類型);
            Assert.Equal(詞法單元類型.一數, 結果[1].類型);
            Assert.Equal(詞法單元類型.曰, 結果[2].類型);
            Assert.Equal(詞法單元類型.字符串, 結果[3].類型);
            Assert.Equal("甲", 結果[3].值);
            Assert.Equal(詞法單元類型.其值, 結果[4].類型);
            Assert.Equal(詞法單元類型.數字, 結果[5].類型);
            Assert.Equal("三", 結果[5].值);
            Assert.Equal(詞法單元類型.也, 結果[6].類型);
        }

        [Fact]
        public void 測試輸出語句()
        {
            // Arrange
            var 分析器 = new 詞法分析器();
            string 源代碼 = "云「甲」";

            // Act
            var 結果 = 分析器.分析(源代碼);

            // Assert
            Assert.Equal(3, 結果.Count);
            Assert.Equal(詞法單元類型.云, 結果[0].類型);
            Assert.Equal(詞法單元類型.字符串, 結果[1].類型);
            Assert.Equal("甲", 結果[1].值);
        }

        [Fact]
        public void 測試條件語句關鍵字()
        {
            // Arrange
            var 分析器 = new 詞法分析器();
            string 源代碼 = "若者若非云云";

            // Act
            var 結果 = 分析器.分析(源代碼);

            // Assert
            Assert.Equal(5, 結果.Count);
            Assert.Equal(詞法單元類型.若, 結果[0].類型);
            Assert.Equal(詞法單元類型.者, 結果[1].類型);
            Assert.Equal(詞法單元類型.若非, 結果[2].類型);
            Assert.Equal(詞法單元類型.云云, 結果[3].類型);
        }

        [Fact]
        public void 測試函數定義關鍵字()
        {
            // Arrange
            var 分析器 = new 詞法分析器();
            string 源代碼 = "有術欲行是術必先得是謂之術也";

            // Act
            var 結果 = 分析器.分析(源代碼);

            // Assert
            Assert.Equal(6, 結果.Count);
            Assert.Equal(詞法單元類型.有術, 結果[0].類型);
            Assert.Equal(詞法單元類型.欲行是術, 結果[1].類型);
            Assert.Equal(詞法單元類型.必先得, 結果[2].類型);
            Assert.Equal(詞法單元類型.是謂, 結果[3].類型);
            Assert.Equal(詞法單元類型.之術也, 結果[4].類型);
        }

        [Fact]
        public void 測試空白字符處理()
        {
            // Arrange
            var 分析器 = new 詞法分析器();
            string 源代碼 = "吾有  一數\n曰\t「甲」";

            // Act
            var 結果 = 分析器.分析(源代碼);

            // Assert
            Assert.Equal(5, 結果.Count);
            Assert.Equal(詞法單元類型.吾有, 結果[0].類型);
            Assert.Equal(詞法單元類型.一數, 結果[1].類型);
            Assert.Equal(詞法單元類型.曰, 結果[2].類型);
            Assert.Equal(詞法單元類型.字符串, 結果[3].類型);
        }

        [Fact]
        public void 測試行號列號記錄()
        {
            // Arrange
            var 分析器 = new 詞法分析器();
            string 源代碼 = "吾有\n一數";

            // Act
            var 結果 = 分析器.分析(源代碼);

            // Assert
            Assert.Equal(1, 結果[0].行號);
            Assert.Equal(2, 結果[1].行號);
        }
    }
}