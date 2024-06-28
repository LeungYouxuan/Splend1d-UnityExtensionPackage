using System.Reflection;
using Splend1d.UtilityPackage;
using UnityEngine.UI;
namespace Splend1d.UIPackage.TextExtensionPackage
{
    /// <summary>
    /// 根据传入类的字段，对Text组件进行自动赋值
    /// </summary>
    public interface ICanAutoSetTextValue:ICanSetPropertyByCamelCase
    {
        public void AutoSetTextValue(object sourceData)
        {
            if (sourceData == null)
                return;
            ICanSetPropertyByCamelCase toCamelCaseLoader = this;
            var type = sourceData.GetType();
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                //把下划线命名转换成驼峰命名
                var camelCaseName = toCamelCaseLoader.SetSnakeCaseToCamelCase(field.Name);
                var value = field.GetValue(sourceData);
                // 查找并设置UI元素的值
                var uiField = this.GetType().GetField(camelCaseName + "Text", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                if (uiField != null && uiField.FieldType == typeof(Text))
                {
                    var uiText = (Text)uiField.GetValue(this);
                    uiText.text = value.ToString();
                }
                // 设置对应的字段值
                var targetField = this.GetType().GetField(camelCaseName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                if (targetField != null)
                {
                    targetField.SetValue(this, value);
                }
            }
        }
    }
}