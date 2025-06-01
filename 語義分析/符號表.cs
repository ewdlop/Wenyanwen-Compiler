using System;
using System.Collections.Generic;

namespace 語義分析
{
    /// <summary>
    /// 符號類型枚舉
    /// </summary>
    public enum 符號類型
    {
        變量,
        函數,
        參數
    }
    
    /// <summary>
    /// 符號信息
    /// </summary>
    public class 符號信息
    {
        /// <summary>
        /// 符號名稱
        /// </summary>
        public string 名稱 { get; set; }
        
        /// <summary>
        /// 符號類型（數據類型）
        /// </summary>
        public string 數據類型 { get; set; }
        
        /// <summary>
        /// 符號種類
        /// </summary>
        public 符號類型 符號種類 { get; set; }
        
        /// <summary>
        /// 作用域層級
        /// </summary>
        public int 作用域層級 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="名稱">符號名稱</param>
        /// <param name="數據類型">數據類型</param>
        /// <param name="符號種類">符號種類</param>
        /// <param name="作用域層級">作用域層級</param>
        public 符號信息(string 名稱, string 數據類型, 符號類型 符號種類, int 作用域層級)
        {
            this.名稱 = 名稱;
            this.數據類型 = 數據類型;
            this.符號種類 = 符號種類;
            this.作用域層級 = 作用域層級;
        }
        
        public override string ToString()
        {
            return $"{名稱} ({數據類型}, {符號種類}, 層級{作用域層級})";
        }
    }
    
    /// <summary>
    /// 符號表 - 管理變量和函數的聲明信息
    /// </summary>
    public class 符號表
    {
        private Dictionary<string, 符號信息> 符號字典;
        private Stack<Dictionary<string, 符號信息>> 作用域棧;
        private int 當前作用域層級;
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 符號表()
        {
            符號字典 = new Dictionary<string, 符號信息>();
            作用域棧 = new Stack<Dictionary<string, 符號信息>>();
            當前作用域層級 = 0;
            
            // 添加內建函數
            添加內建符號();
        }
        
        /// <summary>
        /// 添加符號到符號表
        /// </summary>
        /// <param name="名稱">符號名稱</param>
        /// <param name="數據類型">數據類型</param>
        /// <param name="符號種類">符號種類</param>
        public void 添加符號(string 名稱, string 數據類型, 符號類型 符號種類)
        {
            var 符號 = new 符號信息(名稱, 數據類型, 符號種類, 當前作用域層級);
            
            // 在當前作用域中檢查是否重複
            if (當前作用域層級 > 0 && 作用域棧.Count > 0)
            {
                var 當前作用域 = 作用域棧.Peek();
                if (當前作用域.ContainsKey(名稱))
                {
                    throw new Exception($"符號 '{名稱}' 在當前作用域中已存在");
                }
                當前作用域[名稱] = 符號;
            }
            else
            {
                if (符號字典.ContainsKey(名稱))
                {
                    throw new Exception($"符號 '{名稱}' 在全局作用域中已存在");
                }
                符號字典[名稱] = 符號;
            }
        }
        
        /// <summary>
        /// 檢查符號是否存在
        /// </summary>
        /// <param name="名稱">符號名稱</param>
        /// <returns>是否存在</returns>
        public bool 是否存在(string 名稱)
        {
            // 從當前作用域向上查找
            foreach (var 作用域 in 作用域棧)
            {
                if (作用域.ContainsKey(名稱))
                {
                    return true;
                }
            }
            
            // 檢查全局作用域
            return 符號字典.ContainsKey(名稱);
        }
        
        /// <summary>
        /// 獲取符號信息
        /// </summary>
        /// <param name="名稱">符號名稱</param>
        /// <returns>符號信息</returns>
        public 符號信息 獲取符號(string 名稱)
        {
            // 從當前作用域向上查找
            foreach (var 作用域 in 作用域棧)
            {
                if (作用域.TryGetValue(名稱, out 符號信息 符號))
                {
                    return 符號;
                }
            }
            
            // 檢查全局作用域
            if (符號字典.TryGetValue(名稱, out 符號信息 全局符號))
            {
                return 全局符號;
            }
            
            return null;
        }
        
        /// <summary>
        /// 進入新的作用域
        /// </summary>
        public void 進入作用域()
        {
            當前作用域層級++;
            作用域棧.Push(new Dictionary<string, 符號信息>());
        }
        
        /// <summary>
        /// 退出當前作用域
        /// </summary>
        public void 退出作用域()
        {
            if (作用域棧.Count > 0)
            {
                作用域棧.Pop();
                當前作用域層級--;
            }
        }
        
        /// <summary>
        /// 獲取當前作用域層級
        /// </summary>
        /// <returns>作用域層級</returns>
        public int 獲取作用域層級()
        {
            return 當前作用域層級;
        }
        
        /// <summary>
        /// 獲取所有符號
        /// </summary>
        /// <returns>符號列表</returns>
        public List<符號信息> 獲取所有符號()
        {
            var 所有符號 = new List<符號信息>();
            
            // 添加全局符號
            所有符號.AddRange(符號字典.Values);
            
            // 添加局部符號
            foreach (var 作用域 in 作用域棧)
            {
                所有符號.AddRange(作用域.Values);
            }
            
            return 所有符號;
        }
        
        /// <summary>
        /// 清空符號表
        /// </summary>
        public void 清空()
        {
            符號字典.Clear();
            作用域棧.Clear();
            當前作用域層級 = 0;
            添加內建符號();
        }
        
        /// <summary>
        /// 添加內建符號
        /// </summary>
        private void 添加內建符號()
        {
            // 添加內建函數
            符號字典["輸出"] = new 符號信息("輸出", "函數", 符號類型.函數, 0);
            符號字典["輸入"] = new 符號信息("輸入", "函數", 符號類型.函數, 0);
            符號字典["長度"] = new 符號信息("長度", "函數", 符號類型.函數, 0);
            符號字典["轉換"] = new 符號信息("轉換", "函數", 符號類型.函數, 0);
            
            // 添加內建常量
            符號字典["真"] = new 符號信息("真", "布爾", 符號類型.變量, 0);
            符號字典["假"] = new 符號信息("假", "布爾", 符號類型.變量, 0);
            符號字典["空"] = new 符號信息("空", "空值", 符號類型.變量, 0);
        }
        
        /// <summary>
        /// 打印符號表內容（調試用）
        /// </summary>
        public void 打印符號表()
        {
            Console.WriteLine("=== 符號表內容 ===");
            Console.WriteLine("全局作用域:");
            foreach (var 符號 in 符號字典.Values)
            {
                Console.WriteLine($"  {符號}");
            }
            
            int 層級 = 作用域棧.Count;
            foreach (var 作用域 in 作用域棧)
            {
                Console.WriteLine($"作用域 {層級}:");
                foreach (var 符號 in 作用域.Values)
                {
                    Console.WriteLine($"  {符號}");
                }
                層級--;
            }
            Console.WriteLine("================");
        }
    }
} 