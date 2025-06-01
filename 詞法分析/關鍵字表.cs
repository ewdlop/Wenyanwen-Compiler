using System;
using System.Collections.Generic;

namespace 詞法分析
{
    /// <summary>
    /// 關鍵字表 - 存儲文言文關鍵字與詞法單元類型的映射
    /// </summary>
    public static class 關鍵字表
    {
        /// <summary>
        /// 關鍵字字典
        /// </summary>
        private static readonly Dictionary<string, 詞法單元類型> 關鍵字字典 = new Dictionary<string, 詞法單元類型>
        {
            // 變量聲明相關
            { "吾有", 詞法單元類型.吾有 },
            { "曰", 詞法單元類型.曰 },
            { "其值", 詞法單元類型.其值 },
            { "也", 詞法單元類型.也 },
            
            // 數據類型
            { "一數", 詞法單元類型.一數 },
            { "一言", 詞法單元類型.一言 },
            { "一列", 詞法單元類型.一列 },
            
            // 函數相關
            { "有術", 詞法單元類型.有術 },
            { "欲行是術", 詞法單元類型.欲行是術 },
            { "必先得", 詞法單元類型.必先得 },
            { "乃得", 詞法單元類型.乃得 },
            { "是謂", 詞法單元類型.是謂 },
            { "之術也", 詞法單元類型.之術也 },
            
            // 條件判斷
            { "若", 詞法單元類型.若 },
            { "者", 詞法單元類型.者 },
            { "若非", 詞法單元類型.若非 },
            
            // 輸出和執行
            { "云", 詞法單元類型.云 },
            { "云云", 詞法單元類型.云云 },
            
            // 循環
            { "恆為是", 詞法單元類型.恆為是 },
            { "乃止", 詞法單元類型.乃止 },
            
            // 操作符
            { "施", 詞法單元類型.施 },
            { "於", 詞法單元類型.於 },
            { "名之", 詞法單元類型.名之 },
            { "以", 詞法單元類型.以 },
            
            // 算術操作
            { "加", 詞法單元類型.加 },
            { "減", 詞法單元類型.減 },
            { "乘", 詞法單元類型.乘 },
            { "除", 詞法單元類型.除 },
            
            // 比較操作
            { "大於", 詞法單元類型.大於 },
            { "小於", 詞法單元類型.小於 },
            { "等於", 詞法單元類型.等於 }
        };
        
        /// <summary>
        /// 數字關鍵字映射表（文言文數字表示）
        /// </summary>
        private static readonly Dictionary<string, int> 數字映射表 = new Dictionary<string, int>
        {
            { "零", 0 }, { "〇", 0 },
            { "一", 1 }, { "壹", 1 },
            { "二", 2 }, { "貳", 2 }, { "兩", 2 },
            { "三", 3 }, { "參", 3 }, { "叁", 3 },
            { "四", 4 }, { "肆", 4 },
            { "五", 5 }, { "伍", 5 },
            { "六", 6 }, { "陸", 6 },
            { "七", 7 }, { "柒", 7 },
            { "八", 8 }, { "捌", 8 },
            { "九", 9 }, { "玖", 9 },
            { "十", 10 }, { "拾", 10 },
            { "百", 100 }, { "佰", 100 },
            { "千", 1000 }, { "仟", 1000 },
            { "萬", 10000 }
        };
        
        /// <summary>
        /// 檢查給定的字符串是否為關鍵字
        /// </summary>
        /// <param name="文本">要檢查的文本</param>
        /// <returns>如果是關鍵字返回true，否則返回false</returns>
        public static bool 是關鍵字(string 文本)
        {
            return 關鍵字字典.ContainsKey(文本);
        }
        
        /// <summary>
        /// 獲取關鍵字對應的詞法單元類型
        /// </summary>
        /// <param name="關鍵字">關鍵字文本</param>
        /// <returns>對應的詞法單元類型</returns>
        public static 詞法單元類型 獲取關鍵字類型(string 關鍵字)
        {
            if (關鍵字字典.TryGetValue(關鍵字, out 詞法單元類型 類型))
            {
                return 類型;
            }
            return 詞法單元類型.標識符;
        }
        
        /// <summary>
        /// 檢查給定的字符串是否為文言文數字
        /// </summary>
        /// <param name="文本">要檢查的文本</param>
        /// <returns>如果是文言文數字返回true，否則返回false</returns>
        public static bool 是文言文數字(string 文本)
        {
            return 數字映射表.ContainsKey(文本) || 嘗試解析複合數字(文本, out _);
        }
        
        /// <summary>
        /// 將文言文數字轉換為阿拉伯數字
        /// </summary>
        /// <param name="文言文數字">文言文數字字符串</param>
        /// <returns>對應的阿拉伯數字</returns>
        public static int 轉換為數字(string 文言文數字)
        {
            if (數字映射表.TryGetValue(文言文數字, out int 簡單數字))
            {
                return 簡單數字;
            }
            
            if (嘗試解析複合數字(文言文數字, out int 複合數字))
            {
                return 複合數字;
            }
            
            throw new ArgumentException($"無法識別的文言文數字: {文言文數字}");
        }
        
        /// <summary>
        /// 嘗試解析複合的文言文數字（如"三十五"）
        /// </summary>
        /// <param name="文本">文言文數字文本</param>
        /// <param name="結果">解析結果</param>
        /// <returns>是否解析成功</returns>
        private static bool 嘗試解析複合數字(string 文本, out int 結果)
        {
            結果 = 0;
            
            // 簡單實現：處理常見的複合數字格式
            // 例如："三十五" = 3 * 10 + 5 = 35
            
            int 當前值 = 0;
            int 總和 = 0;
            
            for (int i = 0; i < 文本.Length; i++)
            {
                string 字符 = 文本[i].ToString();
                
                if (數字映射表.TryGetValue(字符, out int 數值))
                {
                    if (數值 >= 10) // 十、百、千、萬
                    {
                        if (當前值 == 0) 當前值 = 1; // 處理"十"開頭的情況
                        總和 += 當前值 * 數值;
                        當前值 = 0;
                    }
                    else
                    {
                        當前值 = 數值;
                    }
                }
                else
                {
                    return false; // 包含無法識別的字符
                }
            }
            
            結果 = 總和 + 當前值;
            return true;
        }
        
        /// <summary>
        /// 獲取所有關鍵字
        /// </summary>
        /// <returns>關鍵字集合</returns>
        public static IEnumerable<string> 獲取所有關鍵字()
        {
            return 關鍵字字典.Keys;
        }
    }
} 