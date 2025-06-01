using System;
using 詞法分析;
using 語法分析;
using 語義分析;
using 代碼生成;

namespace 文言文編譯器
{
    /// <summary>
    /// 文言文編譯器主程序
    /// </summary>
    class Program
    {
        static void Main(string[] 參數)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("文言文編譯器 v1.0");
            Console.WriteLine("====================");
            
            if (參數.Length == 0)
            {
                顯示使用說明();
                return;
            }
            
            string 源文件路徑 = 參數[0];
            
            try
            {
                編譯文件(源文件路徑);
            }
            catch (Exception 異常)
            {
                Console.WriteLine($"編譯錯誤: {異常.Message}");
            }
        }
        
        /// <summary>
        /// 編譯指定的文言文源文件
        /// </summary>
        /// <param name="文件路徑">源文件路徑</param>
        static void 編譯文件(string 文件路徑)
        {
            Console.WriteLine($"正在編譯文件: {文件路徑}");
            
            // 讀取源代碼
            string 源代碼 = File.ReadAllText(文件路徑);
            Console.WriteLine("源代碼讀取完成");
            
            // 詞法分析
            var 詞法分析器 = new 詞法分析器();
            var 詞法單元列表 = 詞法分析器.分析(源代碼);
            Console.WriteLine($"詞法分析完成，共生成 {詞法單元列表.Count} 個詞法單元");
            
            // 語法分析
            var 語法分析器 = new 語法分析器();
            var 抽象語法樹 = 語法分析器.分析(詞法單元列表);
            Console.WriteLine("語法分析完成，抽象語法樹構建成功");
            
            // 語義分析
            var 語義分析器 = new 語義分析器();
            語義分析器.分析(抽象語法樹);
            Console.WriteLine("語義分析完成");
            
            // 代碼生成
            var 代碼生成器 = new 代碼生成器();
            string 目標代碼 = 代碼生成器.生成(抽象語法樹);
            Console.WriteLine("代碼生成完成");
            
            // 輸出結果
            string 輸出文件 = Path.ChangeExtension(文件路徑, ".cs");
            File.WriteAllText(輸出文件, 目標代碼);
            Console.WriteLine($"編譯成功！輸出文件: {輸出文件}");
        }
        
        /// <summary>
        /// 顯示使用說明
        /// </summary>
        static void 顯示使用說明()
        {
            Console.WriteLine("使用方法:");
            Console.WriteLine("  文言文編譯器.exe <源文件.wy>");
            Console.WriteLine();
            Console.WriteLine("示例:");
            Console.WriteLine("  文言文編譯器.exe 示例.wy");
            Console.WriteLine();
            Console.WriteLine("支持的文件擴展名: .wy, .wenyan");
        }
    }
}
