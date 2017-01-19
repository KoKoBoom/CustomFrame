/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/1/11 10:19:10
** Desc：	常用函数扩展
*********************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Taki.Logging;

namespace Taki.Common
{
    /// <summary>
    /// 扩展方法
    ///     其他扩展可参考 
    ///     NuGet的PGK.Extensions
    ///     
    /// Newtonsoft 高级用法 http://www.cnblogs.com/yanweidie/p/4605212.html
    /// </summary>
    public static class Extensions
    {
        #region 通用类型转换

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T">转换目标类型</typeparam>
        /// <param name="value">需要转换的值</param>
        /// <param name="isReturnDefault">true:如果异常则返回默认的（T）的值，false:抛出异常</param>
        /// <returns>T</returns>
        public static T To<T>(this object value, bool isReturnDefault = true)
        {
            var result = default(T);
            try
            {
                if (value != null)
                {
                    result = (T)Convert.ChangeType(value, typeof(T));
                }
                else if (!isReturnDefault)
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception) { if (!isReturnDefault) { throw; } }
            return result;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T">转换目标类型</typeparam>
        /// <param name="value">需要转换的值</param>
        /// <param name="defaultValue">转换失败时的默认值 当此值的类型为bool类型时，isReturnDefault必须赋值</param>
        /// <param name="isReturnDefault">true:如果异常则返回默认的（T）的值，false:抛出异常</param>
        /// <returns>T</returns>
        public static T To<T>(this object value, T defaultValue, bool isReturnDefault = true)
        {
            try
            {
                if (value != null)
                {
                    defaultValue = (T)Convert.ChangeType(value, typeof(T));
                }
                else if (!isReturnDefault)
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception) { if (!isReturnDefault) { throw; } }
            return defaultValue;
        }

        #endregion

        #region 返回指定日期的 00:00:00 时间
        /// <summary>
        /// 返回指定日期的 00:00:00 时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="isReturnDefault">【true:返回默认值 1970-01-01】 【false:抛出异常】</param>
        /// <returns></returns>
        public static DateTime GetToDayBeginDateTime(this DateTime dateTime, bool isReturnDefault = true)
        {
            try
            {
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
            }
            catch (Exception) { if (!isReturnDefault) throw; }
            return new DateTime(1970, 01, 01, 00, 00, 00);
        }

        /// <summary>
        /// 返回指定日期的 00:00:00 时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="isReturnDefault">如果发生异常是否返回默认值【true:返回默认值 1970-01-01】 【false:抛出异常】</param>
        /// <returns></returns>
        public static DateTime GetToDayBeginDateTime(this string dateTime, bool isReturnDefault = true)
        {
            var defaultValue = new DateTime(1970, 01, 01, 00, 00, 00);
            try
            {
                var _dateTime = dateTime.To<DateTime>(defaultValue, isReturnDefault);
                return new DateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, 0, 0, 0);
            }
            catch (Exception) { if (!isReturnDefault) throw; }
            return defaultValue;
        }

        /// <summary>
        /// 返回指定日期的 00:00:00 时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="defaultValue"> 【如果异常则返回 defaultValue】 ,【如果 defaultValue 也异常 则返回1970-01-01 00:00:00】</param>
        /// <param name="isWriteLog">转换失败是否需要写入日志【默认不写日志】</param>
        /// <returns></returns>
        public static DateTime GetToDayBeginDateTime(this string dateTime, string defaultValue, bool isWriteLog = false)
        {
            try
            {
                var _dateTime = dateTime.To<DateTime>(false);
                return new DateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, 0, 0, 0);
            }
            catch (Exception ex) { if (isWriteLog) LoggerFactory.Create()?.Error($"无法将 {dateTime} 转换为日期时间", ex, 1); }
            return defaultValue.GetToDayBeginDateTime();
        }

        /// <summary>
        /// 返回指定日期的 00:00:00 时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="defaultValue">【如果异常则返回 defaultValue】</param>
        /// <param name="isWriteLog">转换失败是否需要写入日志【默认不写日志】</param>
        /// <returns></returns>
        public static DateTime GetToDayBeginDateTime(this string dateTime, DateTime defaultValue, bool isWriteLog = false)
        {
            try
            {
                var _dateTime = dateTime.To<DateTime>(false);
                return new DateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, 0, 0, 0);
            }
            catch (Exception ex) { if (isWriteLog) LoggerFactory.Create()?.Error($"无法将 {dateTime} 转换为日期时间", ex, 1); }
            return defaultValue;
        }
        #endregion

        #region 返回指定日期的 23:59:59 时间
        /// <summary>
        /// 返回指定日期的 23:59:59 时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetToDayEndDateTime(this DateTime dateTime, bool isReturnDefault = true)
        {
            try
            {
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
            }
            catch (Exception) { if (!isReturnDefault) throw; }
            return new DateTime(9999, 12, 31, 23, 59, 59);
        }

        /// <summary>
        /// 返回指定日期的 23:59:59 时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="isReturnDefault">如果发生异常是否返回默认值【true:返回默认值 9999-12-31 23:59:59】 【false:抛出异常】</param>
        /// <returns></returns>
        public static DateTime GetToDayEndDateTime(this string dateTime, bool isReturnDefault = true)
        {
            var defaultValue = new DateTime(9999, 12, 31, 23, 59, 59);    //这里可以用 DateTime.MaxValue ，但是MaxValue将来肯能会改成其他值，具有不确定性。
            try
            {
                var _dateTime = dateTime.To<DateTime>(defaultValue, isReturnDefault);
                return new DateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, 23, 59, 59);
            }
            catch (Exception) { if (!isReturnDefault) throw; }
            return defaultValue;
        }


        /// <summary>
        /// 返回指定日期的 23:59:59 时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="defaultValue"> 【如果异常则返回 defaultValue】 ,【如果 defaultValue 也异常 则返回 9999-12-31 23:59:59】</param>
        /// <param name="isWriteLog">转换失败是否需要写入日志【默认不写日志】</param>
        /// <returns></returns>
        public static DateTime GetToDayEndDateTime(this string dateTime, string defaultValue, bool isWriteLog = false)
        {
            try
            {
                var _dateTime = dateTime.To<DateTime>(false);
                return new DateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, 23, 59, 59);
            }
            catch (Exception ex) { if (isWriteLog) LoggerFactory.Create()?.Error($"无法将 {dateTime} 转换为日期时间", ex, 1); }
            return defaultValue.GetToDayEndDateTime();
        }

        /// <summary>
        /// 返回指定日期的 23:59:59 时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="defaultValue">【如果异常则返回 defaultValue】</param>
        /// <param name="isWriteLog">转换失败是否需要写入日志【默认不写日志】</param>
        /// <returns></returns>
        public static DateTime GetToDayEndDateTime(this string dateTime, DateTime defaultValue, bool isWriteLog = false)
        {
            try
            {
                var _dateTime = dateTime.To<DateTime>(false);
                return new DateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, 23, 59, 59);
            }
            catch (Exception ex) { if (isWriteLog) LoggerFactory.Create()?.Error($"无法将 {dateTime} 转换为日期时间", ex, 1); }
            return defaultValue;
        }
        #endregion

        #region 时间戳转为C#格式时间
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">时间戳的值</param>
        /// <returns></returns>
        public static DateTime StampToDateTime(this int timeStamp)
        {
            DateTime dateTimeBegin = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeBegin.Add(toNow);
        }
        #endregion

        #region DateTime时间格式转换为Unix时间戳格式
        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        public static int ToUnixDate(this DateTime dateTime)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(dateTime - startTime).TotalSeconds;
        }
        #endregion

        #region 字符串真实长度 如:一个汉字为两个字节
        /// <summary>
        /// 字符串真实长度 如:一个汉字为两个字节  Null或者"" 返回 0
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthReal(this string s)
        {
            if (s.IsNotNullAndEmpty())
                return Encoding.Default.GetBytes(s).Length;
            else
                return 0;
        }
        #endregion

        #region 转全角(SBC case)
        /// <summary>
        /// 转全角(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        public static string ToSBC(this string input)
        {
            if (input.IsNotNullAndEmpty())
            {
                char[] c = input.ToCharArray();
                for (int i = 0; i < c.Length; i++)
                {
                    if (c[i] == 32)
                    {
                        c[i] = (char)12288;
                        continue;
                    }
                    if (c[i] < 127)
                        c[i] = (char)(c[i] + 65248);
                }
                return new string(c);
            }
            return string.Empty;
        }
        #endregion

        #region 转半角(DBC case)
        /// <summary>
        /// 转半角(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        public static string ToDBC(this string input)
        {
            if (input.IsNotNullAndWhiteSpace())
            {
                char[] c = input.ToCharArray();
                for (int i = 0; i < c.Length; i++)
                {
                    if (c[i] == 12288)
                    {
                        c[i] = (char)32;
                        continue;
                    }
                    if (c[i] > 65280 && c[i] < 65375)
                        c[i] = (char)(c[i] - 65248);
                }
                return new string(c);
            }
            return string.Empty;
        }
        #endregion

        #region IsNullOrEmpty IsNullOrWhiteSpace IsNotNullAndEmpty IsNotNullAndWhiteSpace
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static bool IsNotNullAndEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        public static bool IsNotNullAndWhiteSpace(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }
        #endregion

        #region WhenNullThenDefault WhenNullWhiteSpaceThenDefault
        /// <summary>
        /// 当字符串为Null时返回string.Empty，其它情况返回自己本身
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string WhenNullThenDefault(this string s)
        {
            return s ?? string.Empty;
        }

        /// <summary>
        /// 当字符串为Null、""、或者 "   " (空白) 时返回string.Empty，其它情况返回自己本身
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string WhenNullWhiteSpaceThenDefault(this string s)
        {
            return string.IsNullOrWhiteSpace(s) == true ? "" : s;
        }
        #endregion

        #region 序列 反序列
        /* Newtonsoft 高级用法：http://www.cnblogs.com/yanweidie/p/4605212.html */
        #endregion

        #region 序列化
        /// <summary>
        /// 将 obj 序列化为 JSON 字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isReturnDefaultValue">是否返回默认值 ==》 【true: 返回默认值(null)】 【false: 抛出异常】<</param>
        /// <param name="isWriteLog">转换失败是否需要写入日志【默认不写日志】</param>
        /// <returns></returns>
        public static string ToJson(this object obj, bool isReturnDefaultValue = true, bool isWriteLog = false)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                if (!isReturnDefaultValue)
                    throw new Exception("序列化 Json 失败", ex);
                if (isWriteLog) LoggerFactory.Create()?.Error($"序列化 Json 失败", ex, 1);
            }
            return null;
        }

        /// <summary>
        /// 将 obj 序列化为 JSON 字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isReturnDefaultValue">是否返回默认值 ==》 【true: 返回默认值(null)】 【false: 抛出异常】<</param>
        /// <returns></returns>
        public static string ToLitJson(this object obj, bool isReturnDefaultValue = true, bool isWriteLog = false)
        {
            try
            {
                return LitJson2.JsonMapper.ToJson(obj);
            }
            catch (Exception ex)
            {
                if (!isReturnDefaultValue)
                    throw new Exception("序列化 Json 失败", ex);
                if (isWriteLog) LoggerFactory.Create()?.Error($"序列化 Json 失败", ex, 1);
            }
            return null;
        }
        #endregion

        #region 反序列化
        /// <summary>
        /// 将 Json 反序列为 T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="isReturnDefaultValue">是否返回默认值 ==》 【true: 返回默认值 一般情况为 null】 【false: 抛出异常】</param>
        /// <param name="isWriteLog">转换失败是否需要写入日志【默认不写日志】</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json, bool isReturnDefaultValue = true, bool isWriteLog = false)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                if (!isReturnDefaultValue)
                    throw new Exception("反序列化 Json 失败", ex);
                if (isWriteLog) LoggerFactory.Create()?.Error($"反序列化 Json 失败", ex, 1);
            }
            return default(T);
        }

        /// <summary>
        /// 将 Json 反序列为 T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="isReturnDefaultValue">是否返回默认值 ==》 【true: 返回默认值 一般情况为 null】 【false: 抛出异常】</param>
        /// <param name="isWriteLog">转换失败是否需要写入日志【默认不写日志】</param>
        /// <returns></returns>
        public static T ToLitObject<T>(this string json, bool isReturnDefaultValue = true, bool isWriteLog = false)
        {
            try
            {
                return LitJson2.JsonMapper.ToObject<T>(json);
            }
            catch (Exception ex)
            {
                if (!isReturnDefaultValue)
                    throw new Exception("反序列化 Json 失败", ex);
                if (isWriteLog) LoggerFactory.Create()?.Error($"反序列化 Json 失败", ex, 1);
            }
            return default(T);
        }
        #endregion

        #region 正则
        /// <summary>
        ///  判断是否符合正则
        ///  示例：
        ///     bool b = "12345".IsMatch(@"\d+");
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string s, string pattern)
        {
            if (s == null)
                return false;
            else
                return Regex.IsMatch(s, pattern);
        }
        /// <summary>
        /// 匹配第一个符合正则的字符串
        /// 示例：
        ///     string s = "ldp615".Match("[a-zA-Z]+");
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string Match(this string s, string pattern)
        {
            if (s == null)
                return "";
            return
                Regex.Match(s, pattern).Value;
        }

        /// <summary>
        /// 匹配所有符合正则的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static IList<string> Matches(this string s, string pattern)
        {
            if (s == null)
                return null;
            else
                return Regex.Matches(s, pattern).ToList();
        }

        /// <summary>
        /// 循环 MatchCollection 子项
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IList<string> ToList(this MatchCollection s)
        {
            var results = new List<string>();
            if (s != null && s.Count > 0)
            {
                foreach (Match item in s)
                {
                    results.Add(item.Value);
                }
            }
            return results;
        }

        #endregion

        #region byte 操作
        public static void Save(this byte[] data, string path)
        {
            System.IO.File.WriteAllBytes(path, data);
        }

        public static System.IO.MemoryStream ToMemoryStream(this byte[] data)
        {
            return new System.IO.MemoryStream(data);
        }
        #endregion

        #region Dictionary<TKey, TValue> 操作

        #region 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// <summary>
        /// 尝试将键和值添加到字典中：如果【不存在，才添加】；【存在，不添加】也不抛导常
        /// </summary>
        public static void TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key) == false) dict.Add(key, value);
        }
        #endregion

        #region 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// <summary>
        /// 将键和值添加或替换到字典中：如果【不存在，则添加】；【存在，则替换】
        /// </summary>
        public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
        }
        #endregion

        #region 向字典中批量添加键值对
        /// <summary>
        /// 向字典中批量添加键值对
        /// 没有考虑线程安全 ConcurrentDictionary
        /// </summary>
        /// <param name="replaceExisted">如果已存在，是否替换</param>
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
        {
            foreach (var item in values.WhenNullThenDefault())
            {
                if (dict.ContainsKey(item.Key) == false || replaceExisted)
                    dict[item.Key] = item.Value;
            }
        }
        #endregion

        #region 获取与指定的键相关联的值，如果没有则返回输入的默认值
        /// <summary>
        /// 获取与指定的键相关联的值，如果没有则返回输入的默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            TValue result;
            if (dict.TryGetValue(key, out result) == false)
                result = defaultValue;
            return result;
        }
        #endregion

        #endregion

        #region t是否在lowerBound 和 upperBound 之间
        /// <summary>
        /// t是否在lowerBound 和 upperBound 之间 （例如 2 是否在 1 和 3 之间）
        /// includeLowerBound 是否包含下边界 即 t == lowerBound 的情况
        /// includeUpperBound 是否包含上边界 即 t == upperBound 的情况
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <param name="includeLowerBound"></param>
        /// <param name="includeUpperBound"></param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T t, T lowerBound, T upperBound, bool includeLowerBound = false, bool includeUpperBound = false) where T : IComparable<T>
        {
            if (t == null) throw new ArgumentNullException("t");

            var lowerCompareResult = t.CompareTo(lowerBound);
            var upperCompareResult = t.CompareTo(upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }
        #endregion

        #region List扩展

        #region 检查是否为空  不检查集合数量
        /// <summary>
        /// 检查是否为空  不检查集合数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this IEnumerable<T> s)
        {
            return s == null;
        }
        #endregion

        #region 如果为空则返回一个数量为0的List<T>  一般用于 foreach  例如 foreach(var item in list.WhenNullThenDefault())
        /// <summary>
        /// 如果为空则返回一个数量为0的List[T]   一般用于 foreach  例如 foreach(var item in list.WhenNullThenDefault())
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhenNullThenDefault<T>(this IEnumerable<T> s)
        {
            return s == null ? new List<T>(0) : s;
        }
        #endregion

        #region 检查集合不为空 不检查子项个数
        /// <summary>
        /// 不为空 不检查子项个数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this IEnumerable<T> s)
        {
            return s != null;
        }
        #endregion

        #region 集合为空或者子项个数为0
        /// <summary>
        /// 集合为空或者子项个数为0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> s)
        {
            return s == null || !s.Any();
        }
        #endregion

        #region 集合不为空 且存在子项
        /// <summary>
        /// 集合不为空 且存在子项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotNullAndEmpty<T>(this IEnumerable<T> s)
        {
            return s != null && s.Any();
        }
        #endregion

        #region 扩展
        public static T Next<T>(this IList<T> list, int index)
        {
            if (list != null)
            {
                if (index + 1 < list.Count)
                {
                    return list[index + 1];
                }
            }
            return default(T);
        }


        /// <summary>
        /// 获取集合的下一项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T Next<T>(this IList<T> list, T item)
        {
            if (list != null)
            {
                var index = list.IndexOf(item);
                if (index > -1 && index + 1 < list.Count)
                {
                    return list[index + 1];
                }
            }
            return default(T);
        }

        public static T NextOrDefault<T>(this IList<T> list, int index, int defaultIndex = 0)
        {
            try
            {
                if (list.IsNotNullAndEmpty())
                {
                    if (index + 1 < list.Count)
                    {
                        return list[index + 1];
                    }
                    return list[defaultIndex];
                }
                return default(T);
            }
            catch (Exception ex) { }
            return default(T);
        }

        /// <summary>
        /// 获取集合的下一项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <param name="defaultIndex">默认返回值的Index</param>
        /// <returns></returns>
        public static T NextOrDefault<T>(this IList<T> list, T item, int defaultIndex = 0)
        {
            try
            {
                if (list.IsNotNullAndEmpty())
                {
                    var index = list.IndexOf(item);
                    if (index + 1 < list.Count)
                    {
                        return list[index + 1];
                    }
                    return list[defaultIndex];
                }
                return default(T);
            }
            catch (Exception ex) { }
            return default(T);
        }
        #endregion

        #endregion

        #region 切分
        /// <summary>
        /// 切分
        /// </summary>
        /// <param name="str"></param>
        /// <param name="speater"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static List<string> Split(this string str, char speater, bool toLower = false)
        {
            List<string> list = new List<string>();
            if (str.IsNotNullAndWhiteSpace() && speater != Char.MinValue)
            {
                if (toLower) { str.ToLower(); }
                string[] ss = str.Split(speater);
                if (ss.IsNotNullAndEmpty())
                {
                    list = ss.ToList();
                }
            }
            return list;
        }

        /// <summary>
        /// 切分，默认按“,”切分   如果 str == null  则返回 null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] Split(this string str)
        {
            if (str != null)
            {
                return str.Split(new char[',']);
            }
            return null;
        }

        /// <summary>
        /// 按 speater 连接成字符串   如果 list 为空 则返回 ""
        /// </summary>
        /// <param name="list"></param>
        /// <param name="speater"></param>
        /// <returns> 如果 list 为空 返回null</returns>
        public static string Join(this List<string> list, string speater)
        {
            return string.Join(speater, list.WhenNullThenDefault());
        }
        #endregion

        #region 删除最后一个字符之后的字符

        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string TrimEnd(this string str)
        {
            return str.WhenNullThenDefault().TrimEnd(',');
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string TrimEnd(this string str, string strchar)
        {
            var index = str.LastIndexOf(strchar);
            if (index > -1)
                return str.Substring(0, index);
            return str;
        }
        #endregion

    }

    /// <summary>
    /// 正则集合
    /// </summary>
    public class Regexs
    {
        /// <summary>
        /// 整数
        /// </summary>
        public const string Intege = @"^-?\d*$";
        /// <summary>
        /// 正整数
        /// </summary>
        public const string UIntege = @"^[1-9]\d*$";
        /// <summary>
        /// 负整数
        /// </summary>
        public const string NegateIntege = @"^-[1-9]\d*$";
        /// <summary>
        /// 数字
        /// </summary>
        public const string Num = @"^([+-]?)\d*$";
        /// <summary>
        /// 浮点数 不包括 0
        /// </summary>
        public const string Decmal = @"^([+-]?)\d*\.\d+$";
        /// <summary>
        /// 正浮点数
        /// </summary>
        public const string UDecmal = @"^[1-9]\d*.\d*|0.\d*[1-9]\d*$";
        /// <summary>
        /// 负浮点数
        /// </summary>
        public const string NegateDecmal = @"^-([1-9]\d*.\d*|0.\d*[1-9]\d*)$";
        /// <summary>
        /// 浮点数 包括 0
        /// </summary>
        public const string DecmalWithZero = @"^-?([1-9]\d*.\d*|0.\d*[1-9]\d*|0?.0+|0)$";
        /// <summary>
        /// 非负浮点数（正浮点数 + 0）
        /// </summary>
        public const string UDecmalWithZero = @"^[1-9]\d*|0$.\d*|0.\d*[1-9]\d*|0?.0+|0$";
        /// <summary>
        /// 非正浮点数（负浮点数 + 0）
        /// </summary>
        public const string NegateDecmalWithZero = @"^(-([1-9]\d*.\d*|0.\d*[1-9]\d*))|0?.0+|0$";
        /// <summary>
        /// 邮件
        /// </summary>
        public const string Email = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f] |\[\x01-\x09\x0b\x0c\x0e-\x7f])*)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
        /// <summary>
        /// 地址
        /// </summary>
        public const string Url = @"^http[s]?:\/\/([\w-]+\.)+[\w-]+([\w-./?%&=]*)?$";
        /// <summary>
        /// 中文
        /// </summary>
        public const string Chinese = @"^[\u4E00-\u9FA5\uF900-\uFA2D]+$";
        /// <summary>
        /// ACSII字符
        /// </summary>
        public const string Ascii = @"^[\x00-\xFF]+$";
        /// <summary>
        /// 邮编
        /// </summary>
        public const string ZipCode = @"^\d{6}$";
        /// <summary>
        /// 手机 （截止到2015年最新）
        /// 手机号码:
        ///     13[0-9], 14[5,7], 15[0, 1, 2, 3, 5, 6, 7, 8, 9], 17[6, 7, 8], 18[0-9], 170[0-9] 
        ///     移动号段: 134,135,136,137,138,139,150,151,152,157,158,159,182,183,184,187,188,147,178,1705 
        ///     联通号段: 130,131,132,155,156,185,186,145,176,1709 
        ///     电信号段: 133,153,180,181,189,177,1700 
        /// </summary>
        public const string Mobile = @"^1(3[0-9]|4[57]|5[0-35-9]|8[0-9]|70)\d{8}$";
        /// <summary>
        /// IP4
        /// </summary>
        public const string IP4 = @"^(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)$";
        /// <summary>
        /// 图片
        /// </summary>
        public const string Picture = @"(.*)\.(jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$";
        /// <summary>
        /// 压缩文件
        /// </summary>
        public const string RAR = @"(.*)\.(rar|zip|7zip|tgz)$";
        /// <summary>
        /// 日期
        /// </summary>
        public const string Date = @"^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$";
        /// <summary>
        /// 中国身份证
        /// </summary>
        public const string ChinaID = @"(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$)|(^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}[0-9xX]$)";

    }
}