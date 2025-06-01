using System;
using System.Collections.Generic;
using System.Text;
using 語法分析;
using 詞法分析;

namespace 代碼生成
{
    /// <summary>
    /// 代碼生成器 - 將抽象語法樹轉換為C#代碼
    /// </summary>
    public class 代碼生成器 : 語法樹訪問者
    {
        private StringBuilder 代碼構建器;
        private int 縮進層級;
        private const string 縮進字符 = "    ";
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 代碼生成器()
        {
            代碼構建器 = new StringBuilder();
            縮進層級 = 0;
        }
        
        /// <summary>
        /// 生成C#代碼
        /// </summary>
        /// <param name="程序節點">程序根節點</param>
        /// <returns>生成的C#代碼</returns>
        public string 生成(程序節點 程序節點)
        {
            代碼構建器.Clear();
            縮進層級 = 0;
            
            // 添加using語句
            添加行("using System;");
            添加行("using System.Collections.Generic;");
            添加行("");
            
            // 添加命名空間和類定義
            添加行("namespace 文言文程序");
            添加行("{");
            增加縮進();
            
            添加行("public class 主類");
            添加行("{");
            增加縮進();
            
            // 生成主方法
            添加行("public static void Main(string[] args)");
            添加行("{");
            增加縮進();
            
            // 訪問程序節點
            程序節點.接受訪問者(this);
            
            減少縮進();
            添加行("}");
            
            減少縮進();
            添加行("}");
            
            減少縮進();
            添加行("}");
            
            return 代碼構建器.ToString();
        }
        
        public void 訪問程序節點(程序節點 節點)
        {
            foreach (var 語句 in 節點.語句列表)
            {
                語句.接受訪問者(this);
            }
        }
        
        public void 訪問變量聲明節點(變量聲明節點 節點)
        {
            string csharp類型 = 轉換為CSharp類型(節點.變量類型);
            string 變量名 = 轉換標識符(節點.變量名稱);
            
            if (節點.初始值 != null)
            {
                添加縮進();
                代碼構建器.Append($"{csharp類型} {變量名} = ");
                節點.初始值.接受訪問者(this);
                代碼構建器.AppendLine(";");
            }
            else
            {
                string 默認值 = 獲取默認值(csharp類型);
                添加行($"{csharp類型} {變量名} = {默認值};");
            }
        }
        
        public void 訪問函數定義節點(函數定義節點 節點)
        {
            string 函數名 = 轉換標識符(節點.函數名稱);
            
            // 生成函數簽名
            添加縮進();
            代碼構建器.Append($"public static object {函數名}(");
            
            // 生成參數列表
            for (int i = 0; i < 節點.參數列表.Count; i++)
            {
                var 參數 = 節點.參數列表[i];
                string 參數類型 = 轉換為CSharp類型(參數.參數類型);
                string 參數名 = 轉換標識符(參數.參數名稱);
                
                代碼構建器.Append($"{參數類型} {參數名}");
                if (i < 節點.參數列表.Count - 1)
                {
                    代碼構建器.Append(", ");
                }
            }
            
            代碼構建器.AppendLine(")");
            添加行("{");
            增加縮進();
            
            // 生成函數體
            foreach (var 語句 in 節點.函數體)
            {
                語句.接受訪問者(this);
            }
            
            // 如果沒有返回語句，添加默認返回
            添加行("return null;");
            
            減少縮進();
            添加行("}");
            添加行("");
        }
        
        public void 訪問參數節點(參數節點 節點)
        {
            // 參數在函數定義中處理
        }
        
        public void 訪問條件語句節點(條件語句節點 節點)
        {
            添加縮進();
            代碼構建器.Append("if (");
            節點.條件表達式.接受訪問者(this);
            代碼構建器.AppendLine(")");
            添加行("{");
            增加縮進();
            
            // 生成真分支
            foreach (var 語句 in 節點.真分支)
            {
                語句.接受訪問者(this);
            }
            
            減少縮進();
            添加行("}");
            
            // 生成假分支
            if (節點.假分支.Count > 0)
            {
                添加行("else");
                添加行("{");
                增加縮進();
                
                foreach (var 語句 in 節點.假分支)
                {
                    語句.接受訪問者(this);
                }
                
                減少縮進();
                添加行("}");
            }
        }
        
        public void 訪問循環語句節點(循環語句節點 節點)
        {
            添加行("while (true)");
            添加行("{");
            增加縮進();
            
            foreach (var 語句 in 節點.循環體)
            {
                語句.接受訪問者(this);
            }
            
            減少縮進();
            添加行("}");
        }
        
        public void 訪問輸出語句節點(輸出語句節點 節點)
        {
            添加縮進();
            代碼構建器.Append("Console.WriteLine(");
            節點.輸出表達式.接受訪問者(this);
            代碼構建器.AppendLine(");");
        }
        
        public void 訪問返回語句節點(返回語句節點 節點)
        {
            添加縮進();
            代碼構建器.Append("return ");
            if (節點.返回值 != null)
            {
                節點.返回值.接受訪問者(this);
            }
            else
            {
                代碼構建器.Append("null");
            }
            代碼構建器.AppendLine(";");
        }
        
        public void 訪問賦值語句節點(賦值語句節點 節點)
        {
            string 變量名 = 轉換標識符(節點.變量名稱);
            添加縮進();
            代碼構建器.Append($"{變量名} = ");
            節點.賦值表達式.接受訪問者(this);
            代碼構建器.AppendLine(";");
        }
        
        public void 訪問二元運算表達式節點(二元運算表達式節點 節點)
        {
            代碼構建器.Append("(");
            節點.左操作數.接受訪問者(this);
            代碼構建器.Append($" {轉換運算符(節點.運算符)} ");
            節點.右操作數.接受訪問者(this);
            代碼構建器.Append(")");
        }
        
        public void 訪問字面量表達式節點(字面量表達式節點 節點)
        {
            switch (節點.類型)
            {
                case 詞法單元類型.數字:
                    代碼構建器.Append(節點.值.ToString());
                    break;
                case 詞法單元類型.字符串:
                    代碼構建器.Append($"\"{轉義字符串(節點.值.ToString())}\"");
                    break;
                default:
                    代碼構建器.Append(節點.值.ToString());
                    break;
            }
        }
        
        public void 訪問標識符表達式節點(標識符表達式節點 節點)
        {
            代碼構建器.Append(轉換標識符(節點.名稱));
        }
        
        public void 訪問函數調用表達式節點(函數調用表達式節點 節點)
        {
            string 函數名 = 轉換標識符(節點.函數名稱);
            代碼構建器.Append($"{函數名}(");
            
            for (int i = 0; i < 節點.參數列表.Count; i++)
            {
                節點.參數列表[i].接受訪問者(this);
                if (i < 節點.參數列表.Count - 1)
                {
                    代碼構建器.Append(", ");
                }
            }
            
            代碼構建器.Append(")");
        }
        
        /// <summary>
        /// 轉換文言文類型為C#類型
        /// </summary>
        /// <param name="文言文類型">文言文類型</param>
        /// <returns>C#類型</returns>
        private string 轉換為CSharp類型(string 文言文類型)
        {
            return 文言文類型 switch
            {
                "一數" => "double",
                "一言" => "string",
                "一列" => "List<object>",
                _ => "object"
            };
        }
        
        /// <summary>
        /// 轉換運算符
        /// </summary>
        /// <param name="文言文運算符">文言文運算符</param>
        /// <returns>C#運算符</returns>
        private string 轉換運算符(string 文言文運算符)
        {
            return 文言文運算符 switch
            {
                "加" => "+",
                "減" => "-",
                "乘" => "*",
                "除" => "/",
                "大於" => ">",
                "小於" => "<",
                "等於" => "==",
                _ => 文言文運算符
            };
        }
        
        /// <summary>
        /// 轉換標識符為有效的C#標識符
        /// </summary>
        /// <param name="標識符">原始標識符</param>
        /// <returns>C#標識符</returns>
        private string 轉換標識符(string 標識符)
        {
            // 簡單處理：為中文標識符添加前綴
            if (是中文字符串(標識符))
            {
                return $"變量_{標識符.GetHashCode().ToString().Replace("-", "N")}";
            }
            return 標識符;
        }
        
        /// <summary>
        /// 檢查是否為中文字符串
        /// </summary>
        /// <param name="文本">要檢查的文本</param>
        /// <returns>是否為中文字符串</returns>
        private bool 是中文字符串(string 文本)
        {
            foreach (char 字符 in 文本)
            {
                if (字符 >= 0x4e00 && 字符 <= 0x9fff)
                {
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// 獲取類型的默認值
        /// </summary>
        /// <param name="類型">類型名稱</param>
        /// <returns>默認值</returns>
        private string 獲取默認值(string 類型)
        {
            return 類型 switch
            {
                "double" => "0.0",
                "string" => "\"\"",
                "List<object>" => "new List<object>()",
                _ => "null"
            };
        }
        
        /// <summary>
        /// 轉義字符串
        /// </summary>
        /// <param name="文本">原始文本</param>
        /// <returns>轉義後的文本</returns>
        private string 轉義字符串(string 文本)
        {
            return 文本.Replace("\\", "\\\\")
                      .Replace("\"", "\\\"")
                      .Replace("\n", "\\n")
                      .Replace("\r", "\\r")
                      .Replace("\t", "\\t");
        }
        
        /// <summary>
        /// 添加縮進
        /// </summary>
        private void 添加縮進()
        {
            for (int i = 0; i < 縮進層級; i++)
            {
                代碼構建器.Append(縮進字符);
            }
        }
        
        /// <summary>
        /// 添加一行代碼
        /// </summary>
        /// <param name="代碼">代碼內容</param>
        private void 添加行(string 代碼)
        {
            添加縮進();
            代碼構建器.AppendLine(代碼);
        }
        
        /// <summary>
        /// 增加縮進層級
        /// </summary>
        private void 增加縮進()
        {
            縮進層級++;
        }
        
        /// <summary>
        /// 減少縮進層級
        /// </summary>
        private void 減少縮進()
        {
            if (縮進層級 > 0)
            {
                縮進層級--;
            }
        }
    }
} 