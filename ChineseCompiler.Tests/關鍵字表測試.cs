using System;
using System.Linq;
using Xunit;
using 詞法分析;

namespace ChineseCompiler.Tests
{
    /// <summary>
    /// 關鍵字表測試類
    /// </summary>
    public class 關鍵字表測試
    {
        [Theory]
        [InlineData("吾有", true)]
        [InlineData("曰", true)]
        [InlineData("其值", true)]
        [InlineData("也", true)]
        [InlineData("一數", true)]
        [InlineData("一言", true)]
        [InlineData("一列", true)]
        [InlineData("云", true)]
        [InlineData("若", true)]
        [InlineData("者", true)]
        [InlineData("若非", true)]
        [InlineData("云云", true)]
        [InlineData("有術", true)]
        [InlineData("是謂", true)]
        [InlineData("之術也", true)]
        [InlineData("加", true)]
        [InlineData("減", true)]
        [InlineData("乘", true)]
        [InlineData("除", true)]
        [InlineData("大於", true)]
        [InlineData("小於", true)]
        [InlineData("等於", true)]
        [InlineData("不是關鍵字", false)]
        [InlineData("", false)]
        [InlineData("abc", false)]
        public void 測試關鍵字識別(string 文本, bool 期望結果)
        {
            // Act & Assert
            Assert.Equal(期望結果, 關鍵字表.是關鍵字(文本));
        }
        
        [Theory]
        [InlineData("吾有", 詞法單元類型.吾有)]
        [InlineData("曰", 詞法單元類型.曰)]
        [InlineData("其值", 詞法單元類型.其值)]
        [InlineData("也", 詞法單元類型.也)]
        [InlineData("一數", 詞法單元類型.一數)]
        [InlineData("一言", 詞法單元類型.一言)]
        [InlineData("云", 詞法單元類型.云)]
        [InlineData("若", 詞法單元類型.若)]
        [InlineData("加", 詞法單元類型.加)]
        [InlineData("大於", 詞法單元類型.大於)]
        public void 測試關鍵字類型獲取(string 關鍵字, 詞法單元類型 期望類型)
        {
            // Act
            var 結果 = 關鍵字表.獲取關鍵字類型(關鍵字);
            
            // Assert
            Assert.Equal(期望類型, 結果);
        }
        
        [Theory]
        [InlineData("一", true)]
        [InlineData("二", true)]
        [InlineData("三", true)]
        [InlineData("四", true)]
        [InlineData("五", true)]
        [InlineData("六", true)]
        [InlineData("七", true)]
        [InlineData("八", true)]
        [InlineData("九", true)]
        [InlineData("十", true)]
        [InlineData("百", true)]
        [InlineData("千", true)]
        [InlineData("萬", true)]
        [InlineData("零", true)]
        [InlineData("〇", true)]
        [InlineData("壹", true)]
        [InlineData("貳", true)]
        [InlineData("叁", true)]
        [InlineData("肆", true)]
        [InlineData("伍", true)]
        [InlineData("陸", true)]
        [InlineData("柒", true)]
        [InlineData("捌", true)]
        [InlineData("玖", true)]
        [InlineData("拾", true)]
        [InlineData("佰", true)]
        [InlineData("仟", true)]
        [InlineData("不是數字", false)]
        [InlineData("abc", false)]
        [InlineData("", false)]
        public void 測試文言文數字識別(string 文本, bool 期望結果)
        {
            // Act & Assert
            Assert.Equal(期望結果, 關鍵字表.是文言文數字(文本));
        }
        
        [Theory]
        [InlineData("一", 1)]
        [InlineData("二", 2)]
        [InlineData("三", 3)]
        [InlineData("四", 4)]
        [InlineData("五", 5)]
        [InlineData("六", 6)]
        [InlineData("七", 7)]
        [InlineData("八", 8)]
        [InlineData("九", 9)]
        [InlineData("十", 10)]
        [InlineData("百", 100)]
        [InlineData("千", 1000)]
        [InlineData("萬", 10000)]
        [InlineData("零", 0)]
        [InlineData("〇", 0)]
        [InlineData("壹", 1)]
        [InlineData("貳", 2)]
        [InlineData("叁", 3)]
        [InlineData("肆", 4)]
        [InlineData("伍", 5)]
        [InlineData("陸", 6)]
        [InlineData("柒", 7)]
        [InlineData("捌", 8)]
        [InlineData("玖", 9)]
        [InlineData("拾", 10)]
        [InlineData("佰", 100)]
        [InlineData("仟", 1000)]
        public void 測試文言文數字轉換(string 文言文數字, int 期望值)
        {
            // Act
            var 結果 = 關鍵字表.轉換為數字(文言文數字);
            
            // Assert
            Assert.Equal(期望值, 結果);
        }
        
        [Fact]
        public void 測試無效數字轉換拋出異常()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => 關鍵字表.轉換為數字("不是數字"));
        }
        
        [Fact]
        public void 測試非關鍵字返回標識符類型()
        {
            // Act
            var 結果 = 關鍵字表.獲取關鍵字類型("不是關鍵字");
            
            // Assert
            Assert.Equal(詞法單元類型.標識符, 結果);
        }
        
        [Fact]
        public void 測試獲取所有關鍵字()
        {
            // Act
            var 關鍵字列表 = 關鍵字表.獲取所有關鍵字().ToList();
            
            // Assert
            Assert.NotEmpty(關鍵字列表);
            Assert.Contains("吾有", 關鍵字列表);
            Assert.Contains("曰", 關鍵字列表);
            Assert.Contains("也", 關鍵字列表);
            Assert.Contains("云", 關鍵字列表);
        }
    }
} 