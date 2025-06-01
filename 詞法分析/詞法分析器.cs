using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace 詞法分析
{
    /// <summary>
    /// 詞法分析器 - 將文言文源代碼分解為詞法單元
    /// </summary>
    public class 詞法分析器
    {
        private string 源代碼;
        private int 當前位置;
        private int 當前行號;
        private int 當前列號;
        private List<詞法單元> 詞法單元列表;
        
        /// <summary>
        /// 構造函數
        /// </summary>
        public 詞法分析器()
        {
            詞法單元列表 = new List<詞法單元>();
        }
        
        /// <summary>
        /// 分析源代碼並生成詞法單元列表
        /// </summary>
        /// <param name="源代碼">要分析的源代碼</param>
        /// <returns>詞法單元列表</returns>
        public List<詞法單元> 分析(string 源代碼)
        {
            this.源代碼 = 源代碼;
            this.當前位置 = 0;
            this.當前行號 = 1;
            this.當前列號 = 1;
            this.詞法單元列表.Clear();
            
            while (!是文件結束())
            {
                跳過空白字符();
                
                if (是文件結束())
                    break;
                
                char 當前字符 = 獲取當前字符();
                
                // 處理註釋（以//開始的行註釋）
                if (當前字符 == '/' && 查看下一個字符() == '/')
                {
                    跳過行註釋();
                    continue;
                }
                
                // 處理字符串字面量
                if (當前字符 == '「')
                {
                    處理字符串字面量();
                    continue;
                }
                
                // 處理標點符號
                if (處理標點符號())
                {
                    continue;
                }
                
                // 處理阿拉伯數字
                if (char.IsDigit(當前字符))
                {
                    處理阿拉伯數字();
                    continue;
                }
                
                // 處理中文字符（關鍵字、標識符、文言文數字）
                if (是中文字符(當前字符))
                {
                    處理中文詞彙();
                    continue;
                }
                
                // 處理英文字符（標識符）
                if (char.IsLetter(當前字符) || 當前字符 == '_')
                {
                    處理英文標識符();
                    continue;
                }
                
                // 未知字符
                添加詞法單元(詞法單元類型.未知, 當前字符.ToString());
                前進();
            }
            
            // 添加文件結束標記
            添加詞法單元(詞法單元類型.文件結束, "");
            
            return 詞法單元列表;
        }
        
        /// <summary>
        /// 處理字符串字面量
        /// </summary>
        private void 處理字符串字面量()
        {
            StringBuilder 字符串內容 = new StringBuilder();
            前進(); // 跳過開始的「
            
            while (!是文件結束() && 獲取當前字符() != '」')
            {
                字符串內容.Append(獲取當前字符());
                前進();
            }
            
            if (!是文件結束())
            {
                前進(); // 跳過結束的」
            }
            
            添加詞法單元(詞法單元類型.字符串, 字符串內容.ToString());
        }
        
        /// <summary>
        /// 處理標點符號
        /// </summary>
        /// <returns>是否處理了標點符號</returns>
        private bool 處理標點符號()
        {
            char 當前字符 = 獲取當前字符();
            
            switch (當前字符)
            {
                case '「':
                    添加詞法單元(詞法單元類型.左引號, "「");
                    前進();
                    return true;
                case '」':
                    添加詞法單元(詞法單元類型.右引號, "」");
                    前進();
                    return true;
                case '、':
                    添加詞法單元(詞法單元類型.頓號, "、");
                    前進();
                    return true;
                default:
                    return false;
            }
        }
        
        /// <summary>
        /// 處理阿拉伯數字
        /// </summary>
        private void 處理阿拉伯數字()
        {
            StringBuilder 數字 = new StringBuilder();
            
            while (!是文件結束() && (char.IsDigit(獲取當前字符()) || 獲取當前字符() == '.'))
            {
                數字.Append(獲取當前字符());
                前進();
            }
            
            添加詞法單元(詞法單元類型.數字, 數字.ToString());
        }
        
        /// <summary>
        /// 處理中文詞彙（關鍵字、標識符、文言文數字）
        /// </summary>
        private void 處理中文詞彙()
        {
            // 嘗試匹配最長的關鍵字
            詞法單元類型 匹配結果 = 嘗試匹配關鍵字();
            
            if (匹配結果 != 詞法單元類型.標識符)
            {
                // 已經在嘗試匹配關鍵字中處理了
                return;
            }
            
            // 如果不是關鍵字，收集單個中文字符作為標識符或數字
            StringBuilder 詞彙 = new StringBuilder();
            詞彙.Append(獲取當前字符());
            前進();
            
            string 詞彙文本 = 詞彙.ToString();
            
            if (關鍵字表.是文言文數字(詞彙文本))
            {
                添加詞法單元(詞法單元類型.數字, 詞彙文本);
            }
            else
            {
                添加詞法單元(詞法單元類型.標識符, 詞彙文本);
            }
        }
        
        /// <summary>
        /// 嘗試匹配關鍵字
        /// </summary>
        /// <returns>匹配到的詞法單元類型</returns>
        private 詞法單元類型 嘗試匹配關鍵字()
        {
            // 從最長的可能關鍵字開始匹配（最長4個字符）
            for (int 長度 = 4; 長度 > 0; 長度--)
            {
                if (當前位置 + 長度 > 源代碼.Length)
                    continue;
                    
                string 子字符串 = 源代碼.Substring(當前位置, 長度);
                if (關鍵字表.是關鍵字(子字符串))
                {
                    // 找到關鍵字，前進相應位置
                    for (int i = 0; i < 長度; i++)
                    {
                        前進();
                    }
                    
                    詞法單元類型 類型 = 關鍵字表.獲取關鍵字類型(子字符串);
                   添加詞法單元(類型, 子字符串);
                    return 類型;
                }
            }
            
            return 詞法單元類型.標識符;
        }
        
        /// <summary>
        /// 處理英文標識符
        /// </summary>
        private void 處理英文標識符()
        {
            StringBuilder 標識符 = new StringBuilder();
            
            while (!是文件結束() && (char.IsLetterOrDigit(獲取當前字符()) || 獲取當前字符() == '_'))
            {
                標識符.Append(獲取當前字符());
                前進();
            }
            
            添加詞法單元(詞法單元類型.標識符, 標識符.ToString());
        }
        
        /// <summary>
        /// 跳過空白字符
        /// </summary>
        private void 跳過空白字符()
        {
            while (!是文件結束() && char.IsWhiteSpace(獲取當前字符()))
            {
                if (獲取當前字符() == '\n')
                {
                    當前行號++;
                    當前列號 = 1;
                }
                else
                {
                    當前列號++;
                }
                當前位置++;
            }
        }
        
        /// <summary>
        /// 跳過行註釋
        /// </summary>
        private void 跳過行註釋()
        {
            while (!是文件結束() && 獲取當前字符() != '\n')
            {
                前進();
            }
        }
        
        /// <summary>
        /// 判斷字符是否為中文字符
        /// </summary>
        /// <param name="字符">要判斷的字符</param>
        /// <returns>是否為中文字符</returns>
        private bool 是中文字符(char 字符)
        {
            return 字符 >= 0x4e00 && 字符 <= 0x9fff;
        }
        
        /// <summary>
        /// 獲取當前字符
        /// </summary>
        /// <returns>當前字符</returns>
        private char 獲取當前字符()
        {
            if (是文件結束())
                return '\0';
            return 源代碼[當前位置];
        }
        
        /// <summary>
        /// 查看下一個字符但不移動位置
        /// </summary>
        /// <returns>下一個字符</returns>
        private char 查看下一個字符()
        {
            if (當前位置 + 1 >= 源代碼.Length)
                return '\0';
            return 源代碼[當前位置 + 1];
        }
        
        /// <summary>
        /// 前進一個字符
        /// </summary>
        private void 前進()
        {
            if (!是文件結束())
            {
                if (獲取當前字符() == '\n')
                {
                    當前行號++;
                    當前列號 = 1;
                }
                else
                {
                    當前列號++;
                }
                當前位置++;
            }
        }
        
        /// <summary>
        /// 後退一個字符
        /// </summary>
        private void 後退()
        {
            if (當前位置 > 0)
            {
                當前位置--;
                if (當前列號 > 1)
                {
                    當前列號--;
                }
                else
                {
                    當前行號--;
                    // 簡化處理，不計算上一行的實際列數
                    當前列號 = 1;
                }
            }
        }
        
        /// <summary>
        /// 判斷是否到達文件結束
        /// </summary>
        /// <returns>是否到達文件結束</returns>
        private bool 是文件結束()
        {
            return 當前位置 >= 源代碼.Length;
        }
        
        /// <summary>
        /// 添加詞法單元到列表
        /// </summary>
        /// <param name="類型">詞法單元類型</param>
        /// <param name="值">詞法單元值</param>
        private void 添加詞法單元(詞法單元類型 類型, string 值)
        {
            詞法單元列表.Add(new 詞法單元(類型, 值, 當前行號, 當前列號));
        }
    }
} 