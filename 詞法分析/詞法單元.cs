using System;

namespace 詞法分析
{
    /// <summary>
    /// 詞法單元類型枚舉
    /// </summary>
    public enum 詞法單元類型
    {
        // 關鍵字
        吾有,      // 變量聲明開始
        曰,        // 命名
        其值,      // 賦值
        也,        // 語句結束
        有術,      // 函數定義開始
        欲行是術,  // 函數執行
        必先得,    // 參數聲明
        乃得,      // 返回
        是謂,      // 函數定義結束
        之術也,    // 函數定義結束標記
        若,        // 條件判斷
        者,        // 條件判斷標記
        若非,      // else
        云,        // 輸出/執行
        云云,      // 語句塊結束
        恆為是,    // 循環開始
        乃止,      // 循環結束/break
        施,        // 操作符
        於,        // 操作符連接
        名之,      // 賦值操作
        加,        // 加法
        減,        // 減法
        乘,        // 乘法
        除,        // 除法
        大於,      // 大於
        小於,      // 小於
        等於,      // 等於
        以,        // 操作符連接詞
        
        // 數據類型
        一數,      // 數字類型
        一言,      // 字符串類型
        一列,      // 數組類型
        
        // 字面量
        數字,      // 數字字面量
        字符串,    // 字符串字面量
        標識符,    // 標識符
        
        // 符號
        左引號,    // 「
        右引號,    // 」
        頓號,      // 、
        
        // 特殊
        文件結束,  // EOF
        未知       // 未知詞法單元
    }
    
    /// <summary>
    /// 詞法單元
    /// </summary>
    public class 詞法單元
    {
        /// <summary>
        /// 詞法單元類型
        /// </summary>
        public 詞法單元類型 類型 { get; set; }
        
        /// <summary>
        /// 詞法單元的文本值
        /// </summary>
        public string 值 { get; set; }
        
        /// <summary>
        /// 在源代碼中的行號
        /// </summary>
        public int 行號 { get; set; }
        
        /// <summary>
        /// 在源代碼中的列號
        /// </summary>
        public int 列號 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="類型">詞法單元類型</param>
        /// <param name="值">詞法單元值</param>
        /// <param name="行號">行號</param>
        /// <param name="列號">列號</param>
        public 詞法單元(詞法單元類型 類型, string 值, int 行號, int 列號)
        {
            this.類型 = 類型;
            this.值 = 值;
            this.行號 = 行號;
            this.列號 = 列號;
        }
        
        /// <summary>
        /// 轉換為字符串表示
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return $"[{類型}] '{值}' ({行號}:{列號})";
        }
    }
} 