using System;
using System.Text;
using UnityEngine;

namespace Splend1d.UtilityPackage
{
    /// <summary>
    /// 把其它规范命名转化成驼峰命名
    /// </summary>
    public interface ICanSetPropertyByCamelCase
    {
        /// <summary>
        /// 把下划线命名规范转换成驼峰命名
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public string SetSnakeCaseToCamelCase(string sourceStr)
        {
            var array = sourceStr.Split('_');
            if (array == null) return "";
            StringBuilder builder=new StringBuilder();
            for (int i = 1; i < array.Length; i++)
            {
                builder.Append(array[i]);
                builder[0] = char.ToUpper(builder[0]);
                array[i] = builder.ToString();
                builder.Clear();
            }
            builder.Clear();
            for (int i = 0; i < array.Length; i++)
            {
                builder.Append(array[i]);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 把帕斯卡命名规范转换成驼峰命名
        /// </summary>
        /// <param name="sourceStr">输入串</param>
        /// <returns></returns>
        public string SetPascalCaseToCamelCase(string sourceStr)
        {
            
            StringBuilder builder=new StringBuilder(sourceStr);
            builder[0] = char.ToLower(builder[0]);
            return builder.ToString();
        }
    }
    /// <summary>
    /// 自定义的键盘映射类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct CustomKeyMapper<T>where T: Enum
    {
        public KeyCode code;
        public T customType;
    }

    public interface ICanCreateCustomKeyMapper
    {
        public CustomKeyMapper<T> CreateCustomKeyMapper<T>(T customType,KeyCode code) where T : Enum
        {
            CustomKeyMapper<T> mapper = new CustomKeyMapper<T>()
            {
                code=code,
                customType = customType
            };
            return mapper;
        }
    }
    public class KeyBoardMapperManager:ICanCreateCustomKeyMapper
    {
        private static KeyBoardMapperManager instance;
        // 同步锁用于线程安全
        private static readonly object lockObj = new object();
        // 私有构造函数，防止外部实例化
        private KeyBoardMapperManager() 
        {
            // 初始化代码
        }
        // 公共静态属性用于获取单例实例
        public static KeyBoardMapperManager Instance
        {
            get
            {
                // 双重检查锁定，以确保线程安全
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new KeyBoardMapperManager();
                        }
                    }
                }
                return instance;
            }
        }
        
    }
}