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
                    throw new FormatException();
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
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <param name="ifExceptionReturnDefault">请输入true ，这个参数完全是为了区别重载，没有任何用处。</param>
        /// <returns>T</returns>
        public static T To<T>(this object value, T defaultValue, bool ifExceptionReturnDefault)
        {
            try
            {
                if (value != null)
                {
                    defaultValue = (T)Convert.ChangeType(value, typeof(T));
                }
            }
            catch (Exception) { }
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
                var _dateTime = dateTime.To<DateTime>(defaultValue, true);
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
                var _dateTime = dateTime.To<DateTime>(defaultValue, true);
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
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key) == false) dict.Add(key, value);
            return dict;
        }
        #endregion

        #region 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// </summary>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }
        #endregion

        #region 向字典中批量添加键值对
        /// <summary>
        /// 向字典中批量添加键值对
        /// 没有考虑线程安全 ConcurrentDictionary
        /// </summary>
        /// <param name="replaceExisted">如果已存在，是否替换</param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
        {
            foreach (var item in values)
            {
                if (dict.ContainsKey(item.Key) == false || replaceExisted)
                    dict[item.Key] = item.Value;
            }
            return dict;
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

        public static bool IsEmpty<T>(this IEnumerable<T> s)
        {
            return !s.Any();
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> s)
        {
            return s.Any();
        }

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
                if (list != null && list.Count() > 0)
                {
                    var result = list[defaultIndex];
                    if (index + 1 < list.Count)
                    {
                        return list[index + 1];
                    }
                    return result;
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
                if (list != null && list.Count() > 0)
                {
                    var result = list[defaultIndex];
                    var index = list.IndexOf(item);
                    if (index + 1 < list.Count)
                    {
                        return list[index + 1];
                    }
                    return result;
                }
                return default(T);
            }
            catch (Exception ex) { }
            return default(T);
        }
        #endregion

        #region 切分
        /// <summary>
        /// 切分
        /// </summary>
        /// <param name="str"></param>
        /// <param name="speater"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static List<string> SplitArray(this string str, char speater, bool toLower = false)
        {
            List<string> list = new List<string>();
            if (str.IsNotNullAndWhiteSpace() && speater != Char.MinValue)
            {
                if (toLower) { str.ToLower(); }
                string[] ss = str.Split(speater);
                if (ss.IsNotEmpty())
                {
                    list = ss.ToList();
                }
            }
            return list;
        }

        /// <summary>
        /// 切分，默认切分 “，”
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitArray(this string str)
        {
            return str.Split(new char[',']);
        }

        /// <summary>
        /// 按 speater 连接成字符串 如果 list 为空 返回null
        /// </summary>
        /// <param name="list"></param>
        /// <param name="speater"></param>
        /// <returns> 如果 list 为空 返回null</returns>
        public static string Join(this List<string> list, string speater)
        {
            if (list.IsNotEmpty())
            {
                return string.Join(speater, list);
            }
            return null;
        }
        #endregion

        #region 删除最后一个字符之后的字符

        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string TrimEnd(this string str)
        {
            return str.TrimEnd(',');
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
}