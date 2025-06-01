using System;
using System.Collections.Generic;
using 詞法分析;

namespace 語法分析
{
    /// <summary>
    /// 抽象語法樹節點基類
    /// </summary>
    public abstract class 抽象語法樹節點
    {
        /// <summary>
        /// 節點在源代碼中的行號
        /// </summary>
        public int 行號 { get; set; }
        
        /// <summary>
        /// 節點在源代碼中的列號
        /// </summary>
        public int 列號 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="行號">行號</param>
        /// <param name="列號">列號</param>
        protected 抽象語法樹節點(int 行號, int 列號)
        {
            this.行號 = 行號;
            this.列號 = 列號;
        }
        
        /// <summary>
        /// 接受訪問者模式
        /// </summary>
        /// <param name="訪問者">訪問者</param>
        public abstract void 接受訪問者(語法樹訪問者 訪問者);
    }
    
    /// <summary>
    /// 程序節點 - 整個程序的根節點
    /// </summary>
    public class 程序節點 : 抽象語法樹節點
    {
        /// <summary>
        /// 語句列表
        /// </summary>
        public List<語句節點> 語句列表 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 程序節點() : base(1, 1)
        {
            語句列表 = new List<語句節點>();
        }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問程序節點(this);
        }
    }
    
    /// <summary>
    /// 語句節點基類
    /// </summary>
    public abstract class 語句節點 : 抽象語法樹節點
    {
        protected 語句節點(int 行號, int 列號) : base(行號, 列號) { }
    }
    
    /// <summary>
    /// 變量聲明語句節點
    /// </summary>
    public class 變量聲明節點 : 語句節點
    {
        /// <summary>
        /// 變量類型
        /// </summary>
        public string 變量類型 { get; set; }
        
        /// <summary>
        /// 變量名稱
        /// </summary>
        public string 變量名稱 { get; set; }
        
        /// <summary>
        /// 初始值表達式
        /// </summary>
        public 表達式節點 初始值 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 變量聲明節點(int 行號, int 列號) : base(行號, 列號) { }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問變量聲明節點(this);
        }
    }
    
    /// <summary>
    /// 函數定義節點
    /// </summary>
    public class 函數定義節點 : 語句節點
    {
        /// <summary>
        /// 函數名稱
        /// </summary>
        public string 函數名稱 { get; set; }
        
        /// <summary>
        /// 參數列表
        /// </summary>
        public List<參數節點> 參數列表 { get; set; }
        
        /// <summary>
        /// 函數體語句列表
        /// </summary>
        public List<語句節點> 函數體 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 函數定義節點(int 行號, int 列號) : base(行號, 列號)
        {
            參數列表 = new List<參數節點>();
            函數體 = new List<語句節點>();
        }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問函數定義節點(this);
        }
    }
    
    /// <summary>
    /// 參數節點
    /// </summary>
    public class 參數節點 : 抽象語法樹節點
    {
        /// <summary>
        /// 參數類型
        /// </summary>
        public string 參數類型 { get; set; }
        
        /// <summary>
        /// 參數名稱
        /// </summary>
        public string 參數名稱 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 參數節點(int 行號, int 列號) : base(行號, 列號) { }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問參數節點(this);
        }
    }
    
    /// <summary>
    /// 條件語句節點
    /// </summary>
    public class 條件語句節點 : 語句節點
    {
        /// <summary>
        /// 條件表達式
        /// </summary>
        public 表達式節點 條件表達式 { get; set; }
        
        /// <summary>
        /// 真分支語句列表
        /// </summary>
        public List<語句節點> 真分支 { get; set; }
        
        /// <summary>
        /// 假分支語句列表
        /// </summary>
        public List<語句節點> 假分支 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 條件語句節點(int 行號, int 列號) : base(行號, 列號)
        {
            真分支 = new List<語句節點>();
            假分支 = new List<語句節點>();
        }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問條件語句節點(this);
        }
    }
    
    /// <summary>
    /// 循環語句節點
    /// </summary>
    public class 循環語句節點 : 語句節點
    {
        /// <summary>
        /// 循環體語句列表
        /// </summary>
        public List<語句節點> 循環體 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 循環語句節點(int 行號, int 列號) : base(行號, 列號)
        {
            循環體 = new List<語句節點>();
        }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問循環語句節點(this);
        }
    }
    
    /// <summary>
    /// 輸出語句節點
    /// </summary>
    public class 輸出語句節點 : 語句節點
    {
        /// <summary>
        /// 輸出表達式
        /// </summary>
        public 表達式節點 輸出表達式 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 輸出語句節點(int 行號, int 列號) : base(行號, 列號) { }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問輸出語句節點(this);
        }
    }
    
    /// <summary>
    /// 返回語句節點
    /// </summary>
    public class 返回語句節點 : 語句節點
    {
        /// <summary>
        /// 返回值表達式
        /// </summary>
        public 表達式節點 返回值 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 返回語句節點(int 行號, int 列號) : base(行號, 列號) { }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問返回語句節點(this);
        }
    }
    
    /// <summary>
    /// 賦值語句節點
    /// </summary>
    public class 賦值語句節點 : 語句節點
    {
        /// <summary>
        /// 變量名稱
        /// </summary>
        public string 變量名稱 { get; set; }
        
        /// <summary>
        /// 賦值表達式
        /// </summary>
        public 表達式節點 賦值表達式 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 賦值語句節點(int 行號, int 列號) : base(行號, 列號) { }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問賦值語句節點(this);
        }
    }
    
    /// <summary>
    /// 表達式節點基類
    /// </summary>
    public abstract class 表達式節點 : 抽象語法樹節點
    {
        protected 表達式節點(int 行號, int 列號) : base(行號, 列號) { }
    }
    
    /// <summary>
    /// 二元運算表達式節點
    /// </summary>
    public class 二元運算表達式節點 : 表達式節點
    {
        /// <summary>
        /// 左操作數
        /// </summary>
        public 表達式節點 左操作數 { get; set; }
        
        /// <summary>
        /// 運算符
        /// </summary>
        public string 運算符 { get; set; }
        
        /// <summary>
        /// 右操作數
        /// </summary>
        public 表達式節點 右操作數 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 二元運算表達式節點(int 行號, int 列號) : base(行號, 列號) { }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問二元運算表達式節點(this);
        }
    }
    
    /// <summary>
    /// 字面量表達式節點
    /// </summary>
    public class 字面量表達式節點 : 表達式節點
    {
        /// <summary>
        /// 字面量值
        /// </summary>
        public object 值 { get; set; }
        
        /// <summary>
        /// 字面量類型
        /// </summary>
        public 詞法單元類型 類型 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 字面量表達式節點(int 行號, int 列號) : base(行號, 列號) { }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問字面量表達式節點(this);
        }
    }
    
    /// <summary>
    /// 標識符表達式節點
    /// </summary>
    public class 標識符表達式節點 : 表達式節點
    {
        /// <summary>
        /// 標識符名稱
        /// </summary>
        public string 名稱 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 標識符表達式節點(int 行號, int 列號) : base(行號, 列號) { }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問標識符表達式節點(this);
        }
    }
    
    /// <summary>
    /// 函數調用表達式節點
    /// </summary>
    public class 函數調用表達式節點 : 表達式節點
    {
        /// <summary>
        /// 函數名稱
        /// </summary>
        public string 函數名稱 { get; set; }
        
        /// <summary>
        /// 參數列表
        /// </summary>
        public List<表達式節點> 參數列表 { get; set; }
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 函數調用表達式節點(int 行號, int 列號) : base(行號, 列號)
        {
            參數列表 = new List<表達式節點>();
        }
        
        public override void 接受訪問者(語法樹訪問者 訪問者)
        {
            訪問者.訪問函數調用表達式節點(this);
        }
    }
    
    /// <summary>
    /// 語法樹訪問者接口
    /// </summary>
    public interface 語法樹訪問者
    {
        void 訪問程序節點(程序節點 節點);
        void 訪問變量聲明節點(變量聲明節點 節點);
        void 訪問函數定義節點(函數定義節點 節點);
        void 訪問參數節點(參數節點 節點);
        void 訪問條件語句節點(條件語句節點 節點);
        void 訪問循環語句節點(循環語句節點 節點);
        void 訪問輸出語句節點(輸出語句節點 節點);
        void 訪問返回語句節點(返回語句節點 節點);
        void 訪問賦值語句節點(賦值語句節點 節點);
        void 訪問二元運算表達式節點(二元運算表達式節點 節點);
        void 訪問字面量表達式節點(字面量表達式節點 節點);
        void 訪問標識符表達式節點(標識符表達式節點 節點);
        void 訪問函數調用表達式節點(函數調用表達式節點 節點);
    }
} 